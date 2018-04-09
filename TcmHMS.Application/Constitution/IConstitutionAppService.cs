using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TcmHMS.Constitution.Dto;

namespace TcmHMS.Constitution
{
    public interface IConstitutionAppService : IApplicationService
    {
        /// <summary>
        /// 获取体质分类
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<ConstitutionGroupListDto>> GetConstitutionGroups();

        /// <summary>
        /// 获取健康建议
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ConstitutionSuggestEditDto> GetConstitutionSuggestForEdit(NullableIdDto input);

        /// <summary>
        /// 保存健康建议
        /// </summary>
        /// <param name="suggest"></param>
        /// <returns></returns>
        Task CreateOrUpdateConstitutionSuggest(ConstitutionSuggestEditDto suggest);

        /// <summary>
        /// 获取体质问卷
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ListResultDto<ConstitutionSubjectListDto>> GetConstitutionSubjects(GetConstitutionSubjectsInput input);

        /// <summary>
        /// 获取体质问卷
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ConstitutionSubjectEditDto> GetConstitutionSubjectForEdit(NullableIdDto input);

        /// <summary>
        /// 保存体质问卷
        /// </summary>
        /// <param name="disease"></param>
        /// <returns></returns>
        Task CreateOrUpdateConstitutionSubject(ConstitutionSubjectEditDto subject);

        /// <summary>
        /// 删除体质问卷
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteConstitutionSubject(EntityDto input);
    }
}
