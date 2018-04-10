using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using TcmHMS.Entities;

namespace TcmHMS.Doctors.Dto
{
    [AutoMapFrom(typeof(Doctor))]
    public class DoctorEditDto
    {
        public int? Id { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [DisplayName("手机号")]
        [Required(ErrorMessage = "请输入{1}")]
        [RegularExpression(@"^1[3458][0-9]{9}$", ErrorMessage = "{0}格式不正确")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [DisplayName("密码")]
        [Required]
        [StringLength(Doctor.MaxPasswordLength, MinimumLength = 6, ErrorMessage = "{0}的长度在{2}至{1}个字符间")]
        public string Password { get; set; }
    }
}
