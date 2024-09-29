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
        private readonly StoreContex _storeContex;
        private readonly Dictionary<string, object> _repositry;

        public UnitOfWork(StoreContex storeContex)
        {
            _storeContex = storeContex;
            _repositry = new();
        }

        public IGenericRepository<Tenity, Tkey> GetRepository<Tenity, Tkey>() where Tenity : BaseEntity<Tkey>
        {
            var TypeName = typeof(Tenity).Name;

            if (_repositry.ContainsKey(TypeName))
            {
                return (IGenericRepository<Tenity, Tkey>) _repositry[TypeName];
            }

            var repo = new GenericRepository<Tenity, Tkey>(_storeContex);

            _repositry[TypeName] = repo;

            return repo;
        }

        public async Task<int> ToSaveChanges()
        {
           return await _storeContex.SaveChangesAsync();
        }
    }
}
