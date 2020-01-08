using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public class MusicFile
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Author { get; set; }
        public string SongName { get; set; }
        public List<byte> AudioFile { get; set; } = new List<byte>();
    }
}
