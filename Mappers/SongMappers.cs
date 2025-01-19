using MusicAPI.Dtos;
using MusicAPI.Models;

namespace MusicAPI.Mappers
{
    public static class SongMappers
    {
        public static SongDto ToSongDto(this Song song)
        {
            return new SongDto
            {
                Title = song.Title,
                Artist = song.Artist,
                Album = song.Album,
                Genre = song.Genre,
                Year = song.Year
            };
        }
    }
}
