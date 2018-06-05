using RentApp.Persistance.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentApp.Persistance.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IServiceRepository Services { get; set; }
        IAppUserRepository AppUsers { get; set; }
        ITypeRepository Types { get; set; }
        IBranchRepository Branches { get; set; }
        IGradingRepository Gradings { get; set; }
        IPriceItemRepository PriceItems { get; set; }
        IPriceListRepository PriceLists { get; set; }
        IReservationRepository Reservations { get; set; }
        IVehicleRepository Vehicles { get; set; }

        int Complete();
    }
}
