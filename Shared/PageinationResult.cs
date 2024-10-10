using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record PageinationResult<TData> (int pageIndex , int pageSize , int totalCountOfAllProdcut , IEnumerable<TData> Data)
    {
    }
}
