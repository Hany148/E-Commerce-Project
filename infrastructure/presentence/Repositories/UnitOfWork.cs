using Domain.Contracts.IRepository;
using Domain.Contracts.IUnitOfWork;
using presentence.Data.DbContexts;
using System;
 using System.Collections.Generic;
 using System.Linq;
 using System.Text;
 using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _storeContext;
        private readonly Dictionary<string, object> _repositry;

        public UnitOfWork(StoreContext storeContext)
        {
            _storeContext = storeContext;
            _repositry = new();
        }

        public IGenericRepository<Tenity, Tkey> GetRepository<Tenity, Tkey>() where Tenity : BaseEntity<Tkey>
        {
            var TypeName = typeof(Tenity).Name;

            if (_repositry.ContainsKey(TypeName))
            {
                return (IGenericRepository<Tenity, Tkey>) _repositry[TypeName];
            }

            var repo = new GenericRepository<Tenity, Tkey>(_storeContext);

            _repositry[TypeName] = repo;

            return repo;
        }

        public async Task<int> ToSaveChanges()
        {
           return await _storeContext.SaveChangesAsync();
        }
    }
}
