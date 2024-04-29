using AutoMapper;
using Project2.Infrastructure.Context;
using Project2.Infrastructure.DTOs;
using Project2.Infrastructure.Model;
using Project2.Infrastructure.Repos.Interfaces;

namespace Project2.Infrastructure.Repos
{
    public class OwnerRepo(GardenContext context, IMapper mapper) : GenericRepo<Owner, OwnerDTO>(context, mapper), IOwnerRepo
    {
    }
}
