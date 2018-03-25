using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcmHMS.Dto;

namespace TcmHMS.Departments.Dto
{
    public class GetDiseasesInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Keyword { get; set; }

        public int? Department { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "DisplayName,Pinyin";
            }
        }
    }
}
