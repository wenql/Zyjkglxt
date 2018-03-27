using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;
using TcmHMS.Entities;

namespace TcmHMS.Departments.Dto
{
    [AutoMapFrom(typeof(Department))]
    public class DepartmentListDto : EntityDto, IHasCreationTime
    {
        public string DisplayName { get; set; }

        public string Code { get; set; }

        public bool IsEnabled { get; set; }

        public string Description { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
