using GymSheet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymSheet.Services.Interfaces
{
    interface IExerciseService : IService<Exercise>
    {
        new Task<List<Exercise>> FindAllAsync();
    }
}
