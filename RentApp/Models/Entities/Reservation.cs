using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentApp.Models.Entities
{
    public class Reservation
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set;  }

        public DateTime EndDate { get; set; }


        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

  
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }


        public int StartBranchId { get; set; }
        public Branch StartBranch { get; set; }

   
        public int EndBranchId { get; set; }
        public Branch EndBranch { get; set; }

    }
}