using Backend.Model;
using Backend.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {

        private readonly IAlbumRepository _repo;

        public AlbumController(IAlbumRepository repository)
        {
            _repo = repository;
        }


        /// <summary>
        /// Lists all albums
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repo.GetAll());
        }


        /// <summary>
        /// Get album by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _repo.GetById(id);

            return result is not null ? Ok(result) : NotFound();
        }


        /// <summary>
        /// Add a new album 
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post(Album model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _repo.Save(model);
            return Created($"/{model.Id}", model);
        }


        /// <summary>
        /// Deletes an album 
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _repo.GetById(id);
            await _repo.Remove(result);
            return NoContent();
        }
    }
}
