using System.Linq.Expressions;
using Web_Api_LPasto_ASP_NET_Core.Database.Models;

namespace Web_Api_LPasto_ASP_NET_Core.Database.Services
{
    public interface IBaseRepo<T> where T : BaseModel
    {
        Task<List<T>> GetAllModelsAsync();  

        Task<T> GetModelByIdAsync(int id);
        Task<T> GetModelByIdAsync<TInclude>(List<Expression<Func<T, TInclude>>> includeExpressions, int id);
        Task<bool> DeleteModelByIdAsync(int id);
        Task<bool> AddUpdateModelAsync(T model);

        Task<List<T>> GetAllModelsAsync<TInclude>(List<Expression<Func<T, TInclude>>> includeExpressions);
        void DisposeContext();
    }
}
