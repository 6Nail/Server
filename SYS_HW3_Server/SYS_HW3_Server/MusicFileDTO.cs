using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public class MusicFileDTO
    {
        public Guid Id { get; set; }
        public string Author { get; set; }
        public string SongName { get; set; }
        public string MusicPath { get; set; }
    }
}
