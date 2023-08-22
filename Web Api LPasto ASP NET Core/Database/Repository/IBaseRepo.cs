using Web_Api_LPasto_ASP_NET_Core.Database.Models;

namespace Web_Api_LPasto_ASP_NET_Core.Database.Services
{
    public interface IBaseRepo<T> where T : BaseModel
    {
        public Task<List<T>> GetAllModelsAsync();
        public Task<T> GetModelByIdAsync(int id);
        public Task<bool> DeleteModelByIdAsync(int id);
        public Task<bool> AddUpdateModelAsync(T model);

        public void DisposeContext();
    }
}
