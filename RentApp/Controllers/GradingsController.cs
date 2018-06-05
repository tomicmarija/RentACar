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
    public class GradingsController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public GradingsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/Gradings
        public IEnumerable<Grading> GetGradings()
        {
            return unitOfWork.Gradings.GetAll();
        }

        // GET: api/Gradings/5
        [ResponseType(typeof(Grading))]
        public IHttpActionResult GetGrading(int id)
        {
            Grading grading = unitOfWork.Gradings.Get(id);
            if (grading == null)
            {
                return NotFound();
            }

            return Ok(grading);
        }

        // PUT: api/Gradings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGrading(int id, Grading grading)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != grading.Id)
            {
                return BadRequest();
            }



            try
            {
                unitOfWork.Gradings.Update(grading);
                unitOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradingExists(id))
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

        // POST: api/Gradings
        [ResponseType(typeof(Grading))]
        public IHttpActionResult PostGrading(Grading grading)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.Gradings.Add(grading);
            unitOfWork.Complete();

            return CreatedAtRoute("DefaultApi", new { id = grading.Id }, grading);
        }

        // DELETE: api/Gradings/5
        [ResponseType(typeof(Grading))]
        public IHttpActionResult DeleteGrading(int id)
        {
            Grading grading = unitOfWork.Gradings.Get(id);
            if (grading == null)
            {
                return NotFound();
            }

            unitOfWork.Gradings.Remove(grading);
            unitOfWork.Complete();

            return Ok(grading);
        }

        private bool GradingExists(int id)
        {
            return unitOfWork.Gradings.Get(id) != null;
        }
    }
}