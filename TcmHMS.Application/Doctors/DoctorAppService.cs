using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.ObjectMapping;
using Abp.UI;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using TcmHMS.Authorization;
using TcmHMS.Authorization.Users;
using TcmHMS.Doctors.Dto;
using TcmHMS.Entities;

namespace TcmHMS.Doctors
{
    public class DoctorAppService : TcmHMSAppServiceBase, IDoctorAppService
    {
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly IRepository<Rank> _rankRepository;
        private readonly IRepository<Department> _departmentRepository;
        private readonly IObjectMapper _objectMapper;

        public DoctorAppService(IRepository<Doctor> doctorRepository, IRepository<Department> departmentRepository,
            IRepository<Rank> rankRepository,
            IObjectMapper objectMapper)
        {
            this._doctorRepository = doctorRepository;
            this._rankRepository = rankRepository;
            this._departmentRepository = departmentRepository;
            this._objectMapper = objectMapper;
        }

        public async Task<ListResultDto<DoctorListDto>> GetDoctors(GetDoctorsInput input)
        {
            throw new NotImplementedException();
        }

        public async Task<DoctorEditDto> GetDoctorForEdit(NullableIdDto input)
        {
            if (input.Id.HasValue)
            {
                var doctor = await this._doctorRepository.GetAsync(input.Id.Value);
                return doctor.MapTo<DoctorEditDto>();
            }
            var defaultDept = await this._departmentRepository.GetAll().FirstOrDefaultAsync();
            if (defaultDept == null)
            {
                throw new UserFriendlyException("请先添加科室");
            }

            var defaultRank = await this._rankRepository.GetAll().FirstOrDefaultAsync();
            if (defaultRank == null)
            {
                throw new UserFriendlyException("请先添加职称");
            }

            return new DoctorEditDto()
            {
                DepartmentId = defaultDept.Id,
                RankId = defaultRank.Id
            };
        }

        [AbpAuthorize(PermissionNames.Pages_Doctors_Edit, PermissionNames.Pages_Doctors_Create)]
        public async Task CreateOrUpdateDoctor(CreateOrUpdateDoctorInput input)
        {

            var model = this._objectMapper.Map(input.Doctor, input.Doctor.Id.HasValue
                ? await this._doctorRepository.GetAsync(input.Doctor.Id.Value)
                : new Doctor());

            if (!input.Doctor.Password.IsNullOrEmpty())
            {
                CheckErrors(await UserManager.PasswordValidator.ValidateAsync(input.Doctor.Password));
            }
            else
            {
                input.Doctor.Password = User.CreateRandomPassword();
            }

            model.Password = new PasswordHasher().HashPassword(input.Doctor.Password);

            await this._doctorRepository.InsertOrUpdateAsync(model);

        }

        public async Task DeleteDoctor(EntityDto input)
        {
            throw new NotImplementedException();
        }
    }
}
