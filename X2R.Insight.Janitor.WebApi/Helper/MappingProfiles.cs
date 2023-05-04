using AutoMapper;
using X2R.Insight.Janitor.WebApi.Dto;
using X2R.Insight.Janitor.WebApi.Models;

namespace X2R.Insight.Janitor.WebApi.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Querys, QueryDto>();
            CreateMap<QueryDto, Querys>();
        }
    }
}
