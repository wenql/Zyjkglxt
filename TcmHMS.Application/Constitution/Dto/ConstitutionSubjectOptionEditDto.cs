using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using TcmHMS.Entities.Constitution;

namespace TcmHMS.Constitution.Dto
{
    [AutoMap(typeof(ConstitutionSubjectOption))]
    public class ConstitutionSubjectOptionEditDto
    {
        public int? id { get; set; }

        [Required]
        public int SubjectId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(ConstitutionSubjectOption.MaxNameLength)]
        public string DisplayName { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 分数
        /// </summary>
        public int Score { get; set; }
    }
}
