using eTickets.Data.Base;
using eTickets.Data.DataContext;
using eTickets.Data.Services.Abstraction;
using eTickets.Models;

namespace eTickets.Data.Services.Implementation
{
    public class ProducersService : EntityBaseRepository<Producer>, IProducersService
    {
        public ProducersService(ApplicationDbContext context) : base(context)
        {

        }
    }
}
