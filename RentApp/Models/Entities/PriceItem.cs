using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentApp.Models.Entities
{
    public class PriceItem
    {
        public int Id { get; set; }

        public double Price { get; set; }

  
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public int PriceListId { get; set; }
        public PriceList PriceList { get; set; }

    }
}