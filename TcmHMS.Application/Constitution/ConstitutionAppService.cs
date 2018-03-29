using Abp.Application.Services.Dto;
using Abp.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TcmHMS.Authorization;
using TcmHMS.Constitution.Dto;
using TcmHMS.Entities.Constitution;

namespace TcmHMS.Constitution
{
    [AbpAuthorize(PermissionNames.Pages_Constitutions)]
    public class ConstitutionAppService : TcmHMSAppServiceBase, IConstitutionAppService
    {
        public async Task<ListResultDto<ConstitutionGroupListDto>> GetConstitutionGroups()
        {
            var selectList = new List<ConstitutionGroupListDto>();
            var arrays = Enum.GetValues(typeof(ConstitutionGroup));
            for (var i = 0; i < arrays.LongLength; i++)
            {
                selectList.Add(new ConstitutionGroupListDto
                {
                    GroupId = Convert.ToInt32((ConstitutionGroup)arrays.GetValue(i))
                });
            }
            return new ListResultDto<ConstitutionGroupListDto>(selectList);
        }
    }
}
