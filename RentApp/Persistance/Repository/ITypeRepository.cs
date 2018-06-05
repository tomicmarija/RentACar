using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentApp.Persistance.Repository
{
    public interface ITypeRepository : IRepository<Models.Entities.Type, int>
    {
        IEnumerable<Models.Entities.Type> GetAll(int pageIndex, int pageSize);
    }
}
