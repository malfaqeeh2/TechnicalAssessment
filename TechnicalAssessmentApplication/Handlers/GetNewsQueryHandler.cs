using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using TechnicalAssessmentApplication.DTOs;
using TechnicalAssessmentApplication.Mappers;
using TechnicalAssessmentApplication.Queries;

namespace TechnicalAssessmentApplication.Handlers
{
    public class GetNewsQueryHandler : IRequestHandler<GetNewsQuery, NewsResponseDto>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<GetNewsQueryHandler> _logger;
        private readonly IMemoryCache _memoryCache;
        private const int CacheExpirationSeconds = 60;

        public GetNewsQueryHandler(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            ILogger<GetNewsQueryHandler> logger,
            IMemoryCache memoryCache)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public async Task<NewsResponseDto> Handle(GetNewsQuery request, CancellationToken cancellationToken)
        {

            var cacheKey = $"news:uae:page:{request.Page}";

            // Try to get from cache first
            if (_memoryCache.TryGetValue(cacheKey, out NewsResponseDto? cachedResult))
            {
                _logger.LogInformation("Cache hit for key: {CacheKey}", cacheKey);
                return cachedResult!;
            }

            _logger.LogInformation("Cache miss for key: {CacheKey}. Fetching from API...", cacheKey);

            var apiKey = _configuration["NewsApi:ApiKey"] 
                ?? throw new InvalidOperationException("NewsApi:ApiKey is not configured");
            
            var baseUrl = _configuration["NewsApi:BaseUrl"] ?? "https://newsapi.org/v2";
            
            var url = $"{baseUrl}/everything?q=uae&apiKey={apiKey}&page={request.Page}";

            var httpClient = _httpClientFactory.CreateClient("NewsApi");
            
            try
            {
                var response = await httpClient.GetAsync(url, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                    _logger.LogError("News API returned status {StatusCode}: {ErrorContent}", 
                        response.StatusCode, errorContent);
                    
                    throw new HttpRequestException(
                        $"External API returned status code: {response.StatusCode}. Details: {errorContent}",
                        null,
                        response.StatusCode);
                }

                var content = await response.Content.ReadAsStringAsync(cancellationToken);
                var newsApiResponse = JsonSerializer.Deserialize<NewsArticleReponse>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (newsApiResponse == null || newsApiResponse.Status != "ok")
                {
                    throw new InvalidOperationException(
                        "Failed to deserialize news data or API returned error status");
                }

                // Map articles to response DTOs
                var mappedArticles = newsApiResponse.Articles?
                    .Select(NewsMapper.MapToResponseDto)
                    .ToList() ?? new List<NewsArticleResponseDto>();

                var result = new NewsResponseDto
                {
                    Page = request.Page,
                    PageSize = 20, // Fixed page size as per requirements
                    TotalResults = newsApiResponse.TotalResults,
                    Items = mappedArticles
                };

                // Store in cache with expiration
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(CacheExpirationSeconds),
                    SlidingExpiration = null // Use absolute expiration only
                };

                _memoryCache.Set(cacheKey, result, cacheOptions);
                _logger.LogInformation("Cached result for key: {CacheKey} with TTL: {TTL} seconds", cacheKey, CacheExpirationSeconds);

                return result;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error calling News API");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in GetNewsQueryHandler");
                throw;
            }
        }
    }
}

