using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using TcmHMS.Entities.Constitution;

namespace TcmHMS.Constitution.Dto
{
    [AutoMap(typeof(ConstitutionSubject))]
    public class ConstitutionSubjectEditDto
    {
        public int? Id { get; set; }

        [Required]
        public int GroupId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [StringLength(ConstitutionSubject.MaxTitleLength)]
        public string Title { get; set; }

        /// <summary>
        /// 性别限定
        /// </summary>
        public int SpecifyGebder { get; set; }
    }
}
