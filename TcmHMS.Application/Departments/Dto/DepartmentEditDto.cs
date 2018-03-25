using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using TcmHMS.Entities;

namespace TcmHMS.Departments.Dto
{
    [AutoMap(typeof(Department))]
    public class DepartmentEditDto
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(Department.MaxNameOrCodeLength)]
        public string DisplayName { get; set; }

        [Required]
        [StringLength(Department.MaxNameOrCodeLength)]
        public string Code { get; set; }

        public bool IsEnabled { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(Department.MaxDescriptionLength)]
        public string Description { get; set; }

        public DepartmentEditDto()
        {
            IsEnabled = true;
        }

    }
}
