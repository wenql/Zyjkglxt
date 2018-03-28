using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TcmHMS.Medicines.Dto;

namespace TcmHMS.Medicines
{
    public interface IMedicineAppService : IApplicationService
    {
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        Task<ListResultDto<MedicineListDto>> GetMedicines(GetMedicinesInput input);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<MedicineEditDto> GetMedicineForEdit(NullableIdDto input);

        /// <summary>
        /// 保存实体数据
        /// </summary>
        /// <param name="medicine"></param>
        /// <returns></returns>
        Task CreateOrUpdateMedicine(MedicineEditDto medicine);

        /// <summary>
        /// 删除科室
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteMedicine(EntityDto input);
    }
}
