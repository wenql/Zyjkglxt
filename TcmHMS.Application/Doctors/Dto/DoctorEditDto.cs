using Abp.AutoMapper;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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

        /// <summary>
        /// 头像
        /// </summary>
        [DisplayName("头像")]
        public string HeadImg { get; set; }

        /// <summary>
        /// 医院
        /// </summary>
        [DisplayName("医院")]
        [StringLength(100, ErrorMessage = "{0}不能超过{1}个字符")]
        public string Hospital { get; set; }

        /// <summary>
        /// 职称
        /// </summary>
        [DisplayName("职称")]
        [Required]
        public int RankId { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        [DisplayName("部门")]
        [Required]
        public int DepartmentId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [DisplayName("姓名")]
        [StringLength(10, ErrorMessage = "{0}不能超过{1}个字符")]
        public string NickName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [DisplayName("性别")]
        public int Gender { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        [DisplayName("年龄")]
        public int Age { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        [DisplayName("详细地址")]
        [StringLength(100, ErrorMessage = "{0}不能超过{1}个字符")]
        public string Address { get; set; }

        /// <summary>
        /// 擅长
        /// </summary>
        [DisplayName("擅长")]
        [StringLength(500, ErrorMessage = "{0}不能超过{1}个字符")]
        public string GoodAt { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        [DisplayName("简介")]
        public string Introduction { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActice { get; set; }
    }
}
