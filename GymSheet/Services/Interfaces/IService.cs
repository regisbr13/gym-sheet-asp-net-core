using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymSheet.Services.Interfaces
{
    public interface IService<TEntity> where TEntity : class
    {
        Task<List<TEntity>> FindAllAsync();
        Task<TEntity> FindByIdAsync(int? id);
        Task InsertAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(int id);
    }
}
