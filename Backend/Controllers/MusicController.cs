using Backend.Model;
using Backend.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {

        private readonly IAlbumRepository _repo;
        
        public MusicController(IAlbumRepository repository)
        {
            _repo = repository;
        }


        /// <summary>
        /// Lists every music from an album
        /// </summary>
        [HttpGet()]
        public async Task<IActionResult> GetAll(Guid id)
        {
            var music = await _repo.GetMusicFromAlbum(id);

            return Ok(music);
        }


        /// <summary>
        /// Get music by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var music = await _repo.GetMusic(id);

            return Ok(music);
        }


        /// <summary>
        /// Add music to album
        /// </summary>
        [HttpPost("{albumId}")]
        public async Task<IActionResult> Post(Guid albumId, Music model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var album = await _repo.GetById(albumId);

            album.Musics.Add(model);

            await _repo.Update(album);

            return Ok();
        }
    }
}
