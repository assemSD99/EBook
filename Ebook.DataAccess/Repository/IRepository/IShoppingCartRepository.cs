using Ebook.Models;

namespace Ebook.DataAccess.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        int DecrementCount(ShoppingCart shoppingCart, int count);
        int IncrementCount(ShoppingCart shoppingCart, int count);


    }
}
