using AutoMapper;
using Project2.Infrastructure.DTOs;
using Project2.Infrastructure.Model;

namespace Project2.Infrastructure.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Flower, FlowerDTO>().ReverseMap();
            CreateMap<Garden, GardenDTO>().ReverseMap();
            CreateMap<Owner, OwnerDTO>().ReverseMap();
            CreateMap<Plant, PlantDTO>().ReverseMap();
            CreateMap<Tree, TreeDTO>().ReverseMap();
            CreateMap<BaseEntity, BaseDTO>().ReverseMap();
        }
    }
}
