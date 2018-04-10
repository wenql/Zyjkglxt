using System.ComponentModel;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;
using TcmHMS.Entities.Constitution;

namespace TcmHMS.Constitution.Dto
{
    [AutoMapFrom(typeof(ConstitutionSuggest))]
    public class ConstitutionSuggestEditDto
    {
        public int? Id { get; set; }
        public int GroupId { get; set; }

        /// <summary>
        /// 总体特征
        /// </summary>
        [StringLength(ConstitutionSuggest.MaxLength)]
        [DisplayName("总体特征")]
        public string GeneralFeature { get; set; }

        /// <summary>
        /// 形体特征
        /// </summary>
        [StringLength(ConstitutionSuggest.MaxLength)]
        [DisplayName("形体特征")]
        public string ShapeFeature { get; set; }

        /// <summary>
        /// 常见表现
        /// </summary>
        [StringLength(ConstitutionSuggest.MaxLength)]
        [DisplayName("常见表现")]
        public string CommonPerformance { get; set; }

        /// <summary>
        /// 心理特征
        /// </summary>
        [StringLength(ConstitutionSuggest.MaxLength)]
        [DisplayName("心理特征")]
        public string PsychologyFeature { get; set; }

        /// <summary>
        /// 发病倾向
        /// </summary>
        [StringLength(ConstitutionSuggest.MaxLength)]
        [DisplayName("发病倾向")]
        public string DiseaseTendency { get; set; }

        /// <summary>
        /// 对外界环境适应能力
        /// </summary>
        [StringLength(ConstitutionSuggest.MaxLength)]
        [DisplayName("对外界环境适应能力")]
        public string Adaptability { get; set; }

        /// <summary>
        /// 运动调养
        /// </summary>
        [StringLength(ConstitutionSuggest.MaxLength)]
        [DisplayName("运动调养")]
        public string ExerciseNurse { get; set; }

        /// <summary>
        /// 药物调养
        /// </summary>
        [StringLength(ConstitutionSuggest.MaxLength)]
        [DisplayName("药物调养")]
        public string MedicineNurse { get; set; }

        /// <summary>
        /// 调养方法
        /// </summary>
        [StringLength(ConstitutionSuggest.MaxLength)]
        [DisplayName("调养方法")]
        public string NurseMethod { get; set; }

        /// <summary>
        /// 健康食谱
        /// </summary>
        [StringLength(ConstitutionSuggest.MaxLength)]
        [DisplayName("健康食谱")]
        public string HealthyRecipes { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault { get; set; }

        public ConstitutionGroupListDto Group => new ConstitutionGroupListDto { GroupId = this.GroupId };
    }
}
