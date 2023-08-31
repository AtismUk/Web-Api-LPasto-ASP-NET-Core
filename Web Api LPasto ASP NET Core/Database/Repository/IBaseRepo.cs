using System.Linq.Expressions;
using Web_Api_LPasto_ASP_NET_Core.Database.Models;

namespace Web_Api_LPasto_ASP_NET_Core.Database.Services
{
    public interface IBaseRepo<T> where T : BaseModel
    {
        Task<List<T>> GetAllModelsAsync();  
        Task<List<T>> GetAllModelsIncludeAsync<TInclude>(Expression<Func<T, TInclude>> includeExpression);
        Task<List<T>> GetAllModelsIncludeAsync<TInclude, TIncludeTwo>(Expression<Func<T, TInclude>> includeExpression, Expression<Func<T, TIncludeTwo>> includeExpressionTwo);
        Task<List<T>> GetAllModelsIncludeAsync<TInclude, TIncludeTwo, TIncludeTherd>(Expression<Func<T, TInclude>> includeExpression, 
            Expression<Func<T, TIncludeTwo>> includeExpressionTwo,
            Expression<Func<T, TIncludeTherd>> IncludeExpressionTherd);

        Task<T> GetModelByIdAsync(int id);
        Task<T> GetModelByIdAsync<TInclude>(Expression<Func<T, TInclude>> includeExpression, int id);
        Task<bool> DeleteModelByIdAsync(int id);
        Task<bool> AddUpdateModelAsync(T model);
        void DisposeContext();
    }
}
