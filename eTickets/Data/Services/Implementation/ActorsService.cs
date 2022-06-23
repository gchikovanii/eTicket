using eTickets.Data.Base;
using eTickets.Data.DataContext;
using eTickets.Data.Services.Abstraction;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services.Implementation
{
    public class ActorsService : EntityBaseRepository<Actor>, IActorsService
    {
        public ActorsService(ApplicationDbContext context) : base(context)
        {           
        }
        
        
    }
}
