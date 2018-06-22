using RentApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RentApp.Persistance.Repository
{
    public class VehicleRepository : Repository<Vehicle, int>, IVehicleRepository
    {
        public VehicleRepository(DbContext context) : base(context)
        {
        }

        public override IEnumerable<Vehicle> GetAll()
        {
            return RADBContext.Vehicles.Include(v => v.Type);
        }

        public IEnumerable<Vehicle> GetAll(int pageIndex, int pageSize)
        {
            IEnumerable<Vehicle> vv = RADBContext.Vehicles.OrderBy(r => r.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return vv;
        }

        protected RADBContext RADBContext { get { return context as RADBContext; } }
    }
}