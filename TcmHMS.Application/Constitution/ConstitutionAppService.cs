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
using Abp.Domain.Uow;
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
        private readonly IRepository<ConstitutionSuggest> _constitutionSuggestRepository;
        private readonly IObjectMapper _objectMapper;

        public ConstitutionAppService(IRepository<ConstitutionSubject> constitutionSubjectRepository, IRepository<ConstitutionSubjectOption> constitutionSubjectOptionRepository, IRepository<ConstitutionSuggest> constitutionSuggestRepository,
            IObjectMapper objectMapper)
        {
            this._constitutionSubjectRepository = constitutionSubjectRepository;
            this._constitutionSubjectOptionRepository = constitutionSubjectOptionRepository;
            this._constitutionSuggestRepository = constitutionSuggestRepository;
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

        public async Task<ConstitutionSuggestEditDto> GetConstitutionSuggestForEdit(NullableIdDto input)
        {
            if (!input.Id.HasValue)
                throw new UserFriendlyException("记录不存在");

            var defaultSuggest = await this._constitutionSuggestRepository.GetAll().FirstOrDefaultAsync(x => x.GroupId == input.Id);
            if (defaultSuggest == null)
                defaultSuggest = new ConstitutionSuggest { GroupId = Convert.ToInt32(input.Id) };

            return defaultSuggest.MapTo<ConstitutionSuggestEditDto>();


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
                var dto = subject.MapTo<ConstitutionSubjectEditDto>();
                dto.Options = dto.Options.OrderBy(x => x.DisplayOrder).ToList();
                return dto;
            }
            return new ConstitutionSubjectEditDto()
            {
                GroupId = (int)ConstitutionGroup.YinYangHarmony
            };
        }

        [UnitOfWork]
        [AbpAuthorize(PermissionNames.Pages_Constitutions_Subjects_Create, PermissionNames.Pages_Constitutions_Subjects_Edit)]
        public async Task CreateOrUpdateConstitutionSubject(ConstitutionSubjectEditDto subject)
        {
            var model = this._objectMapper.Map(subject, subject.Id.HasValue
                ? await this._constitutionSubjectRepository.GetAsync(subject.Id.Value)
                : new ConstitutionSubject());

            var oldOptions = new HashSet<int?>(subject.Options.Where(x => x.id > 0).Select(x => x.id));
            model.Options.Where(x => !oldOptions.Contains(x.Id)).ToList().ForEach(x => this._constitutionSubjectOptionRepository.Delete(x));
            var index = 0;
            subject.Options.ForEach(x =>
            {
                x.DisplayOrder = index++;
                if (x.id == 0)
                {
                    model.Options.Add(x.MapTo<ConstitutionSubjectOption>());
                }
                else
                {
                    var option = model.Options.FirstOrDefault(d => d.Id == x.id);
                    if (option != null)
                    {
                        option = this._objectMapper.Map(x, option);
                    }
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
