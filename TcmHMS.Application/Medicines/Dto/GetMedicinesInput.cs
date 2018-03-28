using Abp.Runtime.Validation;
using TcmHMS.Dto;

namespace TcmHMS.Medicines.Dto
{
    public class GetMedicinesInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Keyword { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "DisplayName,Pinyin";
            }
        }
    }
}
