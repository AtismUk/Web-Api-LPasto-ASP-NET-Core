using Microsoft.EntityFrameworkCore;
using Web_Api_LPasto_ASP_NET_Core.Database.Models;

namespace Web_Api_LPasto_ASP_NET_Core.Database.Services
{
    public class BaseRepo<T> : IBaseRepo<T> where T : BaseModel
    {
        private readonly DbSet<T> _dbSet;
        private readonly AppDbContext _dbContext;
        public BaseRepo(AppDbContext dbContext)
        {
            _dbSet = dbContext.Set<T>();
        }

        public async Task<bool> AddUpdateModelAsync(T model)
        {
            try
            {
                if (model.Id > 0)
                {
                    _dbSet.Add(model);
                }
                else
                {
                    _dbSet.Update(model);
                }
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteModelByIdAsync(int id)
        {
            var model = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            try
            {
                if (model != null)
                {
                    _dbSet.Remove(model);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void DisposeContext()
        {
            _dbContext.Dispose();
        }

        public async Task<List<T>> GetAllModelsAsync()
        {
            var res = await _dbSet.ToListAsync();
            return res;
        }

        public async Task<T> GetModelByIdAsync(int id)
        {
            var res = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            return res;
        }

        
    }
}
