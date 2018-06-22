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

      /*  public override IEnumerable<PriceList> GetAll()
        {
            
            return RADBContext.PriceLists.Include(pl => pl.PriceItems);
        }*/

        public IEnumerable<PriceList> GetAll(int pageIndex, int pageSize)
        {
            return RADBContext.PriceLists.Include(pl => pl.PriceItems).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        protected RADBContext RADBContext { get { return context as RADBContext; } }
    }
}