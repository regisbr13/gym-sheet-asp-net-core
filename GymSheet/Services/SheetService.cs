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
            return await _context.Sheets.Where(s => s.StudentId == id).ToListAsync();
        }
    }
}
