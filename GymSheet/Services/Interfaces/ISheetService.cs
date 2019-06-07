using GymSheet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymSheet.Services.Interfaces
{
    interface ISheetService : IService<Sheet>
    {
        Task<List<Sheet>> FindAllAsync(int? id);
    }
}
