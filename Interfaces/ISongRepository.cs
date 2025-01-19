using MusicAPI.Dtos;
using MusicAPI.Models;

namespace MusicAPI.Interfaces
{
    public interface ISongRepository
    {
        Task<IEnumerable<Song>> GetAllSongs();
        Task<Song?> GetSongById(int id);
        Task<Song?> AddSong(Song request);
        Task<Song?> UpdateSong(int id, UpdateSongRequest request);
        Task<Song?> DeleteSong(int id);
    }
}
