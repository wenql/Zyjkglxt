using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.ObjectMapping;
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
        private readonly IObjectMapper _objectMapper;

        public DiseaseAppService(IRepository<Department> departmentRepository, IRepository<Disease> diseaseRepository,
            IObjectMapper objectMapper)
        {
            this._departmentRepository = departmentRepository;
            this._diseaseRepository = diseaseRepository;
            this._objectMapper = objectMapper;
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
        public async Task CreateOrUpdateDisease(DiseaseEditDto disease)
        {
            if (!CheckNameError(disease.DisplayName, disease.Id))
            {
                throw new UserFriendlyException("名称已存在");
            }
            await this._diseaseRepository.InsertOrUpdateAsync(this._objectMapper.Map(disease, disease.Id.HasValue
                    ? await this._diseaseRepository.GetAsync(disease.Id.Value)
                    : new Disease()));

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
