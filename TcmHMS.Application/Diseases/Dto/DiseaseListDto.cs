using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcmHMS.Departments.Dto;
using TcmHMS.Entities;

namespace TcmHMS.Diseases.Dto
{
    [AutoMapFrom(typeof(Disease))]
    public class DiseaseListDto : EntityDto<int>, IHasCreationTime
    {
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 发病症状
        /// </summary>
        public string Symptom { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        public string Pinyin { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get; set; }

        [AutoMapFrom(typeof(Department))]
        public DepartmentListDto Department { get; set; }
    }
}
