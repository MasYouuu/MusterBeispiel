using Project2.Infrastructure.DTOs;
using Project2.Infrastructure.Model;
using Project2.Infrastructure.Repos.Interfaces;

namespace Project2.WebAPI.Controllers
{
    public class GardenController(IGardenRepo repo) : GenericController<Garden, GardenDTO>(repo)
    {
    }
}
