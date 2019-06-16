using GymSheet.Data;
using GymSheet.Models;
using GymSheet.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GymSheet.Services
{
    public class ExerciseListService : Service<ExerciseList>, IExerciseListService
    {
        private readonly Context _context;

        public ExerciseListService(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> HasExerciseList(int sheetId, int exerciseId)
        {
            return await _context.ExerciseLists.AnyAsync(x => x.SheetId == sheetId && x.ExerciseId == exerciseId);
        }
    }
}
