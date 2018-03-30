using Abp.Runtime.Validation;
using TcmHMS.Dto;

namespace TcmHMS.Constitution.Dto
{
    public class GetConstitutionSubjectsInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Keyword { get; set; }

        public int? Group { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "id";
            }
        }
    }
}
