using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

namespace TcmHMS.Entities
{
    /// <summary>
    /// 药品管理
    /// </summary>
    public class Medicine : Entity, IHasCreationTime
    {
        public const int MaxNameOrPinyinLength = 32;

        /// <summary>
        /// 药品编号
        /// </summary>
        [Required]
        [StringLength(MaxNameOrPinyinLength)]
        public string Identifier { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        [Required]
        [StringLength(MaxNameOrPinyinLength)]
        public string DisplayName { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        [Required]
        [StringLength(MaxNameOrPinyinLength)]
        public string Pinyin { get; set; }

        /// <summary>
        /// 包装规格
        /// </summary>
        public string PackagingSpec { get; set; }

        /// <summary>
        /// 主治功能
        /// </summary>
        public string Function { get; set; }

        /// <summary>
        /// 用法用量
        /// </summary>
        public string Dosage { get; set; }

        /// <summary>
        /// 药品来源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
