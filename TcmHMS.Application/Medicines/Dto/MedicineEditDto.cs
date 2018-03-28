using System.ComponentModel.DataAnnotations;
using TcmHMS.Entities;

namespace TcmHMS.Medicines.Dto
{
    public class MedicineEditDto
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(Rank.MaxNameLength)]
        public string DisplayName { get; set; }

        [Required]
        [StringLength(Rank.MaxNameLength)]
        public string Pinyin { get; set; }

        [Required]
        [StringLength(Rank.MaxNameLength)]
        public string Identifier { get; set; }

        public string PackagingSpec { get; set; }

        public string Function { get; set; }

        public string Dosage { get; set; }

        public string Source { get; set; }
    }
}
