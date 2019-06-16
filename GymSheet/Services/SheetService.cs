using GymSheet.Data;
using GymSheet.Models;
using GymSheet.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymSheet.Services
{
    public class SheetService : Service<Sheet>, ISheetService
    {
        private readonly Context _context;
        public SheetService(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Sheet>> FindAllAsync(int? id)
        {
            return await _context.Sheets.Include(s => s.ExerciseLists).ThenInclude(s => s.Exercise).Include(s => s.Student).ThenInclude(s => s.Objective).Include(s => s.Student).ThenInclude(s => s.Teacher).Where(s => s.StudentId == id).ToListAsync();
        }

        public async Task<bool> HasAny(int? id, string name)
        {
            return await _context.Sheets.AnyAsync(x => x.Id != id && x.Name == name);
        }
    }
}
