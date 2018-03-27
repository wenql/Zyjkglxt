using System.ComponentModel;

namespace TcmHMS.Entities.Enum
{
    /// <summary>
    /// 婚姻状况
    /// </summary>
    public enum Married
    {
        /// <summary>
        /// 未婚
        /// </summary>
        [Description("未婚")]
        UnMarried = 0,

        /// <summary>
        /// 已婚
        /// </summary>
        [Description("已婚")]
        IsMarried = 1,

        /// <summary>
        /// 离异
        /// </summary>
        [Description("离异")]
        Divorce = 2,

        /// <summary>
        /// 丧偶
        /// </summary>
        [Description("丧偶")]
        Widowed = 3
    }
}
