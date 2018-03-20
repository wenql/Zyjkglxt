using System.Data.Entity;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Shouldly;
using TcmHMS.Authorization.Users;
using Xunit;

namespace TcmHMS.Tests.Users
{
    public class UserAppService_Tests : TcmHMSTestBase
    {
        private readonly IUserAppService _userAppService;

        public UserAppService_Tests()
        {
            _userAppService = Resolve<IUserAppService>();
        }
    }
}
