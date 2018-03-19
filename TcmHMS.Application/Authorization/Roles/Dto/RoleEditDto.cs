using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using TcmHMS.Authorization.Roles;

namespace TcmHMS.Application.Authorization.Roles.Dto
{
    [AutoMap(typeof(Role))]
    public class RoleEditDto
    {
        public int? Id { get; set; }

        [Required]
        public string DisplayName { get; set; }
        
        public bool IsDefault { get; set; }
    }
}