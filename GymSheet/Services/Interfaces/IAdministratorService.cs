using GymSheet.Models;
using System.Threading.Tasks;

namespace GymSheet.Services.Interfaces
{
    interface IAdministratorService : IService<Administrator>
    {
        Task<bool> HasAny(string email, string password);
    }
}
