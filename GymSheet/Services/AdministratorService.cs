using GymSheet.Data;
using GymSheet.Models;
using GymSheet.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GymSheet.Services
{
    public class AdministratorService : Service<Administrator>, IAdministratorService
    {
        private readonly Context _context;

        public AdministratorService(Context context) : base(context)
        {
            _context = context;
        }

        public Task<bool> HasAny(string email, string password)
        {
            return _context.Administrators.AnyAsync(a => a.Email == email && a.Password == password);
        }

        public async Task<Administrator> FindByEmail(string email)
        {
            return await _context.Administrators.FirstOrDefaultAsync(a => a.Email == email);
        }
    }
}
