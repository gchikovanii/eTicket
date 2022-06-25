using eTickets.Data.Base;
using eTickets.Data.DataContext;
using eTickets.Data.Services.Abstraction;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services.Implementation
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly ApplicationDbContext _contex;
        public MoviesService(ApplicationDbContext contex): base(contex)
        {
            _contex = contex;
        }

        public async Task AddNewMovieAsync(NewMovieVM data)
        {
            var newMovie = new Movie()
            {
                Name = data.Name,
                Description =data.Description,
                ImageUrl = data.ImageUrl,
                Price = data.Price,
                CinemaId = data.CinemaId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                MovieCategory = data.MovieCategory,
                ProducerId = data.ProducerId
            };

            await _contex.Movies.AddAsync(newMovie);
            await _contex.SaveChangesAsync();
            //Adding Movie Actors
            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId
                };
                await _contex.Actors_Movies.AddAsync(newActorMovie);
            }
            await _contex.SaveChangesAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDetails = await _contex.Movies
                .Include(i => i.Cinema)
                .Include(i => i.Producer)
                .Include(i => i.Actor_Movies)
                .ThenInclude(i => i.Actor)
                .FirstOrDefaultAsync(I => I.Id == id);
            return movieDetails;
        }

        public async Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues()
        {
            var response = new NewMovieDropdownsVM() 
            {
                Actors = await _contex.Actors.OrderBy(i => i.FullName).ToListAsync(),
                Cienmas = await _contex.Cinemas.OrderBy(i => i.Name).ToListAsync(),
                Producers = await _contex.Producers.OrderBy(i => i.FullName).ToListAsync()
            };

            return response;
        }

        public async Task UpdateMovieAsync(NewMovieVM data)
        {
            var dbMovie = await _contex.Movies.FirstOrDefaultAsync(i => i.Id == data.Id);

            if(dbMovie != null)
            {
                dbMovie .Name= data.Name;
                dbMovie.Description = data.Description;
                dbMovie.ImageUrl = data.ImageUrl;
                dbMovie.Price = data.Price;
                dbMovie.CinemaId = data.CinemaId;
                dbMovie.StartDate = data.StartDate;
                dbMovie.EndDate = data.EndDate;
                dbMovie.MovieCategory = data.MovieCategory;
                dbMovie.ProducerId = data.ProducerId;
                await _contex.SaveChangesAsync();
            }
            //Remove existing actors
            var existing = _contex.Actors_Movies.Where(i => i.MovieId == data.Id).ToList();
            _contex.Actors_Movies.RemoveRange(existing);
            await _contex.SaveChangesAsync();        
            //Adding Movie Actors
            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = data.Id,
                    ActorId = actorId
                };
                await _contex.Actors_Movies.AddAsync(newActorMovie);
            }
            await _contex.SaveChangesAsync();
        }
    }
}
