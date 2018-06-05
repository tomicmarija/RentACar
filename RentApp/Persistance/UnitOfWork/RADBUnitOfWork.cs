using RentApp.Persistance.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Unity.Attributes;

namespace RentApp.Persistance.UnitOfWork
{
    public class RADBUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        [Dependency]
        public IServiceRepository Services { get; set; }
        [Dependency]
        public IAppUserRepository AppUsers { get; set; }
        [Dependency]
        public ITypeRepository Types { get; set; }
        [Dependency]
        public IBranchRepository Branches { get; set; }
        [Dependency]
        public IGradingRepository Gradings { get; set; }
        [Dependency]
        public IPriceItemRepository PriceItems { get; set; }
        [Dependency]
        public IPriceListRepository PriceLists { get; set; }
        [Dependency]
        public IReservationRepository Reservations { get; set; }
        [Dependency]
        public IVehicleRepository Vehicles { get; set; }

        public RADBUnitOfWork(DbContext context)
        {
            _context = context;
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}