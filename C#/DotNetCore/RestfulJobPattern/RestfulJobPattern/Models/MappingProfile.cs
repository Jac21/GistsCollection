using AutoMapper;
using RestfulJobPattern.Data.Documents;

namespace RestfulJobPattern.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Star, StarDto>();
            CreateMap<StarDto, Star>();

            CreateMap<Job, JobDto>();
            CreateMap<JobDto, Job>();
        }
    }
}