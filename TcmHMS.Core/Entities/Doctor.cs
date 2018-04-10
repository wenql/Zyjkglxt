using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

namespace TcmHMS.Entities
{
    /// <summary>
    /// 医生
    /// </summary>
    public class Doctor : Entity, IHasCreationTime, IHasDeletionTime, IHasModificationTime
    {
        public const int MaxUserNameLength = 32;
        public const int MaxPasswordLength = 128;

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
        /// 医院
        /// </summary>
        public string Hospital { get; set; }

        /// <summary>
        /// 职称
        /// </summary>
        public int RankId { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public int DepartmentId { get; set; }

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
        /// 擅长
        /// </summary>
        public string GoodAt { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string Introduction { get; set; }

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

        /// <summary>
        /// 职称
        /// </summary>
        public virtual Rank Rank { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public virtual Department Department { get; set; }
    }
}
