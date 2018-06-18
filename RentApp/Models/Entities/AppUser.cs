using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentApp.Models.Entities
{
    public class AppUser
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }


        public bool Approved { get; set; }


        public string DocumentPhoto { get; set; }


        public ICollection<Reservation> Reservations { get; set; }


        public ICollection<Grading> Gradings { get; set; }


        public ICollection<Service> Services { get; set; }


        public AppUser()
        {

        }

    }
}