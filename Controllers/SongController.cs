using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicAPI.Data;
using MusicAPI.Dtos;
using MusicAPI.Interfaces;
using MusicAPI.Mappers;
using MusicAPI.Models;

namespace MusicAPI.Controllers
{
    [Route("api/song")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly ISongRepository _repository;

        public SongController(ApiDbContext context, ISongRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var songs = await _repository.GetAllSongs();
            var songDtos = songs.Select(s => s.ToSongDto());
            return Ok(songDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var song = await _repository.GetSongById(id);
            if (song == null)
            {
                return NotFound();
            }
            return Ok(song.ToSongDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSongRequest request)
        {
            var songModel = request.ToStockFromCreateSong();

            await _repository.AddSong(songModel);
       
            return CreatedAtAction(nameof(GetById), new { id = songModel.Id }, songModel.ToSongDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateSongRequest request)
        {
            var song = await _repository.UpdateSong(id, request);

            if (song == null)
            {
                return NotFound();
            }

            return Ok(song.ToSongDto());
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var song = await _repository.DeleteSong(id);

            if (song == null)
            {
                return NotFound();
            }

            return NoContent(); 

        }

    }
}
