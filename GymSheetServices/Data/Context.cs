using Microsoft.EntityFrameworkCore;

namespace GymSheet.Services.Data
{
    class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
        }
    }
}
