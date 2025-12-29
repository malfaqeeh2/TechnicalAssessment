using System.Text.Json.Serialization;

namespace TechnicalAssessmentApplication.DTOs
{
    public class NewsResponseDto
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        [JsonPropertyName("totalResults")]
        public int TotalResults { get; set; }

        [JsonPropertyName("items")]
        public List<NewsArticleResponseDto> Items { get; set; } = new();
    }
}


