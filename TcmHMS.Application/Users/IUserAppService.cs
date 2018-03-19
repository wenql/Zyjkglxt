using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TcmHMS.Application.Authorization.Roles.Dto;
using TcmHMS.Users.Dto;

namespace TcmHMS.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UpdateUserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();
    }
}