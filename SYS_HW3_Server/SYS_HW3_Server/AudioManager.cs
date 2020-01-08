using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Server
{
    public class AudioManager
    {
        private readonly IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("10.1.4.78"), 3231);

        public async Task SaveAllSongsToDb()
        {
            var songPaths = Directory.GetFiles(@$"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\Pictures)").ToList();

            using (var context = new MusicContext())
            {
                foreach (var music in songPaths)
                {
                    var parts = Path.GetFileName(music).Split("-");
                    var isExist = await context.MusicFiles.SingleOrDefaultAsync(x => x.MusicPath == music);
                    if (isExist is null) continue;

                    context.MusicFiles.Add(new MusicFileDTO
                    {
                        Author = parts.First(),
                        SongName = parts.Last(),
                        MusicPath = music,
                    });

                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task SendMusic(Guid id)
        {
            using (var context = new MusicContext())
            {
                var music = await context.MusicFiles.SingleOrDefaultAsync(x => x.Id == id);
                if (music is null) return;

                using (var client = new TcpClient())
                {
                    client.Connect(endPoint);
                    using (var stream = client.GetStream())
                    {
                        var data = await File.ReadAllBytesAsync(music.MusicPath);
                        var musicFile = new MusicFile {Id = music.Id, SongName = music.SongName, Author = music.Author, AudioFile = data.ToList()};
                        var json = JsonConvert.SerializeObject(musicFile);
                        var sendData = Encoding.UTF8.GetBytes(json);

                        stream.Write(sendData, 0, sendData.Length);
                    }
                }
            }
        }

        public async Task SendMusicInfo()
        {
            using (var client = new TcpClient())
            {
                client.Connect(endPoint);
                using (var stream = client.GetStream())
                {


                    //stream.Write(sendData, 0, sendData.Length);
                }
            }
        }
    }
}
