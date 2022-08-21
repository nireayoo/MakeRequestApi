using MakeRequestApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace MakeRequestApi
{
    public class UsersContext: DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
