using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcmHMS.Entities.Enum
{
    /// <summary>
    /// 血型
    /// </summary>
    public enum BloodType
    {
        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        Other = 0,

        /// <summary>
        /// A型
        /// </summary>
        [Description("A型")]
        TypeA = 1,

        /// <summary>
        /// B型
        /// </summary>
        [Description("B型")]
        TypeB = 2,

        /// <summary>
        /// AB型
        /// </summary>
        [Description("AB型")]
        TypeAb = 3,

        /// <summary>
        /// 丧偶
        /// </summary>
        [Description("丧偶")]
        TypeO = 3
    }
}
