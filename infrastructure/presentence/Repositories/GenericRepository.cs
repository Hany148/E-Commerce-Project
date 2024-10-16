using Domain.Contracts.IRepository;
using Domain.Contracts___Interface__;
using presentence.Data.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class GenericRepository<Tentity, Tkey> : IGenericRepository<Tentity, Tkey> where Tentity : BaseEntity<Tkey>
    {
        private readonly StoreContext _storeContex;
        private readonly DbSet<Tentity> _dbSet;
        public GenericRepository(StoreContext storeContex)
        {
            _storeContex = storeContex;
            _dbSet = _storeContex.Set<Tentity>();
        }

        public async Task AddAsync(Tentity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(Tentity entity)
        {
              _dbSet.Remove(entity);
        }

        public async Task<Tentity?> FindByIdAysnc(Tkey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<Tentity>> GetAllAsync(bool trackChenges)
        {
            /*
              
                       if (trackChenges)
                     {
                        return await _dbSet.ToListAsync();
                     }
                    return  await _dbSet.AsNoTracking().ToListAsync();
            
             */

            return trackChenges ? await _dbSet.ToListAsync() : await _dbSet.AsNoTracking().ToListAsync();
        }

        public void Update(Tentity entity)
        {
            _dbSet.Update(entity);
        }

        public async Task<Tentity?> FindByIdAysnc(Specification<Tentity> specification)
        {
            return await SpecificationsEvaluator.GetQuery(_storeContex.Set<Tentity>(), specification).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Tentity>> GetAllAsync(Specification<Tentity> specification)
        {
           return  await SpecificationsEvaluator.GetQuery(_storeContex.Set<Tentity>(), specification).ToListAsync();

        }

        public async Task<int> CountAsync(Specification<Tentity> specification)
        {
            return await SpecificationsEvaluator.GetQuery(_dbSet , specification).CountAsync();
        }

    }
}
