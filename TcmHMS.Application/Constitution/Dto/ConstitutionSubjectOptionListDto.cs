using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using TcmHMS.Entities.Constitution;

namespace TcmHMS.Constitution.Dto
{
    [AutoMapFrom(typeof(ConstitutionSubjectOption))]
    public class ConstitutionSubjectOptionListDto : EntityDto
    {
        public string DisplayName { get; set; }

        public int DisplayOrder { get; set; }

        public int Score { get; set; }
    }
}
