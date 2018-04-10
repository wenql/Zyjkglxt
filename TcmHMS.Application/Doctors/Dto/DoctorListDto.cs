using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using TcmHMS.Entities;

namespace TcmHMS.Doctors.Dto
{
    [AutoMapFrom(typeof(Doctor))]
    public class DoctorListDto : EntityDto<int>, IHasCreationTime
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNumber { get; set; }



        public DateTime CreationTime { get; set; }
    }
}
