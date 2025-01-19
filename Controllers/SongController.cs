using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicAPI.Data;
using MusicAPI.Dtos;
using MusicAPI.Mappers;
using MusicAPI.Models;

namespace MusicAPI.Controllers
{
    [Route("api/song")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public SongController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var songs = await _context.Songs.ToListAsync();
            var songDtos = songs.Select(s => s.ToSongDto());
            return Ok(songDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var song = await _context.Songs.FindAsync(id);
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
            await _context.Songs.AddAsync(songModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = songModel.Id }, songModel.ToSongDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateSongRequest request)
        {
            var song = await _context.Songs.FirstOrDefaultAsync(x => x.Id == id);

            if (song == null)
            {
                return NotFound();
            }

            song.Title = request.Title;
            song.Artist = request.Artist;
            song.Album = request.Album;
            song.Year = request.Year;
            song.Genre = request.Genre;

            await _context.SaveChangesAsync();

            return Ok(song.ToSongDto());
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var song = await _context.Songs.FirstOrDefaultAsync(x => x.Id == id);

            if (song == null)
            {
                return NotFound();
            }

            _context.Songs.Remove(song);

           await _context.SaveChangesAsync();

            return NoContent(); 

        }

    }
}
