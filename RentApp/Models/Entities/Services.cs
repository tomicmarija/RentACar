using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace RentApp.Models.Entities
{
    public class Service
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public string Logo { get; set; }

        [Required]
        [StringLength(30)]
        public string Email { get; set; }

        [Required]
        [StringLength(300)]
        public string Descritpion { get; set; }

        public bool Approved { get; set; }

        public ICollection<Branch> Branches { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }

        public ICollection<PriceList> PriceLists { get; set; }

        public ICollection<Grading> Gradings { get; set; }

        public int UserManagerId { get; set; }
        public AppUser UserManager { get; set; }
    }
}