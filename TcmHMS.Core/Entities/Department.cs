using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TcmHMS.Entities
{
    /// <summary>
    /// 科室
    /// </summary>
    public class Department : Entity, IHasCreationTime
    {
        public const int MaxDescriptionLength = 5000;
        public const int MaxNameOrCodeLength = 32;

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(MaxNameOrCodeLength)]
        public string DisplayName { get; set; }

        /// <summary>
        /// 代码
        /// </summary>
        [Required]
        [StringLength(MaxNameOrCodeLength)]
        public string Code { get; set; }

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

        private ICollection<Disease> _disease;

        /// <summary>
        /// Gets or sets the blog comments
        /// </summary>
        public virtual ICollection<Disease> Disease
        {
            get { return _disease ?? (_disease = new List<Disease>()); }
        }

        public Department()
        {
            this.CreationTime = DateTime.Now;
        }
    }
}
