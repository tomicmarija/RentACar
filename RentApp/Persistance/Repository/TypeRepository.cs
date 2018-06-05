using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RentApp.Persistance.Repository
{
    public class TypeRepository : Repository<Models.Entities.Type, int>, ITypeRepository
    {
        public TypeRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Models.Entities.Type> GetAll(int pageIndex, int pageSize)
        {
            return RADBContext.Types.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        protected RADBContext RADBContext { get { return context as RADBContext; } }
    }
}