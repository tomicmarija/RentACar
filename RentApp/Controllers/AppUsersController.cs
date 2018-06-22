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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json;
<<<<<<< HEAD
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading;
using RentApp.Services;
=======
>>>>>>> dc24c4590cef0f34dea85dc7e96aaded3ee010ce

namespace RentApp.Controllers
{
    public class AppUsersController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private RADBContext rb = new RADBContext();
<<<<<<< HEAD
        private Mutex mutex = new Mutex();
=======
>>>>>>> dc24c4590cef0f34dea85dc7e96aaded3ee010ce

        public AppUsersController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/AppUsers
        public IEnumerable<AppUser> GetAppUsers()
        {
            mutex.WaitOne();
            IEnumerable<AppUser> appUsers = unitOfWork.AppUsers.GetAll();
            mutex.ReleaseMutex();
            return appUsers;

        }

        [Route("api/AppUsers/GetManagers")]
        public IEnumerable<AppUser> GetAllManagers()
        {
            mutex.WaitOne();
            List<IdentityRole> userRoles = rb.Roles.Where(n => n.Name == "Manager").ToList();
            List<IdentityUserRole> idur = userRoles[0].Users.ToList();
            List<string> managerIds = new List<string>();

            foreach(var iur in idur)
            {
                managerIds.Add(iur.UserId); //user name-ovi u appUser tabeli
            }
            List<RAIdentityUser> raIdentityUsers = new List<RAIdentityUser>();

            foreach(string mId in managerIds)
            {   
                raIdentityUsers.Add(rb.Users.Where(u => u.Id == mId).FirstOrDefault()); //AppUserId == Id od AppUser tabele
            }

            List<AppUser> allUsers = new List<AppUser>();
            IEnumerable<AppUser> allU = unitOfWork.AppUsers.GetAll();

           
            foreach(AppUser au in allU)
            {
                foreach (RAIdentityUser user in raIdentityUsers)
                {
                    if(au.Id == user.AppUserId)
                    {
                        allUsers.Add(au);
                    }
                }
            }

            IEnumerable<AppUser> allManagers = allUsers;
            mutex.ReleaseMutex();
            return allManagers;
        }

        // GET: api/AppUsers/5
        [Authorize]
        [ResponseType(typeof(AppUser))]
        public IHttpActionResult GetAppUser(int id)
        {
<<<<<<< HEAD
            mutex.WaitOne();
            if (id == 0)
            {
                id = rb.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).AppUserId;
            }
=======
            var username = User.Identity.Name;
            var user = rb.Users.FirstOrDefault(u => u.UserName == username);
            id = rb.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).AppUserId;
>>>>>>> dc24c4590cef0f34dea85dc7e96aaded3ee010ce

            AppUser appUser = unitOfWork.AppUsers.Get(id);
            if (appUser == null)
            {
                return NotFound();
            }
            mutex.ReleaseMutex();
            return Ok(appUser);
        }

        // PUT: api/AppUsers/5
        [ResponseType(typeof(AppUser))]
        public async Task<IHttpActionResult> PutAppUser()
        {
<<<<<<< HEAD
            mutex.WaitOne();
            AppUser appUser = new AppUser();

            if (!Request.Content.IsMimeMultipartContent())
=======
            AppUser appUser = new AppUser();

            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/Content/images/users/");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                var f = HttpContext.Current.Request.Files[0];
                FileInfo ff = new FileInfo(f.FileName);
                var fileName = Guid.NewGuid() + ff.Extension;
                var fullPath = root + fileName;

                if (System.IO.File.Exists(fullPath))
                {
                    fileName = Guid.NewGuid() + ff.Extension;
                    fullPath = root + fileName;
                }

                var relativePath = "/Content/images/users/";
                f.SaveAs(fullPath);

                if (HttpContext.Current.Request.Form.Count > 0)
                {
                    appUser = JsonConvert.DeserializeObject<AppUser>(HttpContext.Current.Request.Form[0]);
                    appUser.DocumentPhoto = relativePath + fileName;
                }
                else
                {
                    //ukoliko se form data nije popunilo
                }

            }
            catch (System.Exception e)
            {
                appUser = JsonConvert.DeserializeObject<AppUser>(HttpContext.Current.Request.Form[0]);
            }



            if (!ModelState.IsValid)
