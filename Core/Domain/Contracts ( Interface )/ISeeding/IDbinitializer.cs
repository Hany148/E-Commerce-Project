using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.ISeeding
{
    public interface IDbinitializer
    {
        public Task InitializAsync();
        public Task InitializIdentityAsync();
    }
}
