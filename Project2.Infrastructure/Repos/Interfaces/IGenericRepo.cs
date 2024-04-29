using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Infrastructure.Repos.Interfaces
{
    public interface IGenericRepo<T, D>
    {
        public Task Add(D entity);
        public Task Remove(int id);
        public Task Update(D entity);
        public IEnumerable<D> GetAll();
        public D GetById(int id);
    }
}
