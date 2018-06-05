﻿using RentApp.Models.Entities;
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

        public IEnumerable<PriceItem> GetAll(int pageIndex, int pageSize)
        {
            return RADBContext.PriceItems.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        protected RADBContext RADBContext { get { return context as RADBContext; } }
    }
}