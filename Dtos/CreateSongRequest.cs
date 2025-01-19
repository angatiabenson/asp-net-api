﻿namespace MusicAPI.Dtos
{
    public class CreateSongRequest
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
    }
}
