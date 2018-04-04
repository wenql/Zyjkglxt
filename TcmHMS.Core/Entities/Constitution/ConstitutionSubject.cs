using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TcmHMS.Entities.Constitution
{
    /// <summary>
    /// 体质问卷
    /// </summary>
    public class ConstitutionSubject : Entity, IHasCreationTime
    {
        public const int MaxTitleLength = 200;

        /// <summary>
        /// 体质分组
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [StringLength(MaxTitleLength)]
        public string Title { get; set; }

        /// <summary>
        /// 性别限定
        /// </summary>
        public int SpecifyGebder { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreationTime { get; set; }

        public virtual ICollection<ConstitutionSubjectOption> Options { get; set; }

        public ConstitutionSubject()
        {
            this.CreationTime = DateTime.Now;
        }
    }
}
