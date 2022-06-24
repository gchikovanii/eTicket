using eTickets.Data.Base;
using eTickets.Data.DataContext;
using eTickets.Models;

namespace eTickets.Data.Services.Implementation
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        public MoviesService(ApplicationDbContext contex): base(contex)
        {

        }
    }
}
