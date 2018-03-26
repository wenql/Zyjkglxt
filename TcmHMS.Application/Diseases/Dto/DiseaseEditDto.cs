using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using TcmHMS.Entities;

namespace TcmHMS.Diseases.Dto
{
    [AutoMap(typeof(Disease))]
    public class DiseaseEditDto
    {
        public int? Id { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(Disease.MaxNameOrPinyinLength)]
        public string DisplayName { get; set; }

        [Required]
        [StringLength(Disease.MaxNameOrPinyinLength)]
        public string Pinyin { get; set; }

        public bool IsEnabled { get; set; }

        [MaxLength(Department.MaxDescriptionLength)]
        public string Symptom { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(Department.MaxDescriptionLength)]
        public string Description { get; set; }

        public DiseaseEditDto()
        {
            IsEnabled = true;
            Symptom = "";
            Description = "";
        }

    }
}
