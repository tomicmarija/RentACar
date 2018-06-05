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
    public class PriceListsController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public PriceListsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/PriceLists
        public IEnumerable<PriceList> GetPriceLists()
        {
            return unitOfWork.PriceLists.GetAll();
        }

        // GET: api/PriceLists/5
        [ResponseType(typeof(PriceList))]
        public IHttpActionResult GetPriceList(int id)
        {
            PriceList priceList = unitOfWork.PriceLists.Get(id);
            if (priceList == null)
            {
                return NotFound();
            }

            return Ok(priceList);
        }

        // PUT: api/PriceLists/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPriceList(int id, PriceList priceList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != priceList.Id)
            {
                return BadRequest();
            }



            try
            {
                unitOfWork.PriceLists.Update(priceList);
                unitOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PriceListExists(id))
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

        // POST: api/PriceLists
        [ResponseType(typeof(PriceList))]
        public IHttpActionResult PostPriceList(PriceList priceList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.PriceLists.Add(priceList);
            unitOfWork.Complete();

            return CreatedAtRoute("DefaultApi", new { id = priceList.Id }, priceList);
        }

        // DELETE: api/PriceLists/5
        [ResponseType(typeof(PriceList))]
        public IHttpActionResult DeletePriceList(int id)
        {
            PriceList priceList = unitOfWork.PriceLists.Get(id);
            if (priceList == null)
            {
                return NotFound();
            }

            unitOfWork.PriceLists.Remove(priceList);
            unitOfWork.Complete();

            return Ok(priceList);
        }

        private bool PriceListExists(int id)
        {
            return unitOfWork.PriceLists.Get(id) != null;
        }
    }
}