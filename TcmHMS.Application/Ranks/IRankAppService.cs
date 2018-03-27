using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TcmHMS.Ranks.Dto;

namespace TcmHMS.Ranks
{
    public interface IRankAppService : IApplicationService
    {
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        Task<ListResultDto<RankListDto>> GetRanks(GetRanksInput input);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<RankEditDto> GetRankForEdit(NullableIdDto input);

        /// <summary>
        /// 保存实体数据
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        Task createOrUpdateRank(RankEditDto department);

        /// <summary>
        /// 删除科室
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteRank(EntityDto input);
    }
}
