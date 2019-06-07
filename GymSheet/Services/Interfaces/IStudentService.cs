using GymSheet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymSheet.Services.Interfaces
{
    interface IStudentService : IService<Student>
    {
        new Task<List<Student>> FindAllAsync();
        Task<bool> HasAnyName(int? id, string name);
    }
}
