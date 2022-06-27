using eTickets.Data.DataContext;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Cart
{
    public class ShoppingCart
    {
        public ApplicationDbContext _context { get; set; }
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public ShoppingCart(ApplicationDbContext context)
        {
            _context = context;
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<ApplicationDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context)
            {
                ShoppingCartId = cartId
            };

        }

        public void AddItemToCart(Movie movie)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(i => i.Movie.Id == movie.Id &&
            i.ShoppingCardId == ShoppingCartId);
            if(shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCardId = ShoppingCartId,
                    Movie = movie,
                    Amount = 1
                };
                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount += 1;
            }
            _context.SaveChanges();
        }
        public void RemoveItemFromCart(Movie movie)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(i => i.Movie.Id == movie.Id &&
            i.ShoppingCardId == ShoppingCartId);
            if (shoppingCartItem != null)
            {
                if(shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            _context.SaveChanges();

        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems.Where(i =>
            i.ShoppingCardId == ShoppingCartId).Include(i => i.Movie).ToList());
        }

        public double GetShoppingCartTotal()
        {
            var total = _context.ShoppingCartItems.Where(i => i.ShoppingCardId == ShoppingCartId)
                .Select(i => i.Movie.Price * i.Amount).Sum();
            return total;
        }


        public async Task ClearShoppingCartAsync()
        {
            var items = await _context.ShoppingCartItems.Where(i => i.ShoppingCardId == ShoppingCartId).ToListAsync();
            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
            ShoppingCartItems = new List<ShoppingCartItem>();
        }


    }
}
