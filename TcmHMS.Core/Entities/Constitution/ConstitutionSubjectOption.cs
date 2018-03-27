using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;

namespace TcmHMS.Entities.Constitution
{
    /// <summary>
    /// 问卷选项
    /// </summary>
    public class ConstitutionSubjectOption : Entity
    {
        public const int MaxNameLength = 50;

        /// <summary>
        /// 题目Id
        /// </summary>
        public int SubjectId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(MaxNameLength)]
        public string DisplayName { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 分数
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// 题目
        /// </summary>
        public virtual ConstitutionSubject Subject { get; set; }
    }
}
