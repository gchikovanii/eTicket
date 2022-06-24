using eTickets.Data.Base;
using eTickets.Data.DataContext;
using eTickets.Data.Services.Abstraction;
using eTickets.Models;

namespace eTickets.Data.Services.Implementation
{
    public class CinemasService : EntityBaseRepository<Cinema>,ICinemasService
    {
        public CinemasService(ApplicationDbContext context): base(context)
        {

        }
    }
}
