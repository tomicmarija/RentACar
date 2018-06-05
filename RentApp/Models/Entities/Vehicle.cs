using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentApp.Models.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Model { get; set; }

        [Required]
        [StringLength(30)]
        public string Manufacturer { get; set; }

        [Required]
        public int YearOfMaking { get; set; }

        public string Picture { get; set; }

        [Required]
        [StringLength(200)]
        public string Descritpion { get; set; }

        public bool Enable { get; set; }

        
        public int ServiceId { get; set; }
        public Service Service { get; set; }

        public int TypeId { get; set; }
        public Type Type { get; set; }

        public ICollection<Reservation> Reservations { get; set; }




    }
}