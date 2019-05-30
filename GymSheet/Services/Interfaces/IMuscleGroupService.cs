using GymSheet.Models;
using System.Threading.Tasks;

namespace GymSheet.Services.Interfaces
{
    public interface IMuscleGroupService : IService<MuscleGroup>
    {
        Task<bool> HasAnyName(string name);
    }
}
