using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;
using TcmHMS.Entities;

namespace TcmHMS.Ranks.Dto
{
    [AutoMapFrom(typeof(Rank))]
    public class RankListDto : EntityDto, IHasCreationTime
    {
        public string DisplayName { get; set; }

        public string Pinyin { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int DisplayOrder { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
