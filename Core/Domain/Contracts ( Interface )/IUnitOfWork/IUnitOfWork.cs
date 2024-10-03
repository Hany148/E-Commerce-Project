using Domain.Contracts.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.IUnitOfWork
{
    public interface IUnitOfWork
    {
        public Task<int> ToSaveChanges();

        public IGenericRepository<Tenity, Tkey> GetRepository<Tenity, Tkey>() where Tenity : BaseEntity<Tkey>;

        
    }
}
