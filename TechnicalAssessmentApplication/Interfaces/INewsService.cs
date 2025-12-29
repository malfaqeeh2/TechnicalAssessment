using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalAssessmentApplication.DTOs;
using TechnicalAssessmentDomain.Entites;

namespace TechnicalAssessmentDomain.Interfaces
{
    public interface INewsService
    {
        Task<ResponseDto> GetUaeNewsAsync(string q, int page = 1);

    }
}
