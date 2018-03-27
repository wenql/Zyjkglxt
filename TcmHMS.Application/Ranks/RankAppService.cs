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
using TcmHMS.Authorization;
using TcmHMS.Entities;
using TcmHMS.Ranks.Dto;

namespace TcmHMS.Ranks
{
    [AbpAuthorize(PermissionNames.Pages_Dictionaries_Ranks)]
    public class RankAppService : TcmHMSAppServiceBase, IRankAppService
    {
        private readonly IRepository<Rank> _rankRepository;
        public RankAppService(IRepository<Rank> rankRepository)
        {
            this._rankRepository = rankRepository;
        }

        [AbpAuthorize(PermissionNames.Pages_Dictionaries_Ranks_Delete)]
        public async Task DeleteRank(EntityDto input)
        {
            var rank = await _rankRepository.GetAsync(input.Id);
            if (rank == null)
                throw new UserFriendlyException("记录不存在");

            await this._rankRepository.DeleteAsync(rank);

        }

        [AbpAuthorize(PermissionNames.Pages_Dictionaries_Ranks_Create, PermissionNames.Pages_Dictionaries_Ranks_Edit)]
        public async Task createOrUpdateRank(RankEditDto rank)
        {
            if (!CheckNameError(rank.DisplayName, rank.Id))
            {
                throw new UserFriendlyException("名称已存在");
            }
            var model = new Rank();
            if (rank.Id.HasValue)
            {
                model = await this._rankRepository.GetAsync(rank.Id.Value);
            }
            model.DisplayName = rank.DisplayName;
            model.Pinyin = rank.Pinyin;
            model.DisplayOrder = rank.DisplayOrder;
            await this._rankRepository.InsertOrUpdateAsync(model);

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
