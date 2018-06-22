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
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Threading;
using RentApp.Services;

namespace RentApp.Controllers
{
    public class ServicesController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private RADBContext rb = new RADBContext();
        private Mutex mutex = new Mutex();

        public ServicesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IEnumerable<Service> GetServices()
        {
            mutex.WaitOne();
            IEnumerable<Service> services =  unitOfWork.Services.GetAll();
            mutex.ReleaseMutex();
            return services;
        }


        //GET: api/Services
        [HttpGet]
        public IEnumerable<Service> GetServices(int pageIndex, int pageSize)
        {
            mutex.WaitOne();
            IEnumerable<Service> services = unitOfWork.Services.GetAll(pageIndex,pageSize).Where(service => service.Approved);
            mutex.ReleaseMutex();
            return services;
        }

        // GET: api/Services/5
        [ResponseType(typeof(Service))]
        public IHttpActionResult GetService(int id)
        {
            mutex.WaitOne();
            Service service = unitOfWork.Services.Get(id);
            if (service == null)
            {
                return NotFound();
            }
            mutex.ReleaseMutex();
            return Ok(service);
        }

        // PUT: api/Services/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutService(int id, Service service)
        {
            mutex.WaitOne();
            ISmtpService smtpService = new SmtpService();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != service.Id)
            {
                return BadRequest();
            }
            try
            {
                unitOfWork.Services.Update(service);
                unitOfWork.Complete();

                if (service.Approved)
                {
                    string email = rb.Users.FirstOrDefault(u => u.AppUserId == service.UserManagerId).Email;
                    string subject = "Service approvement";
                    string body = string.Format("Hello from admin team! \n Your service {0} is approved!", service.Name);
                    smtpService.SendMail(subject, body, email);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
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

        // POST: api/Services
        [Authorize(Roles = "Manager")]
        [ResponseType(typeof(Service))]
        public async Task<IHttpActionResult> PostService()
        {
            mutex.WaitOne();
            Service service = new Service();

            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/Content/images/");
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

                var relativePath = "/Content/images/";
                f.SaveAs(fullPath);

                if (HttpContext.Current.Request.Form.Count > 0)
                {

                    service = JsonConvert.DeserializeObject<Service>(HttpContext.Current.Request.Form[0]);
                    service.Logo = relativePath + fileName;
                }else
                {
                    //formData se nije popunio!
                }
                
            }
            catch (System.Exception e)
            {
                //
            }
            service.UserManagerId = rb.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).AppUserId;
            service.Approved = false;

            if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                unitOfWork.Services.Add(service);
                unitOfWork.Complete();
            mutex.ReleaseMutex();
            return CreatedAtRoute("DefaultApi", new { id = service.Id }, service);
        }

        // DELETE: api/Services/5
        [ResponseType(typeof(Service))]
        public IHttpActionResult DeleteService(int id)
        {
            mutex.WaitOne();
            Service service = unitOfWork.Services.Get(id);
            if (service == null)
            {
                return NotFound();
            }

            unitOfWork.Services.Remove(service);
            unitOfWork.Complete();
            mutex.ReleaseMutex();
            return Ok(service);
        }   

        private bool ServiceExists(int id)
        {
            mutex.WaitOne();
            bool ret = unitOfWork.Services.Get(id) != null;
            mutex.ReleaseMutex();
            return ret;
        }

       
    }
}