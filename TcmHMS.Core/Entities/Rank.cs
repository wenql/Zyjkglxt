using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

namespace TcmHMS.Entities
{
    public class Rank : Entity, IHasCreationTime
    {
        public const int MaxNameLength = 32;

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(MaxNameLength)]
        public string DisplayName { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        [Required]
        [StringLength(MaxNameLength)]
        public string Pinyin { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>

        public DateTime CreationTime { get; set; }
    }
}
