using System.ComponentModel;

namespace TcmHMS.Entities.Constitution
{
    /// <summary>
    /// 体质分类
    /// </summary>
    public enum ConstitutionGroup
    {
        /// <summary>
        /// 平和体质
        /// </summary>
        [Description("平和体质")]
        YinYangHarmony = 1,

        /// <summary>
        /// 气虚体质
        /// </summary>
        [Description("气虚体质")]
        QiAsthenia = 2,

        /// <summary>
        /// 阳虚体质
        /// </summary>
        [Description("阳虚体质")]
        YangAsthenia = 3,

        /// <summary>
        /// 阴虚体质
        /// </summary>
        [Description("阴虚体质")]
        YinaAthenia = 4,

        /// <summary>
        /// 痰湿体质
        /// </summary>
        [Description("痰湿体质")]
        PhlegmDampness = 5,

        /// <summary>
        /// 湿热体质
        /// </summary>
        [Description("湿热体质")]
        DampHeat = 6,

        /// <summary>
        /// 血瘀体质
        /// </summary>
        [Description("血瘀体质")]
        BloodStasis = 7,

        /// <summary>
        /// 气郁体质
        /// </summary>
        [Description("气郁体质")]
        QiStagnation = 8,

        /// <summary>
        /// 过敏体质
        /// </summary>
        [Description("过敏体质")]
        Allergic = 9
    }
}
