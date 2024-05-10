using Ebook.DataAccess.Repository.IRepository;
using Ebook.Models;
using EBook.DataAccess;

namespace Ebook.DataAccess.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private ApplicationDbContext _db;

        public ShoppingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }



    }
}
