using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalAssessmentApplication.DTOs
{
    public class NewsArticleReponse
    {
        public string Status { get; set; }
        public int TotalResults { get; set; }
        public List<NewsArticleDto> Articles { get; set; }
    }
}
