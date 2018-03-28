using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System.Threading.Tasks;
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
        /// <param name="rank"></param>
        /// <returns></returns>
        Task CreateOrUpdateRank(RankEditDto rank);

        /// <summary>
        /// 删除科室
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteRank(EntityDto input);

        /// <summary>
        /// 更新排序
        /// </summary>
        /// <param name="rankIds"></param>
        /// <returns></returns>
        Task UpdateSortable(List<int> rankIds);
    }
}
