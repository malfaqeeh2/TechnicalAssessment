using System.Text.Json.Serialization;

namespace TechnicalAssessmentApplication.DTOs
{
    public class ErrorResponseDto
    {
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("details")]
        public string? Details { get; set; }
    }
}


