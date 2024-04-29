using Microsoft.AspNetCore.Mvc;
using Project2.Infrastructure.Repos;
using Project2.Infrastructure.Repos.Interfaces;
using Project2.WebAPI.Controllers.Interfaces;

namespace Project2.WebAPI.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class GenericController<T, D>(IGenericRepo<T, D> repo) : Controller, IGenericController<D> where T : class
    {
        private readonly IGenericRepo<T, D> _repo = repo;

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _repo.Remove(id);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<D>> GetAll()
        {
            try
            {
                var result = _repo.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("byId/{id}")]
        public ActionResult<D> GetById(int id)
        {
            try
            {
                var found = _repo.GetById(id);
                return Ok(found);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        public async Task<ActionResult> Patch(D entity)
        {
            try
            {
                await _repo.Update(entity);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(D entity)
        {
            try
            {
                await _repo.Add(entity);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
