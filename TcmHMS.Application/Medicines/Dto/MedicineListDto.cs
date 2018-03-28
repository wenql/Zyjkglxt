using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;
using TcmHMS.Entities;

namespace TcmHMS.Medicines.Dto
{
    [AutoMapFrom(typeof(Medicine))]
    public class MedicineListDto : EntityDto, IHasCreationTime
    {
        public string Identifier { get; set; }

        public string DisplayName { get; set; }

        public string Pinyin { get; set; }

        public string Source { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
