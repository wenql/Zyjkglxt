using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using TcmHMS.Entities.Constitution;

namespace TcmHMS.Constitution.Dto
{
    [AutoMapFrom(typeof(ConstitutionSuggest))]
    public class ConstitutionSuggestListDto : EntityDto, IHasCreationTime
    {
        public int GroupId { get; set; }

        public string GeneralFeature { get; set; }

        public string ShapeFeature { get; set; }

        public string CommonPerformance { get; set; }

        public string PsychologyFeature { get; set; }

        public string DiseaseTendency { get; set; }

        public string Adaptability { get; set; }

        public string ExerciseNurse { get; set; }

        public string MedicineNurse { get; set; }

        public string NurseMethod { get; set; }

        public string HealthyRecipes { get; set; }

        public bool IsDefault { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