>>>>>>> dc24c4590cef0f34dea85dc7e96aaded3ee010ce
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

<<<<<<< HEAD
            string root = HttpContext.Current.Server.MapPath("~/Content/images/users/");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                var f = HttpContext.Current.Request.Files[0];
                FileInfo ff = new FileInfo(f.FileName);
                var fileName = Guid.NewGuid() + ff.Extension;
                var fullPath = root + fileName;

                if (System.IO.File.Exists(fullPath))
                {
                    fileName = Guid.NewGuid() + ff.Extension;
                    fullPath = root + fileName;
                }

                var relativePath = "/Content/images/users/";
                f.SaveAs(fullPath);

                if (HttpContext.Current.Request.Form.Count > 0)
                {
                    appUser = JsonConvert.DeserializeObject<AppUser>(HttpContext.Current.Request.Form[0]);
                    appUser.DocumentPhoto = relativePath + fileName;
                }
                else
                {
                    //ukoliko se form data nije popunilo
                }
=======


            /*if (id != appUser.Id)
            {
                return BadRequest();
            }*/
>>>>>>> dc24c4590cef0f34dea85dc7e96aaded3ee010ce

            }
            catch (System.Exception e)
            {
                appUser = JsonConvert.DeserializeObject<AppUser>(HttpContext.Current.Request.Form[0]);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                unitOfWork.AppUsers.Update(appUser);
                unitOfWork.Complete();
                if (appUser.Approved)
                {
                    ISmtpService smtpService = new SmtpService();
                    string email = rb.Users.FirstOrDefault(u => u.AppUserId == appUser.Id).Email;
                    string subject = "Account approvement";
                    string body = string.Format("Hello from admin team! \n Your account\n\tFullname:{0} {1}\n\tDate of Birth: {2}\n is approved!Now, You can reservate vehicles from site.", appUser.FirstName, appUser.LastName, appUser.DateOfBirth);
                    smtpService.SendMail(subject, body, email);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppUserExists(appUser.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

<<<<<<< HEAD
            mutex.ReleaseMutex();
=======
            //return CreatedAtRoute("DefaultApi", new { id = appUser.Id }, appUser);

            //return StatusCode(HttpStatusCode.NoContent);
>>>>>>> dc24c4590cef0f34dea85dc7e96aaded3ee010ce
            return Ok(appUser);
        }

        // POST: api/AppUsers
        [ResponseType(typeof(AppUser))]
        public IHttpActionResult PostAppUser(AppUser appUser)
        {
            mutex.WaitOne();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            appUser.FullName = appUser.FirstName + appUser.LastName;

            unitOfWork.AppUsers.Add(appUser);
            unitOfWork.Complete();

            mutex.ReleaseMutex();
            return CreatedAtRoute("DefaultApi", new { id = appUser.Id }, appUser);
        }

        // DELETE: api/AppUsers/5
        [ResponseType(typeof(AppUser))]
        public IHttpActionResult DeleteAppUser(int id)
        {
            mutex.WaitOne();
            AppUser appUser = unitOfWork.AppUsers.Get(id);
            if (appUser == null)
            {
                return NotFound();
            }

            unitOfWork.AppUsers.Remove(appUser);
            unitOfWork.Complete();

            mutex.ReleaseMutex();
            return Ok(appUser);
        }

        private bool AppUserExists(int id)
        {
            mutex.WaitOne();
            bool ret =  unitOfWork.AppUsers.Get(id) != null;
            mutex.ReleaseMutex();
            return ret;
        }
    }
}