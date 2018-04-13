using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Runtime.Validation;
using TcmHMS.Dto;

namespace TcmHMS.Doctors.Dto
{
    public class GetDoctorsInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Keyword { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "NickName";
            }
        }
    }
}
