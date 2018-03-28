using Abp.Application.Services;
using Abp.Application.Services.Dto;
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

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<DiseaseEditDto> GetDiseaseForEdit(NullableIdDto input);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="disease"></param>
        /// <returns></returns>
        Task CreateOrUpdateDisease(DiseaseEditDto disease);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteDisease(EntityDto input);
    }
}
