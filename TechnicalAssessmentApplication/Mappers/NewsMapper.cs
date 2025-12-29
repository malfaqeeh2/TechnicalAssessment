using TechnicalAssessmentApplication.DTOs;

namespace TechnicalAssessmentApplication.Mappers
{
    public static class NewsMapper
    {
        /// <summary>
        /// Maps from external API NewsArticleDto to the clean response DTO
        /// </summary>
        public static NewsArticleResponseDto MapToResponseDto(NewsArticleDto externalArticle)
        {
            return new NewsArticleResponseDto
            {
                Source = externalArticle.Source?.Name ?? string.Empty,
                Title = externalArticle.Title ?? string.Empty,
                Summary = externalArticle.Description ?? string.Empty,
                Url = externalArticle.Url ?? string.Empty,
                Image = externalArticle.UrlToImage,
                PublishedAt = externalArticle.PublishedAt ?? string.Empty
            };
        }
    }
}

