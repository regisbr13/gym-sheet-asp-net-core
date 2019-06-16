using GymSheet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymSheet.Services.Interfaces
{
    interface IExerciseListService : IService<ExerciseList>
    {
        Task<bool> HasExerciseList(int sheetId, int exerciseId);
    }
}
