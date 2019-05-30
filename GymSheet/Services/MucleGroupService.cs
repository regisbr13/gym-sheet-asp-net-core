using System.Linq;
using System.Threading.Tasks;
using GymSheet.Data;
using GymSheet.Models;
using GymSheet.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymSheet.Services
{
    public class MuscleGroupService : Service<MuscleGroup>, IMuscleGroupService
    {
        private readonly Context _context;

        public MuscleGroupService(Context context) : base(context)
        {
            _context = context;
        }

        // Método para validação remota
        public async Task<bool> HasAnyName(string name)
        {
            return await _context.MuscleGroups.AnyAsync(mg => mg.Name.ToUpper() == name.ToUpper());
        }
    }
}
