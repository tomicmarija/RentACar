using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentApp.Models.Entities
{
    public class Grading
    {
        public int Id { get; set; }


        public string Comment { get; set; }

        public int Grade { get; set; }

    
        public int ServiceId { get; set; }
        public Service Service { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}