using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalAssessmentApplication.DTOs
{
    public class ValidateInputDto
    {
        public string Q { get; set; }
        public string ApiKey { get; set; }
        public int Page { get; set; }
        public ValidateOutputDto ValidateOutputDto { get; set; }
    
    }
    public class ValidateOutputDto
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }

    }
}
