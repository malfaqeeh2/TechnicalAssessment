using System.Text.Json.Serialization;

namespace TechnicalAssessmentApplication.DTOs
{
    public class NewsArticleResponseDto
    {
        [JsonPropertyName("source")]
        public string Source { get; set; } = string.Empty;

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("summary")]
        public string Summary { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        [JsonPropertyName("image")]
        public string? Image { get; set; }

        [JsonPropertyName("publishedAt")]
        public string PublishedAt { get; set; } = string.Empty;
    }
}


