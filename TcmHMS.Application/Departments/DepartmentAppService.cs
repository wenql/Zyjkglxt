using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

using Abp.Domain.Repositories;
using Abp.Extensions;
using TcmHMS.Departments.Dto;
using TcmHMS.Entities;
using System.Data.Entity;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using Abp.UI;
using Abp.Authorization;
using TcmHMS.Authorization;

namespace TcmHMS.Departments
{
    [AbpAuthorize(PermissionNames.Pages_Dictionaries_Departments)]
    public class DepartmentAppService : TcmHMSAppServiceBase, IDepartmentAppService
    {
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<Disease> _diseaseRepository;

        public DepartmentAppService(IRepository<Department> departmentRepository, IRepository<Disease> diseaseRepository)
        {
            this._departmentRepository = departmentRepository;
            this._diseaseRepository = diseaseRepository;
        }

        [AbpAuthorize(PermissionNames.Pages_Dictionaries_Departments_Delete)]
        public async Task DeleteDepartment(EntityDto input)
        {
            var department = await _departmentRepository.GetAsync(input.Id);
            if (department == null)
                throw new UserFriendlyException("记录不存在");

            if (department.Disease.Any())
                throw new UserFriendlyException("该科室下还有病症未删除");

            await this._departmentRepository.DeleteAsync(department);

        }

        [AbpAuthorize(PermissionNames.Pages_Dictionaries_Departments_Create, PermissionNames.Pages_Dictionaries_Departments_Edit)]
        public async Task createOrUpdateDepartment(DepartmentEditDto department)
        {
            if (!CheckNameError(department.DisplayName, department.Id))
            {
                throw new UserFriendlyException("名称已存在");
            }
            if (!CheckCodeError(department.Code, department.Id))
            {
                throw new UserFriendlyException("代码已存在");
            }
            var model = new Department();
            if (department.Id.HasValue)
            {
                model = await this._departmentRepository.GetAsync(department.Id.Value);
            }
            model.DisplayName = department.DisplayName;
            model.Code = department.Code;
            model.IsEnabled = department.IsEnabled;
            model.Description = department.Description;
            await this._departmentRepository.InsertOrUpdateAsync(model);

        }

        private bool CheckNameError(string name, int? id)
        {
            return !this._departmentRepository.GetAll().WhereIf(id.HasValue, x => x.Id != id).Any(x => x.DisplayName == name);
        }

        private bool CheckCodeError(string code, int? id)
        {
            return !this._departmentRepository.GetAll().WhereIf(id.HasValue, x => x.Id != id).Any(x => x.Code == code);
        }

        public async Task<DepartmentEditDto> GetDepartmentForEdit(NullableIdDto input)
        {
            DepartmentEditDto departmentEditDto;

            if (input.Id.HasValue)
            {
                var department = await this._departmentRepository.GetAsync(input.Id.Value);
                return department.MapTo<DepartmentEditDto>();
            }
            return new DepartmentEditDto();
        }

        public async Task<ListResultDto<DepartmentListDto>> GetDepartments(GetDepartmentsInput input)
        {
            var departments = await _departmentRepository.GetAll().WhereIf(
                    !input.Keyword.IsNullOrWhiteSpace(),
                    x =>
                        x.DisplayName.Contains(input.Keyword) ||
                        x.Code.Contains(input.Keyword) ||
                        x.Description.Contains(input.Keyword)
                    ).OrderByDescending(x => x.Id).ToListAsync();

            return new ListResultDto<DepartmentListDto>(departments.MapTo<List<DepartmentListDto>>());
        }
    }
}
