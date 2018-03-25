using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcmHMS.Departments.Dto;
using TcmHMS.Diseases.Dto;

namespace TcmHMS.Diseases
{
    public interface IDiseaseAppService : IApplicationService
    {
        /// <summary>
        /// 获取病症
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ListResultDto<DiseaseListDto>> GetDiseases(GetDiseasesInput input);
    }
}
