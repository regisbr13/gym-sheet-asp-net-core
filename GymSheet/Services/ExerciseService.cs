using GymSheet.Data;
using GymSheet.Models;
using GymSheet.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymSheet.Services
{
    public class ExerciseService : Service<Exercise>, IExerciseService
    {
        private readonly Context _context;

        public ExerciseService(Context context) : base(context)
        {
            _context = context;
        }

        // Listar todos:
        public new async Task<List<Exercise>> FindAllAsync()
        {
            return await _context.Excercises.Include(e => e.MuscleGroup).ToListAsync();
        }
    }
}
