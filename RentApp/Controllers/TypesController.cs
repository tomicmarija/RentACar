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
    public class TypesController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public TypesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/Types
        public IEnumerable<Models.Entities.Type> GetTypes()
        {
            return unitOfWork.Types.GetAll();
        }

        // GET: api/Types/5
        [ResponseType(typeof(Models.Entities.Type))]
        public IHttpActionResult GetType(int id)
        {
            Models.Entities.Type type = unitOfWork.Types.Get(id);
            if (type == null)
            {
                return NotFound();
            }

            return Ok(type);
        }

        // PUT: api/Types/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutType(int id, Models.Entities.Type type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != type.Id)
            {
                return BadRequest();
            }



            try
            {
                unitOfWork.Types.Update(type);
                unitOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeExists(id))
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

        // POST: api/Types
        [ResponseType(typeof(Models.Entities.Type))]
        public IHttpActionResult PostType(Models.Entities.Type type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.Types.Add(type);
            unitOfWork.Complete();

            return CreatedAtRoute("DefaultApi", new { id = type.Id }, type);
        }

        // DELETE: api/Types/5
        [ResponseType(typeof(Models.Entities.Type))]
        public IHttpActionResult DeleteType(int id)
        {
            Models.Entities.Type type = unitOfWork.Types.Get(id);
            if (type == null)
            {
                return NotFound();
            }

            unitOfWork.Types.Remove(type);
            unitOfWork.Complete();

            return Ok(type);
        }

        private bool TypeExists(int id)
        {
            return unitOfWork.Types.Get(id) != null;
        }
    }
}