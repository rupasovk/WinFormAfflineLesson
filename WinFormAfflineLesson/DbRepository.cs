using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace WinFormAfflineLesson
{
    class DbRepository : DbContext
    {
        public DbSet<Player> Players { get; set; }

        public DbRepository() : base("Server=(localdb)\\mssqllocaldb;Database=EFdbPlayersOffline;Trusted_Connection=True;") { }
    }
}
