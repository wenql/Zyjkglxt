using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using TcmHMS.Authorization;
using TcmHMS.Departments.Dto;
using TcmHMS.Diseases.Dto;
using TcmHMS.Entities;

namespace TcmHMS.Diseases
{
    public class DiseaseAppService : TcmHMSAppServiceBase, IDiseaseAppService
    {
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<Disease> _diseaseRepository;

        public DiseaseAppService(IRepository<Department> departmentRepository, IRepository<Disease> diseaseRepository)
        {
            this._departmentRepository = departmentRepository;
            this._diseaseRepository = diseaseRepository;
        }

        [AbpAuthorize(PermissionNames.Pages_Dictionaries_Diseases_Delete)]
        public async Task DeleteDisease(EntityDto input)
        {
            var disease = await _diseaseRepository.GetAsync(input.Id);
            if (disease == null)
                throw new UserFriendlyException("记录不存在");

            await this._diseaseRepository.DeleteAsync(disease);

        }

        [AbpAuthorize(PermissionNames.Pages_Dictionaries_Diseases_Create, PermissionNames.Pages_Dictionaries_Diseases_Edit)]
        public async Task createOrUpdateDisease(DiseaseEditDto disease)
        {
            if (!CheckNameError(disease.DisplayName, disease.Id))
            {
                throw new UserFriendlyException("名称已存在");
            }
            var model = new Disease();
            if (disease.Id.HasValue)
            {
                model = await this._diseaseRepository.GetAsync(disease.Id.Value);
            }
            model.DepartmentId = disease.DepartmentId;
            model.DisplayName = disease.DisplayName;
            model.Pinyin = disease.Pinyin;
            model.IsEnabled = disease.IsEnabled;
            model.Symptom = disease.Symptom;
            model.Description = disease.Description;
            await this._diseaseRepository.InsertOrUpdateAsync(model);

        }

        private bool CheckNameError(string name, int? id)
        {
            return !this._departmentRepository.GetAll().WhereIf(id.HasValue, x => x.Id != id).Any(x => x.DisplayName == name);
        }

        public async Task<DiseaseEditDto> GetDiseaseForEdit(NullableIdDto input)
        {
            if (input.Id.HasValue)
            {
                var disease = await this._diseaseRepository.GetAsync(input.Id.Value);
                return disease.MapTo<DiseaseEditDto>();
            }
            var defaultDept = await this._departmentRepository.GetAll().FirstOrDefaultAsync();
            if (defaultDept == null)
            {
                throw new UserFriendlyException("请先添加科室");
            }
            return new DiseaseEditDto()
            {
                DepartmentId = defaultDept.Id
            };
        }

        public async Task<ListResultDto<DiseaseListDto>> GetDiseases(GetDiseasesInput input)
        {
            var query = this._diseaseRepository.GetAllIncluding(t => t.Department)
                       .WhereIf(input.Department.HasValue, u => u.Department.Id == input.Department.Value)
                       .WhereIf(
                           !input.Keyword.IsNullOrWhiteSpace(),
                           u =>
                               u.DisplayName.Contains(input.Keyword) ||
                               u.Pinyin.Contains(input.Keyword) ||
                               u.Description.Contains(input.Keyword) ||
                               u.Symptom.Contains(input.Keyword)
                       );

            var diseaseCount = await query.CountAsync();
            var users = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<DiseaseListDto>(
                diseaseCount,
                users.MapTo<List<DiseaseListDto>>()
                );
        }
    }
}
