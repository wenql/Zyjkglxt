using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
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
