using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.ObjectMapping;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using TcmHMS.Authorization;
using TcmHMS.Constitution.Dto;
using TcmHMS.Entities.Constitution;


namespace TcmHMS.Constitution
{
    [AbpAuthorize(PermissionNames.Pages_Constitutions)]
    public class ConstitutionAppService : TcmHMSAppServiceBase, IConstitutionAppService
    {
        private readonly IRepository<ConstitutionSubject> _constitutionSubjectRepository;
        private readonly IRepository<ConstitutionSubjectOption> _constitutionSubjectOptionRepository;
        private readonly IObjectMapper _objectMapper;

        public ConstitutionAppService(IRepository<ConstitutionSubject> constitutionSubjectRepository, IRepository<ConstitutionSubjectOption> constitutionSubjectOptionRepository,
            IObjectMapper objectMapper)
        {
            this._constitutionSubjectRepository = constitutionSubjectRepository;
            this._constitutionSubjectOptionRepository = constitutionSubjectOptionRepository;
            this._objectMapper = objectMapper;
        }

        public async Task<ListResultDto<ConstitutionGroupListDto>> GetConstitutionGroups()
        {
            var selectList = new List<ConstitutionGroupListDto>();
            var arrays = Enum.GetValues(typeof(ConstitutionGroup));
            for (var i = 0; i < arrays.LongLength; i++)
            {
                selectList.Add(new ConstitutionGroupListDto
                {
                    GroupId = Convert.ToInt32((ConstitutionGroup)arrays.GetValue(i))
                });
            }
            return new ListResultDto<ConstitutionGroupListDto>(selectList);
        }

        public async Task<ListResultDto<ConstitutionSubjectListDto>> GetConstitutionSubjects(GetConstitutionSubjectsInput input)
        {
            var query = this._constitutionSubjectRepository.GetAll().Include(x => x.Options)
                .WhereIf(input.Group.HasValue, u => u.GroupId == input.Group.Value)
                .WhereIf(
                    !input.Keyword.IsNullOrWhiteSpace(),
                    u => u.Title.Contains(input.Keyword));

            var diseaseCount = await query.CountAsync();
            var users = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<ConstitutionSubjectListDto>(
                diseaseCount,
                users.MapTo<List<ConstitutionSubjectListDto>>()
            );
        }

        public async Task<ConstitutionSubjectEditDto> GetConstitutionSubjectForEdit(NullableIdDto input)
        {
            if (input.Id.HasValue)
            {
                var subject = await this._constitutionSubjectRepository.GetAsync(input.Id.Value);
                return subject.MapTo<ConstitutionSubjectEditDto>();
            }
            return new ConstitutionSubjectEditDto()
            {
                GroupId = (int)ConstitutionGroup.YinYangHarmony
            };
        }

        [AbpAuthorize(PermissionNames.Pages_Constitutions_Subjects_Create, PermissionNames.Pages_Constitutions_Subjects_Edit)]
        public async Task CreateOrUpdateConstitutionSubject(ConstitutionSubjectEditDto subject)
        {
            var model = this._objectMapper.Map(subject, subject.Id.HasValue
                ? await this._constitutionSubjectRepository.GetAsync(subject.Id.Value)
                : new ConstitutionSubject());

            var oldOptions = new HashSet<int?>(subject.Options.Where(x => x.id > 0).Select(x => x.id));
            model.Options.Where(x => !oldOptions.Contains(x.Id)).ToList().ForEach(x => this._constitutionSubjectOptionRepository.Delete(x));

            subject.Options.FindAll(x => x.id == 0).ForEach(x => model.Options.Add(x.MapTo<ConstitutionSubjectOption>()));
            subject.Options.FindAll(x => x.id > 0).ForEach(x =>
            {
                var o = model.Options.FirstOrDefault(d => d.Id == x.id);
                if (o != null)
                {
                    o = this._objectMapper.Map(x, o);
                }
            });


            await this._constitutionSubjectRepository.InsertOrUpdateAsync(model);
        }

        [AbpAuthorize(PermissionNames.Pages_Constitutions_Subjects_Delete)]
        public async Task DeleteConstitutionSubject(EntityDto input)
        {
            var subject = await _constitutionSubjectRepository.GetAsync(input.Id);
            if (subject == null)
                throw new UserFriendlyException("记录不存在");

            await this._constitutionSubjectRepository.DeleteAsync(subject);
        }
    }
}
