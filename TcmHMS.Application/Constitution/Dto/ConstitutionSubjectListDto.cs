using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using Abp.AutoMapper;
using TcmHMS.Entities.Constitution;

namespace TcmHMS.Constitution.Dto
{
    [AutoMapFrom(typeof(ConstitutionSubject))]
    public class ConstitutionSubjectListDto : EntityDto, IHasCreationTime
    {
        public int GroupId { get; set; }

        public string Title { get; set; }

        public int SpecifyGebder { get; set; }

        public DateTime CreationTime { get; set; }

        public ConstitutionGroupListDto Group => new ConstitutionGroupListDto { GroupId = this.GroupId };

        public List<ConstitutionSubjectOptionListDto> Options { get; set; }
    }
}
