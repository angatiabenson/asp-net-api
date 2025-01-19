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
                Year = song.Year,
            };
        }

        public static Song ToStockFromCreateSong(this CreateSongRequest createSongRequest)
        {
            return new Song
            {
                Title = createSongRequest.Title,
                Artist = createSongRequest.Artist,
                Album = createSongRequest.Album,
                Genre = createSongRequest.Genre,
                Year = createSongRequest.Year,
            };
        }
    }
}
