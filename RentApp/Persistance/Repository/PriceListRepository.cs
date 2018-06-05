using RentApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RentApp.Persistance.Repository
{
    public class PriceListRepository : Repository<PriceList, int>, IPriceListRepository
    {
        public PriceListRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<PriceList> GetAll(int pageIndex, int pageSize)
        {
            return RADBContext.PriceLists.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        protected RADBContext RADBContext { get { return context as RADBContext; } }
    }
}