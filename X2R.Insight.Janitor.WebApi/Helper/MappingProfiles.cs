using AutoMapper;
using X2R.Insight.Janitor.WebApi.Dto;
using X2R.Insight.Janitor.WebApi.Models;

namespace X2R.Insight.Janitor.WebApi.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<_Querys, QueryDto>();
            CreateMap<QueryDto, _Querys>();
        }
    }
}
