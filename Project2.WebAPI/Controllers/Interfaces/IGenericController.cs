using Microsoft.AspNetCore.Mvc;

namespace Project2.WebAPI.Controllers.Interfaces
{
    public interface IGenericController<D>
    {
        public Task<ActionResult> Post(D entity);
        public Task<ActionResult> Delete(int id);
        public Task<ActionResult> Patch(D entity);
        public ActionResult<IEnumerable<D>> GetAll();
        public ActionResult<D> GetById(int id);
    }
}
