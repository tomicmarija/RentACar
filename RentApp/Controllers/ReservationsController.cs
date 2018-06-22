using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using RentApp.Models.Entities;
using RentApp.Persistance;
using RentApp.Persistance.UnitOfWork;

namespace RentApp.Controllers
{
    public class ReservationsController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private RADBContext rb = new RADBContext();

        public ReservationsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/Reservations
        public IEnumerable<Reservation> GetReservations(int vehicleId)
        {
            return unitOfWork.Reservations.GetAll().Where(r => r.VehicleId == vehicleId).OrderBy(r => r.StartDate).ToList();
        }

        // GET: api/Reservations/5
        //[ResponseType(typeof(Reservation))]
        //public IHttpActionResult GetReservation(int id)
        //{
        //    Reservation reservation = unitOfWork.Reservations.Get(id);
        //    if (reservation == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(reservation);
        //}

        // PUT: api/Reservations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReservation(int id, Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reservation.Id)
            {
                return BadRequest();
            }



            try
            {
                unitOfWork.Reservations.Update(reservation);
                unitOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Reservations
        [Authorize]
        [ResponseType(typeof(Reservation))]
        public IHttpActionResult PostReservation(Reservation reservation)
        {
            var username = User.Identity.Name;
            var user = rb.Users.FirstOrDefault(u => u.UserName == username);
            var id = rb.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).AppUserId;


            reservation.AppUserId = id;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.Reservations.Add(reservation);
            unitOfWork.Complete();

            return CreatedAtRoute("DefaultApi", new { id = reservation.Id }, reservation);
        }

        // DELETE: api/Reservations/5
        [ResponseType(typeof(Reservation))]
        public IHttpActionResult DeleteReservation(int id)
        {
            Reservation reservation = unitOfWork.Reservations.Get(id);
            if (reservation == null)
            {
                return NotFound();
            }

            unitOfWork.Reservations.Remove(reservation);
            unitOfWork.Complete();

            return Ok(reservation);
        }

        private bool ReservationExists(int id)
        {
            return unitOfWork.Reservations.Get(id) != null;
        }
    }
}