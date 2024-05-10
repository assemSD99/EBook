using Ebook.DataAccess.Repository.IRepository;
using Ebook.Models;
using EBook.DataAccess;

namespace Ebook.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }



    }
}
