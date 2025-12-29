using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalAssessmentApplication.DTOs
{
    public class ResponseDto
    {
        public int Page { get; set; }
        public int pageSize { get; set; }
        public NewsArticleDto Items { get; set; }
        public int TotalResults { get; set; }
        public string Code { get; set; }
        public string ExternalApiError { get; set; }
        public string Message { get; set; }

    }
}
