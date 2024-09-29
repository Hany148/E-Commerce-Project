global using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.IRepository
{
    public interface IGenericRepository<Tentity , Tkey> where Tentity : BaseEntity<Tkey>
    {
        public Task<Tentity?> FindByIdAysnc(Tkey id);

        public Task<IEnumerable<Tentity>> GetAllAsync(bool trackChenges);

        public Task AddAsync(Tentity entity);

        public void Update(Tentity entity);

        public void Delete (Tentity entity);




    }
}
