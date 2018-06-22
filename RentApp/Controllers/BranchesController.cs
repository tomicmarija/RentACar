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
using System.Threading;

namespace RentApp.Controllers
{
    public class BranchesController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private Mutex mutex = new Mutex();

        public BranchesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        // GET: api/Branches
        public IEnumerable<Branch> GetBranches()
        {
            mutex.WaitOne();
            IEnumerable<Branch> branches = unitOfWork.Branches.GetAll();
            mutex.ReleaseMutex();
            return branches;
        }


        public IEnumerable<Branch> GetServiceBranches(int serviceId)
        {
            mutex.WaitOne();
            IEnumerable<Branch> branches =  unitOfWork.Branches.GetAll().Where(b => b.ServiceId == serviceId);
            mutex.ReleaseMutex();
            return branches;
        }
        /*
        // GET: api/Branches/5
        [ResponseType(typeof(Branch))]
        public IHttpActionResult GetBranch(int id)
        {
            Branch branch = unitOfWork.Branches.Get(id);
            if (branch == null)
            {
                return NotFound();
            }

            return Ok(branch);
        }*/

        // PUT: api/Branches/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBranch(int id, Branch branch)
        {
            mutex.WaitOne();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != branch.Id)
            {
                return BadRequest();
            }

            try
            {
                unitOfWork.Branches.Update(branch);
                unitOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchExists(id))
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

        // POST: api/Branches
        [Authorize(Roles = "Manager")]
        [ResponseType(typeof(Branch))]
        public async Task<IHttpActionResult> PostBranch()
        {
            mutex.WaitOne();
            Branch branch = new Branch();

            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/Content/images/branches/");
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

                var relativePath = "/Content/images/branches/";
                f.SaveAs(fullPath);

                if (HttpContext.Current.Request.Form.Count > 0)
                {

                    branch = JsonConvert.DeserializeObject<Branch>(HttpContext.Current.Request.Form[0]);
                    branch.Picture = relativePath + fileName;
                    //vehicle.Enable = true;
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

            unitOfWork.Branches.Add(branch);
            unitOfWork.Complete();
            mutex.ReleaseMutex();
            return CreatedAtRoute("DefaultApi", new { id = branch.Id }, branch);
        }

        // DELETE: api/Branches/5
        [ResponseType(typeof(Branch))]
        public IHttpActionResult DeleteBranch(int id)
        {
            mutex.WaitOne();
            Branch branch = unitOfWork.Branches.Get(id);
            if (branch == null)
            {
                return NotFound();
            }

            unitOfWork.Branches.Remove(branch);
            unitOfWork.Complete();
            mutex.ReleaseMutex();
            return Ok(branch);
        }

        private bool BranchExists(int id)
        {
            mutex.WaitOne();
            bool ret = unitOfWork.Branches.Get(id) != null;
            mutex.ReleaseMutex();
            return ret;
        }
    }
}