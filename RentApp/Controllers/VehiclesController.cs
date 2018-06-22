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
using System.Web;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
<<<<<<< HEAD
using System.Threading;
=======
>>>>>>> dc24c4590cef0f34dea85dc7e96aaded3ee010ce

namespace RentApp.Controllers
{
    public class VehiclesController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private Mutex mutex = new Mutex();

        public VehiclesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/Vehicles
        public IEnumerable<Vehicle> GetVehicles()
        {
            mutex.WaitOne();
            IEnumerable<Vehicle> vehicles=  unitOfWork.Vehicles.GetAll();
            mutex.ReleaseMutex();
            return vehicles;
        }

        public IEnumerable<Vehicle> GetServiceVehicles(int serviceId)
        {
<<<<<<< HEAD
            mutex.WaitOne();
            IEnumerable<Vehicle> vehicles =  unitOfWork.Vehicles.GetAll().Where(v => v.ServiceId == serviceId);
            mutex.ReleaseMutex();
            return vehicles;
        }

        [HttpGet]
        public IEnumerable<Vehicle> GetVehicles(int pageIndex, int pageSize)
        {
            mutex.WaitOne();
            IEnumerable<Vehicle> v = unitOfWork.Vehicles.GetAll(pageIndex, pageSize);
            mutex.ReleaseMutex();
            return v;
=======
            return unitOfWork.Vehicles.GetAll().Where(v => v.ServiceId == serviceId);
>>>>>>> dc24c4590cef0f34dea85dc7e96aaded3ee010ce
        }


        //GET: api/Vehicles/5
<<<<<<< HEAD
        [ResponseType(typeof(Vehicle))]
=======
       [ResponseType(typeof(Vehicle))]
>>>>>>> dc24c4590cef0f34dea85dc7e96aaded3ee010ce
        public IHttpActionResult GetVehicle(int id)
        {
            mutex.WaitOne();
            Vehicle vehicle = unitOfWork.Vehicles.Get(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            mutex.ReleaseMutex();
            return Ok(vehicle);
        }

        // PUT: api/Vehicles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVehicle(int id, Vehicle vehicle)
        {
            mutex.WaitOne();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vehicle.Id)
            {
                return BadRequest();
            }
            try
            {
                unitOfWork.Vehicles.Update(vehicle);
                unitOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            mutex.ReleaseMutex();
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Vehicles
        [Authorize(Roles = "Manager")]
        [ResponseType(typeof(Vehicle))]
        public async Task<IHttpActionResult> PostVehicle()
        {
<<<<<<< HEAD
            mutex.WaitOne();
=======
>>>>>>> dc24c4590cef0f34dea85dc7e96aaded3ee010ce
            Vehicle vehicle = new Vehicle();
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/Content/images/vehicles/");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                var f = HttpContext.Current.Request.Files[0];
                FileInfo ff = new FileInfo(f.FileName);
                var fileName = Guid.NewGuid() + ff.Extension;
                var fullPath = root + fileName;

                if (File.Exists(fullPath))
                {
                    fileName = Guid.NewGuid() + ff.Extension;
                    fullPath = root + fileName;
                }

                var relativePath = "/Content/images/vehicles/";
                f.SaveAs(fullPath);

                if (HttpContext.Current.Request.Form.Count > 0)
                {

                    vehicle = JsonConvert.DeserializeObject<Vehicle>(HttpContext.Current.Request.Form[0]);
                    vehicle.Picture = relativePath + fileName;
                    vehicle.Enable = true;
                }
                else
                {
                    //formData se nije popunio!
                }

            }
            catch (System.Exception e)
            {
                //
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.Vehicles.Add(vehicle);
            unitOfWork.Complete();
            mutex.ReleaseMutex();
            return CreatedAtRoute("DefaultApi", new { id = vehicle.Id }, vehicle);
        }

        // DELETE: api/Vehicles/5
        [ResponseType(typeof(Vehicle))]
        public IHttpActionResult DeleteVehicle(int id)
        {
            mutex.WaitOne();
            Vehicle vehicle = unitOfWork.Vehicles.Get(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            unitOfWork.Vehicles.Remove(vehicle);
            unitOfWork.Complete();
            mutex.ReleaseMutex();
            return Ok(vehicle);
        }

        private bool VehicleExists(int id)
        {
            mutex.WaitOne();
            bool ret =  unitOfWork.Vehicles.Get(id) != null;
            mutex.ReleaseMutex();
            return ret;
        }
        
    }
}