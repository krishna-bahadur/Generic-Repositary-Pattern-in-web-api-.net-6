using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccessEF
{
    public class DBContext :DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions options) : base(options) { }
        
        public DbSet<User> Users {get;set;}

        
    }
}