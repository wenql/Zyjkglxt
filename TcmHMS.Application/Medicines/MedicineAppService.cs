using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.ObjectMapping;
using Abp.UI;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using TcmHMS.Authorization;
using TcmHMS.Entities;
using TcmHMS.Medicines.Dto;

namespace TcmHMS.Medicines
{
    [AbpAuthorize(PermissionNames.Pages_Dictionaries_Medicines)]
    public class MedicineAppService : TcmHMSAppServiceBase, IMedicineAppService
    {
        private readonly IRepository<Medicine> _medicineRepository;
        private readonly IObjectMapper _iObjectMapper;

        public MedicineAppService(IRepository<Medicine> medicineRepository, IObjectMapper iObjectMapper)
        {
            this._medicineRepository = medicineRepository;
            this._iObjectMapper = iObjectMapper;
        }

        [AbpAuthorize(PermissionNames.Pages_Dictionaries_Medicines_Delete)]
        public async Task DeleteMedicine(EntityDto input)
        {
            var medicine = await _medicineRepository.GetAsync(input.Id);
            if (medicine == null)
                throw new UserFriendlyException("记录不存在");

            await this._medicineRepository.DeleteAsync(medicine);

        }

        [AbpAuthorize(PermissionNames.Pages_Dictionaries_Medicines_Create, PermissionNames.Pages_Dictionaries_Medicines_Edit)]
        public async Task CreateOrUpdateMedicine(MedicineEditDto medicine)
        {
            if (!CheckNameError(medicine.DisplayName, medicine.Id))
            {
                throw new UserFriendlyException("名称已存在");
            }
            if (!CheckIdentifierError(medicine.Identifier, medicine.Id))
            {
                throw new UserFriendlyException("编号已存在");
            }
            await this._medicineRepository.InsertOrUpdateAsync(this._iObjectMapper.Map(medicine,
                medicine.Id.HasValue ? await this._medicineRepository.GetAsync(medicine.Id.Value) : new Medicine()));

        }

        private bool CheckNameError(string name, int? id)
        {
            return !this._medicineRepository.GetAll().WhereIf(id.HasValue, x => x.Id != id).Any(x => x.DisplayName == name);
        }

        private bool CheckIdentifierError(string identifier, int? id)
        {
            return !this._medicineRepository.GetAll().WhereIf(id.HasValue, x => x.Id != id).Any(x => x.Identifier == identifier);
        }

        public async Task<MedicineEditDto> GetMedicineForEdit(NullableIdDto input)
        {
            if (input.Id.HasValue)
            {
                var medicine = await this._medicineRepository.GetAsync(input.Id.Value);
                return medicine.MapTo<MedicineEditDto>();
            }
            return new MedicineEditDto();
        }

        public async Task<ListResultDto<MedicineListDto>> GetMedicines(GetMedicinesInput input)
        {
            var query = this._medicineRepository.GetAll()
                .WhereIf(
                    !input.Keyword.IsNullOrWhiteSpace(),
                    u =>
                        u.DisplayName.Contains(input.Keyword) ||
                        u.Pinyin.Contains(input.Keyword) ||
                        u.Function.Contains(input.Keyword) ||
                        u.Identifier.Contains(input.Keyword)
                );

            var diseaseCount = await query.CountAsync();
            var users = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<MedicineListDto>(
                diseaseCount,
                users.MapTo<List<MedicineListDto>>()
            );
        }
    }
}
