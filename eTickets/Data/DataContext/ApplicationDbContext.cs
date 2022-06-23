using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Actor_Movie> Actors_Movies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor_Movie>().HasKey(i => new
            {
                i.ActorId,
                i.MovieId
            });
            modelBuilder.Entity<Actor_Movie>().HasOne(i => i.Movie)
                .WithMany(i => i.Actor_Movies)
                .HasForeignKey(i => i.MovieId);
            modelBuilder.Entity<Actor_Movie>().HasOne(i => i.Actor)
               .WithMany(i => i.Actor_Movies)
               .HasForeignKey(i => i.ActorId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
