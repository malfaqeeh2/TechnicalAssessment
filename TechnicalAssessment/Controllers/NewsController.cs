using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechnicalAssessmentApplication.DTOs;
using TechnicalAssessmentApplication.Queries;

namespace TechnicalAssessment.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<NewsController> _logger;

        public NewsController(IMediator mediator, ILogger<NewsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(NewsResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] int page = 1)
        {
            // Validate page parameter
            if (page < 1)
            {
                return BadRequest(new ErrorResponseDto
                {
                    Code = "InvalidPage",
                    Message = "Page must be greater than or equal to 1",
                    Details = $"Provided page value: {page}"
                });
            }

            try
            {
                var query = new GetNewsQuery { Page = page };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error calling external News API");
                
                // Get status code from exception if available
                var statusCode = ex.StatusCode?.ToString() ?? "Unknown";
                
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ErrorResponseDto
                    {
                        Code = "ExternalApiError",
                        Message = "Unable to fetch news",
                        Details = statusCode
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in NewsController");
                
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ErrorResponseDto
                    {
                        Code = "InternalServerError",
                        Message = "An unexpected error occurred",
                        Details = null // Don't expose stack traces
                    });
            }
        }
    }
}
