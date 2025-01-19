using Azure.Core;
using Microsoft.EntityFrameworkCore;
using MusicAPI.Data;
using MusicAPI.Dtos;
using MusicAPI.Interfaces;
using MusicAPI.Models;

namespace MusicAPI.Repositories
{
    public class SongRepository : ISongRepository
    {
        private readonly ApiDbContext _context;


        public SongRepository(ApiDbContext context) {
            _context = context;
        }
        public async Task<Song?> AddSong(Song song)
        {
            await _context.Songs.AddAsync(song);
            await _context.SaveChangesAsync();

            return song;

        }

        public async Task<Song?> DeleteSong(int id)
        {
            var song = await _context.Songs.FirstOrDefaultAsync(x => x.Id == id);

            if (song == null)
            {
                return null;
            }

            _context.Songs.Remove(song);

            await _context.SaveChangesAsync();

            return song;
        }

        public async Task<IEnumerable<Song>> GetAllSongs()
        {
            var songs = await _context.Songs.ToListAsync();
            return songs;
        }

        public async Task<Song?> GetSongById(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return null;
            }
            return song;
        }

        public async Task<Song?> UpdateSong(int id, UpdateSongRequest request)
        {
            var song = await _context.Songs.FirstOrDefaultAsync(x => x.Id == id);

            if (song == null)
            {
                return null;
            }

            song.Title = request.Title;
            song.Artist = request.Artist;
            song.Album = request.Album;
            song.Year = request.Year;
            song.Genre = request.Genre;

            await _context.SaveChangesAsync();

            return song;
        }
    }
}
