using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

namespace TcmHMS.Entities
{
    /// <summary>
    /// 疾病
    /// </summary>
    public class Disease : Entity, IHasCreationTime
    {
        public const int MaxDescriptionLength = 5000;
        public const int MaxNameOrPinyinLength = 32;

        /// <summary>
        /// 部门Id
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(MaxNameOrPinyinLength)]
        public string DisplayName { get; set; }

        /// <summary>
        /// 发病症状
        /// </summary>
        [Required]
        [StringLength(MaxDescriptionLength)]
        public string Symptom { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        [Required]
        [StringLength(MaxNameOrPinyinLength)]
        public string Pinyin { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 所属科室
        /// </summary>
        public virtual Department Department { get; set; }

        public Disease()
        {
            this.CreationTime = DateTime.Now;
        }
    }
}
