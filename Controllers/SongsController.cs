namespace MusicAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MusicAPI.Models;

    /// <summary>
    /// Defines the <see cref="SongsController" />
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        /// <summary>
        /// Defines the _songs
        /// </summary>
        private static List<Song> _songs = new List<Song>
        {
            new Song
            {
                Id = 1,
                Title = "Blinding Lights",
                Artist = "The Weeknd",
                Album = "After Hours",
                Genre = "Pop",
                Year = 2020,
            },
            new Song
            {
                Id = 2,
                Title = "Shape of You",
                Artist = "Ed Sheeran",
                Album = "÷ (Divide)",
                Genre = "Pop",
                Year = 2017,
            },
            new Song
            {
                Id = 3,
                Title = "Despacito",
                Artist = "Luis Fonsi feat. Daddy Yankee",
                Album = "Vida",
                Genre = "Reggaeton",
                Year = 2017,
            },
            new Song
            {
                Id = 4,
                Title = "Levitating",
                Artist = "Dua Lipa feat. DaBaby",
                Album = "Future Nostalgia",
                Genre = "Pop",
                Year = 2020,
            },
            new Song
            {
                Id = 5,
                Title = "Bad Guy",
                Artist = "Billie Eilish",
                Album = "When We All Fall Asleep, Where Do We Go?",
                Genre = "Pop",
                Year = 2019,
            },
            new Song
            {
                Id = 6,
                Title = "Old Town Road",
                Artist = "Lil Nas X feat. Billy Ray Cyrus",
                Album = "7",
                Genre = "Country Rap",
                Year = 2019,
            },
            new Song
            {
                Id = 7,
                Title = "Uptown Funk",
                Artist = "Mark Ronson feat. Bruno Mars",
                Album = "Uptown Special",
                Genre = "Funk Pop",
                Year = 2014,
            },
            new Song
            {
                Id = 8,
                Title = "Someone Like You",
                Artist = "Adele",
                Album = "21",
                Genre = "Soul",
                Year = 2011,
            },
            new Song
            {
                Id = 9,
                Title = "Rolling in the Deep",
                Artist = "Adele",
                Album = "21",
                Genre = "Pop",
                Year = 2010,
            },
            new Song
            {
                Id = 10,
                Title = "Shallow",
                Artist = "Lady Gaga & Bradley Cooper",
                Album = "A Star Is Born Soundtrack",
                Genre = "Pop Ballad",
                Year = 2018,
            },
        };

        /// <summary>
        /// The Get
        /// </summary>
        /// <returns>The <see cref="ActionResult{IEnumerable{Song}}"/></returns>
        [HttpGet]
        public ActionResult<IEnumerable<Song>> Get()
        {
            return _songs;
        }

        /// <summary>
        /// The Post
        /// </summary>
        /// <param name="song">The song<see cref="Song"/></param>
        /// <returns>The <see cref="ActionResult{Song}"/></returns>
        [HttpPost]
        public ActionResult<Song> Post([FromBody] Song song)
        {
            if (song == null)
            {
                return BadRequest("Song is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Generate a new Id for the song
            song.Id = _songs.Max(s => s.Id) + 1;
            _songs.Add(song);

            return CreatedAtAction(nameof(Get), new { id = song.Id }, song);
        }
        /// <summary>
        /// The Put
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <param name="song">The song<see cref="Song"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Song song)
        {
            if (song == null || song.Id != id)
            {
                return BadRequest("Song is null or Id mismatch.");
            }

            var existingSong = _songs.FirstOrDefault(s => s.Id == id);
            if (existingSong == null)
            {
                return NotFound("Song not found.");
            }

            existingSong.Title = song.Title;
            existingSong.Artist = song.Artist;
            existingSong.Album = song.Album;
            existingSong.Genre = song.Genre;
            existingSong.Year = song.Year;

            return NoContent();
        }

        [HttpDelete("{id}")]

        public ActionResult Delete(int id)
        {
            var existingSong = _songs.FirstOrDefault(s => s.Id == id);
            if (existingSong == null)
            {
                return NotFound("Song not found.");
            }

            _songs.Remove(existingSong);

            return NoContent();
        }
    }
}
