using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System.Threading.Tasks;
using TcmHMS.Departments.Dto;

namespace TcmHMS.Departments
{
    public interface IDepartmentAppService : IApplicationService
    {

        /// <summary>
        /// 获取所有科室
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        Task<ListResultDto<DepartmentListDto>> GetDepartments(GetDepartmentsInput input);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<DepartmentEditDto> GetDepartmentForEdit(NullableIdDto input);

        /// <summary>
        /// 保存实体数据
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        Task createOrUpdateDepartment(DepartmentEditDto department);

        /// <summary>
        /// 删除科室
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteDepartment(EntityDto input);
    }
}
