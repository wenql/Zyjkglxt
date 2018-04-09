using AutoMapper;
using TcmHMS.Authorization.Users;
using TcmHMS.Authorization.Users.Dto;
using TcmHMS.Constitution.Dto;
using TcmHMS.Departments.Dto;
using TcmHMS.Diseases.Dto;
using TcmHMS.Entities;
using TcmHMS.Entities.Constitution;
using TcmHMS.Medicines.Dto;
using TcmHMS.Ranks.Dto;

namespace TcmHMS.Application
{
    internal static class CustomDtoMapper
    {
        private static volatile bool _mappedBefore;
        private static readonly object SyncObj = new object();

        public static void CreateMappings(IMapperConfigurationExpression mapper)
        {
            lock (SyncObj)
            {
                if (_mappedBefore)
                {
                    return;
                }

                CreateMappingsInternal(mapper);

                _mappedBefore = true;
            }
        }

        private static void CreateMappingsInternal(IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<User, UserEditDto>()
                .ForMember(dto => dto.Password, options => options.Ignore())
                .ReverseMap()
                .ForMember(user => user.Password, options => options.Ignore());

            mapper.CreateMap<RankEditDto, Rank>()
                .ForMember(user => user.CreationTime, options => options.Ignore());

            mapper.CreateMap<DepartmentEditDto, Department>()
                .ForMember(user => user.CreationTime, options => options.Ignore());

            mapper.CreateMap<DiseaseEditDto, Disease>()
                .ForMember(user => user.CreationTime, options => options.Ignore());

            mapper.CreateMap<MedicineEditDto, Medicine>()
                .ForMember(user => user.CreationTime, options => options.Ignore());

            mapper.CreateMap<ConstitutionSubjectEditDto, ConstitutionSubject>()
                .ForMember(user => user.CreationTime, options => options.Ignore())
                .ForMember(x => x.Options, options => options.Ignore());

            mapper.CreateMap<ConstitutionSuggestEditDto, ConstitutionSuggest>()
    .ForMember(user => user.CreationTime, options => options.Ignore());

        }
    }
}