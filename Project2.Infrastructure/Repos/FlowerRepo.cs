using AutoMapper;
using Project2.Infrastructure.Context;
using Project2.Infrastructure.DTOs;
using Project2.Infrastructure.Model;
using Project2.Infrastructure.Repos.Interfaces;

namespace Project2.Infrastructure.Repos
{
    public class FlowerRepo(GardenContext context, IMapper mapper) : GenericRepo<Flower, FlowerDTO>(context, mapper), IFlowerRepo
    {
    }
}
