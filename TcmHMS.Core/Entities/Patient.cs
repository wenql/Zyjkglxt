using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

namespace TcmHMS.Entities
{
    /// <summary>
    /// 病人
    /// </summary>
    public class Patient : Entity, IHasCreationTime, IHasDeletionTime, IHasModificationTime
    {
        public const int MaxUserNameLength = 32;
        public const int MaxPasswordLength = 128;

        /// <summary>
        /// 微信授权码
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Required]
        [StringLength(MaxUserNameLength)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [StringLength(MaxPasswordLength)]
        public string Password { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string HeadImg { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(MaxPasswordLength)]
        public string NickName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 婚姻
        /// </summary>
        public int Married { get; set; }

        /// <summary>
        /// 职业
        /// </summary>
        public string Profession { get; set; }

        /// <summary>
        /// 病史
        /// </summary>
        public string IllnessHistory { get; set; }

        /// <summary>
        /// 有无药物过敏
        /// </summary>
        public string HasAllergy { get; set; }

        /// <summary>
        /// 有无遗传病史
        /// </summary>
        public string HasHeredity { get; set; }

        /// <summary>
        /// 血型
        /// </summary>
        public string BloodTypeId { get; set; }

        /// <summary>
        /// 最后登录日期
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActice { get; set; }

        /// <summary>
        /// 已删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 删除日期
        /// </summary>

        public DateTime? DeletionTime { get; set; }

        /// <summary>
        /// 最后修改日期
        /// </summary>
        public DateTime? LastModificationTime { get; set; }
    }
}
