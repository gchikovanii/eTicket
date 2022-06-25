using eTickets.Models;

namespace eTickets.Data.ViewModels
{
    public class NewMovieDropdownsVM
    {

        public NewMovieDropdownsVM()
        {
            Producers = new List<Producer>();
            Cienmas = new List<Cinema>();
            Actors = new List<Actor>();
        }
        public List<Producer> Producers { get; set; }
        public List<Cinema> Cienmas { get; set; }
        public List<Actor> Actors { get; set; }
    }
}
