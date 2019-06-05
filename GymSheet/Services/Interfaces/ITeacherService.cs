using GymSheet.Models;
using System.Threading.Tasks;

namespace GymSheet.Services.Interfaces
{
    interface ITeacherService : IService<Teacher>
    {
        Task<bool> HasAnyName(int? id, string name);
    }
}
