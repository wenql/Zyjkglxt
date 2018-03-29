using System.ComponentModel;
using TcmHMS.Entities.Constitution;

namespace TcmHMS.Constitution.Dto
{
    public class ConstitutionGroupListDto
    {
        public int GroupId { get; set; }

        public string GroupName
        {
            get
            {
                var obj = (ConstitutionGroup)GroupId;
                var objName = obj.ToString();
                var t = obj.GetType();
                var fi = t.GetField(objName);
                if (fi == null)
                {
                    return "";
                }
                var arrDesc = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                return arrDesc[0].Description;
            }
        }
    }
}
