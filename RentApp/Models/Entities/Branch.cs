using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentApp.Models.Entities
{
    public class Branch
    {
        public int Id { get; set; }

        [Required]
        public string Picture { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        public int Latitude { get; set; }

        [Required]
        public int Longitude { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; }

        [InverseProperty("StartBranch")]
        public ICollection<Reservation> StartReservations { get; set; }

        [InverseProperty("EndBranch")]
        public ICollection<Reservation> EndReservations { get; set; }
    }
}