using GymSheet.Models;
using System.Threading.Tasks;

namespace GymSheet.Services.Interfaces
{
    interface IObjectiveService : IService<Objective>
    {
        Task<bool> HasAnyName(int? id, string name);
    }
}
