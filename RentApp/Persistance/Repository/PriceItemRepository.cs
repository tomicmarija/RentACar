using RentApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RentApp.Persistance.Repository
{
    public class PriceItemRepository : Repository<PriceItem, int>, IPriceItemRepository
    {
        public PriceItemRepository(DbContext context) : base(context)
        {
        }

        public override IEnumerable<PriceItem> GetAll()
        {
            return RADBContext.PriceItems.Include(pi => pi.Vehicle).OrderBy(pi => pi.Id);
        }

        public IEnumerable<PriceItem> GetAll(int pageIndex, int pageSize)
        {
            IQueryable<PriceItem> priceItems = RADBContext.PriceItems.Include(pi => pi.Vehicle).OrderBy( pi => pi.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return priceItems.ToList();
        }

        protected RADBContext RADBContext { get { return context as RADBContext; } }
    }
}