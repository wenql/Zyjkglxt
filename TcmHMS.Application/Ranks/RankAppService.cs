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
using System.Threading.Tasks;
using Abp.ObjectMapping;
using TcmHMS.Authorization;
using TcmHMS.Entities;
using TcmHMS.Ranks.Dto;

namespace TcmHMS.Ranks
{
    [AbpAuthorize(PermissionNames.Pages_Dictionaries_Ranks)]
    public class RankAppService : TcmHMSAppServiceBase, IRankAppService
    {
        private readonly IRepository<Rank> _rankRepository;
        private readonly IObjectMapper _objectMapper;
        public RankAppService(IRepository<Rank> rankRepository, IObjectMapper objectMapper)
        {
            this._rankRepository = rankRepository;
            this._objectMapper = objectMapper;
        }

        [AbpAuthorize(PermissionNames.Pages_Dictionaries_Ranks_Delete)]
        public async Task DeleteRank(EntityDto input)
        {
            var rank = await _rankRepository.GetAsync(input.Id);
            if (rank == null)
                throw new UserFriendlyException("记录不存在");

            await this._rankRepository.DeleteAsync(rank);

        }

        public async Task UpdateSortable(List<int> rankIds)
        {
            if (!rankIds.Any())
                throw new UserFriendlyException("记录不存在");

            for (int i = 0; i < rankIds.Count; i++)
            {
                var rank = await this._rankRepository.GetAsync(rankIds[i]);
                rank.DisplayOrder = i;
                await this._rankRepository.UpdateAsync(rank);
            }
        }

        [AbpAuthorize(PermissionNames.Pages_Dictionaries_Ranks_Create, PermissionNames.Pages_Dictionaries_Ranks_Edit)]
        public async Task CreateOrUpdateRank(RankEditDto rank)
        {
            if (!CheckNameError(rank.DisplayName, rank.Id))
            {
                throw new UserFriendlyException("名称已存在");
            }
            await this._rankRepository.InsertOrUpdateAsync(_objectMapper.Map(rank,
                rank.Id.HasValue ? await this._rankRepository.GetAsync(rank.Id.Value) : new Rank()));
        }

        private bool CheckNameError(string name, int? id)
        {
            return !this._rankRepository.GetAll().WhereIf(id.HasValue, x => x.Id != id).Any(x => x.DisplayName == name);
        }

        public async Task<RankEditDto> GetRankForEdit(NullableIdDto input)
        {
            if (input.Id.HasValue)
            {
                var rank = await this._rankRepository.GetAsync(input.Id.Value);
                return rank.MapTo<RankEditDto>();
            }
            return new RankEditDto();
        }

        public async Task<ListResultDto<RankListDto>> GetRanks(GetRanksInput input)
        {
            var ranks = await _rankRepository.GetAll().WhereIf(
                    !input.Keyword.IsNullOrWhiteSpace(),
                    x =>
                        x.DisplayName.Contains(input.Keyword) ||
                        x.Pinyin.Contains(input.Keyword)
                    ).OrderBy(x => x.DisplayOrder).ThenBy(x => x.Id).ToListAsync();

            return new ListResultDto<RankListDto>(ranks.MapTo<List<RankListDto>>());
        }
    }
}
