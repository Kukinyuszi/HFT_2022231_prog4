namespace Esport.Repository.Database
{
    using System;

    using Esport.Data;

    using Microsoft.EntityFrameworkCore;
    public class DataContext : DbContext
    {
        public DataContext()
        {
            this.Database.EnsureCreated();
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Team> Team { get; set; }

        public virtual DbSet<Match> Match { get; set; }

        public virtual DbSet<Location> Location { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder
                    .UseLazyLoadingProxies()
                    .UseInMemoryDatabase("esport");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(null, "Optionsbuilder is null!");
            }

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasOne(loc => loc.Match)
                    .WithMany(m => m.Locations)
                    .HasForeignKey(f => f.MatchID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasOne(team => team.Match)
                    .WithMany(match => match.Teams)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
            /*
            var s1 = new Match() { ID = 1, Location = "New York", Name = "FaceIT" };
            var s2 = new Match() { ID = 2, Location = "Budapest", Name = "Arena of Valor" };
            var s3 = new Match() { ID = 3, Location = "Wien", Name = "Amason University Esports" };
            var s4 = new Match() { ID = 4, Location = "Paris", Name = "Battle.Net World Championship Series" };
            var s5 = new Match() { ID = 5, Location = "Tokyo", Name = "Call of Duty Series" };
            var s6 = new Match() { ID = 6, Location = "Madrid", Name = "Dream Hack" };
            var s7 = new Match() { ID = 7, Location = "Frankfurt", Name = "eGames" };
            var s8 = new Match() { ID = 8, Location = "HongKong", Name = "Electronic Sports World Cup" };
            var s9 = new Match() { ID = 9, Location = "Milano", Name = "Evolution Championship Series" };

            var w1 = new Location() { ID = 1, MatchID = s1.ID, Name = "New York", Capacity = 800 };
            var w2 = new Location() { ID = 2, MatchID = s2.ID, Name = "Budapest", Capacity = 300 };
            var w3 = new Location() { ID = 3, MatchID = s3.ID, Name = "Wien", Capacity = 200 };
            var w4 = new Location() { ID = 4, MatchID = s4.ID, Name = "Paris", Capacity = 600 };
            var w5 = new Location() { ID = 5, MatchID = s5.ID, Name = "Tokyo", Capacity = 600 };
            var w6 = new Location() { ID = 6, MatchID = s6.ID, Name = "Madrid", Capacity = 2300 };
            var w7 = new Location() { ID = 7, MatchID = s7.ID, Name = "Frankfurt", Capacity = 3000 };
            var w8 = new Location() { ID = 8, MatchID = s8.ID, Name = "HongKong", Capacity = 30000 };
            var w9 = new Location() { ID = 9, MatchID = s9.ID, Name = "Milano", Capacity = 5000 };

            var c1 = new Team() { ID = 1, Name = "Fnatic", Wins = 423, MatchID = s2.ID };
            var c2 = new Team() { ID = 2, Name = "G2", Wins = 231, MatchID = s3.ID };
            var c3 = new Team() { ID = 3, Name = "Cloud9", Wins = 312, MatchID = s4.ID };
            var c4 = new Team() { ID = 4, Name = "Philadelphia Fusion", Wins = 362, MatchID = s5.ID };
            var c5 = new Team() { ID = 5, Name = "Astrails", Wins = 812, MatchID = s6.ID };
            var c6 = new Team() { ID = 6, Name = "Jin Air Green Wings", Wins = 712, MatchID = s7.ID };
            var c7 = new Team() { ID = 7, Name = "Made in Brazil", Wins = 12, MatchID = s8.ID };
            var c8 = new Team() { ID = 8, Name = "Seoul Dynasty", Wins = 92, MatchID = s9.ID };
            var c9 = new Team() { ID = 9, Name = "London Spitfire", Wins = 120, MatchID = s2.ID };
            var c10 = new Team() { ID = 10, Name = "Dallas Fuel", Wins = 423, MatchID = s3.ID };
            var c11 = new Team() { ID = 11, Name = "Los Angeles Valiant", Wins = 231, MatchID = s4.ID };
            var c12 = new Team() { ID = 12, Name = "CDEC Gaming", Wins = 532, MatchID = s5.ID };
            var c13 = new Team() { ID = 13, Name = "Boston Uprising", Wins = 362, MatchID = s6.ID };
            var c14 = new Team() { ID = 14, Name = "Sanghai Dragons", Wins = 812, MatchID = s7.ID };
            var c15 = new Team() { ID = 15, Name = "Toronto Defiant", Wins = 712, MatchID = s8.ID };
            var c16 = new Team() { ID = 16, Name = "FC Shalke", Wins = 12, MatchID = s9.ID };
            var c17 = new Team() { ID = 17, Name = "Houston Outlaws", Wins = 92, MatchID = s1.ID };
            var c18 = new Team() { ID = 18, Name = "FlyQuest", Wins = 120, MatchID = s1.ID };
            

            modelBuilder.Entity<Match>().HasData(s1, s2, s3, s4, s5, s6, s7, s8, s9);
            modelBuilder.Entity<Location>().HasData(w1, w2, w3, w4, w5, w6, w7, w8, w9);
            modelBuilder.Entity<Team>().HasData(c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12, c13, c14, c15, c16, c17, c18);
            */

            modelBuilder.Entity<Team>().HasData(new Team[]
            {
                new Team("1#Fnatic#21#2000#1"),
                new Team("2#G2#21#22122#1"),
                new Team("3#CDEC Gaming#21#2321#2"),
                new Team("4#Dallas Fuel#21#2313#2"),
                new Team("5#Astrails#21#2222#3"),
            });

            modelBuilder.Entity<Match>().HasData(new Match[]
            {
                new Match("1#Budapest#Call of Duty Series#1#2"),
                new Match("2#Wien#Dream Hack#3#4"),
                new Match("3#Barcelona#eGames#5#1"),
                new Match("4#Tokyo#Electronic Sports World Cup#2#4"),
                new Match("5#Frankfurt#FaceIT#3#5"),
            });

            modelBuilder.Entity<Location>().HasData(new Location[]
            {
                new Location("1#Budapest#20000#1"),
                new Location("2#Wien#25000#2"),
                new Location("3#Barcelona#21000#3"),
                new Location("4#Tokyo#6000#4"),
                new Location("5#Frankfurt#60000#5"),
            });
        }
    }
}
