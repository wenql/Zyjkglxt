using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
