using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

namespace TcmHMS.Entities.Constitution
{
    public class ConstitutionSuggest : Entity, IHasCreationTime
    {
        public const int MaxLength = 500;

        /// <summary>
        /// 体质分组
        /// </summary>
        [Required]
        public int GroupId { get; set; }

        /// <summary>
        /// 总体特征
        /// </summary>
        [StringLength(MaxLength)]
        public string GeneralFeature { get; set; }

        /// <summary>
        /// 形体特征
        /// </summary>
        [StringLength(MaxLength)]
        public string ShapeFeature { get; set; }

        /// <summary>
        /// 常见表现
        /// </summary>
        [StringLength(MaxLength)]
        public string CommonPerformance { get; set; }

        /// <summary>
        /// 心理特征
        /// </summary>
        [StringLength(MaxLength)]
        public string PsychologyFeature { get; set; }

        /// <summary>
        /// 发病倾向
        /// </summary>
        [StringLength(MaxLength)]
        public string DiseaseTendency { get; set; }

        /// <summary>
        /// 对外界环境适应能力
        /// </summary>
        [StringLength(MaxLength)]
        public string Adaptability { get; set; }

        /// <summary>
        /// 运动调养
        /// </summary>
        [StringLength(MaxLength)]
        public string ExerciseNurse { get; set; }

        /// <summary>
        /// 药物调养
        /// </summary>
        [StringLength(MaxLength)]
        public string MedicineNurse { get; set; }

        /// <summary>
        /// 调养方法
        /// </summary>
        [StringLength(MaxLength)]
        public string NurseMethod { get; set; }

        /// <summary>
        /// 健康食谱
        /// </summary>
        [StringLength(MaxLength)]
        public string HealthyRecipes { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
