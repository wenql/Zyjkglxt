using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcmHMS.Entities;

namespace TcmHMS.Ranks.Dto
{
    public class RankEditDto
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(Rank.MaxNameLength)]
        public string DisplayName { get; set; }

        [Required]
        [StringLength(Rank.MaxNameLength)]
        public string Pinyin { get; set; }

        public int DisplayOrder { get; set; }
    }
}
