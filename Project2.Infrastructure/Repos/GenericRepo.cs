using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project2.Infrastructure.Context;
using Project2.Infrastructure.Model;
using Project2.Infrastructure.Repos.Interfaces;

namespace Project2.Infrastructure.Repos
{
    public class GenericRepo<T, D>(GardenContext context, IMapper mapper) : IGenericRepo<T, D> where T : BaseEntity
    {
        private readonly GardenContext _context = context;
        private readonly IMapper _mapper = mapper;


        public async Task Add(D entity)
        {
            _context.Add(_mapper.Map<T>(entity)).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public IEnumerable<D> GetAll()
        {
            var results = _mapper.Map<List<D>>(_context.Set<T>().ToList()).AsEnumerable();
            return results;
        }

        public D GetById(int id)
        {
            var result = _mapper.Map<D>(_context.Set<T>().Where(x => x.Id == id).FirstOrDefault())
                ?? throw new Exception("Entity does not exist!");
            return result;
        }

        public async Task Remove(int id)
        {
            _context.Remove(_context.Set<T>().Where(x => x.Id == id).FirstOrDefault());
            await _context.SaveChangesAsync();
        }

        public async Task Update(D entity)
        {
            _context.Entry(_mapper.Map<T>(entity)).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
