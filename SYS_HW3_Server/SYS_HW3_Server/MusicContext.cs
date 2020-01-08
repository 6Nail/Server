using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public class MusicContext : DbContext
    {
        public MusicContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<MusicFileDTO> MusicFiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=A-104-12;Database=MusicDb;Trusted_Connection=True");
        }
    }
}
