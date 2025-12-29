using MediatR;
using TechnicalAssessmentApplication.DTOs;

namespace TechnicalAssessmentApplication.Queries
{
    public class GetNewsQuery : IRequest<NewsResponseDto>
    {
        public int Page { get; set; } = 1;
    }
}


