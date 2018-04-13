using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using TcmHMS.Departments.Dto;
using TcmHMS.Entities;
using TcmHMS.Ranks.Dto;

namespace TcmHMS.Doctors.Dto
{
    [AutoMapFrom(typeof(Doctor))]
    public class DoctorListDto : EntityDto<int>, IHasCreationTime
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string HeadImg { get; set; }

        /// <summary>
        /// 医院
        /// </summary>
        public string Hospital { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 职称
        /// </summary>
        public RankListDto Rank { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public DepartmentListDto Department { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
