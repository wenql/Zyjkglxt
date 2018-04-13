using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System.Threading.Tasks;
using TcmHMS.Diseases.Dto;
using TcmHMS.Doctors.Dto;

namespace TcmHMS.Doctors
{
    public interface IDoctorAppService : IApplicationService
    {
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ListResultDto<DoctorListDto>> GetDoctors(GetDoctorsInput input);

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<DoctorEditDto> GetDoctorForEdit(NullableIdDto input);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateDoctor(CreateOrUpdateDoctorInput input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteDoctor(EntityDto input);
    }
}
