namespace RentApp.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using RentApp.Models.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RentApp.Persistance.RADBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RentApp.Persistance.RADBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Manager"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Manager" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "AppUser"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "AppUser" };

                manager.Create(role);
            }

            

            
            context.SaveChanges();


            /*context.AppUsers.Add(new AppUser() { FullName = "Admin Adminovic", FirstName = "Admin", LastName = "Adminovic", DateOfBirth = DateTime.Now });
            context.AppUsers.Add(new AppUser() { FullName = "AppUser AppUserovic", FirstName = "AppUser", LastName = "AppUserovic", DateOfBirth = DateTime.Now });*/
            /*context.AppUsers.Add(new AppUser() { FullName = "Marko Markovic", FirstName = "Marko", LastName = "Markovic", DateOfBirth = DateTime.Now });
            context.SaveChanges();*/
            /* context.Services.AddOrUpdate(u => u.Id, new Service() {Id = 2, Name = "Servis 1", Email = "servis1@gmail.com", Descritpion = "Opis servisa 1", UserManagerId = 3, Logo= "servis1.JPG", Approved=true});
             context.Services.AddOrUpdate(u => u.Id, new Service() {Id = 3, Name = "Servis 2", Email = "servis2@gmail.com", Descritpion = "Opis servisa 2", UserManagerId = 3, Logo = "servisb.jpg", Approved = true });
             context.SaveChanges();*/

            /* context.Types.Add(new Models.Entities.Type() {Name="Automobil"});
             context.Types.Add(new Models.Entities.Type() { Name = "Motor" });
             context.Types.Add(new Models.Entities.Type() { Name = "Kombi" });
             context.Types.Add(new Models.Entities.Type() { Name = "Kamion" });
             context.SaveChanges();*/

            //context.Vehicles.AddOrUpdate(u => u.Id, new Vehicle() { Id = 5, Model = "3008", Manufacturer = "Peugeot", YearOfMaking = 2018, Descritpion = "Peugeot 2018", Picture = "3008.jpg", Enable = true, ServiceId = 33, TypeId = 1 });
            //context.Vehicles.AddOrUpdate(u => u.Id, new Vehicle() { Id = 6, Model = "X5", Manufacturer = "BMW", YearOfMaking = 2009, Descritpion = "BMW X5", Picture = "bmwX5.png", Enable = true, ServiceId = 33, TypeId = 1 });
            //context.Vehicles.AddOrUpdate(u => u.Id, new Vehicle() { Id = 7, Model = "500L", Manufacturer = "Fiat", YearOfMaking = 2015, Descritpion = "Fiat 500L", Picture = "filat500l.jpg", Enable = true, ServiceId = 34, TypeId = 1 });
            //context.Vehicles.AddOrUpdate(u => u.Id, new Vehicle() { Id = 8, Model = "Golf 5", Manufacturer = "Volkswagen", YearOfMaking = 2010, Descritpion = "Golf 5", Picture = "golf5.jpg", Enable = true, ServiceId = 33, TypeId = 1 });
            //context.Vehicles.AddOrUpdate(u => u.Id, new Vehicle() { Id = 9, Model = "Niken", Manufacturer = "Yamaha", YearOfMaking = 2017, Descritpion = "Yamaha Niken", Picture = "niken.jpg", Enable = true, ServiceId = 33, TypeId = 2 });


            // context.PriceLists.AddOrUpdate(u => u.Id,  new PriceList() {Id = 2, StartDate = new DateTime(2018,06,01), EndDate= new DateTime(2018,07,23), ServiceId = 33});
            //context.PriceLists.AddOrUpdate(u => u.Id, new PriceList() { Id = 3, StartDate = new DateTime(2018, 06, 01), EndDate = new DateTime(2018, 07, 23), ServiceId = 34 });


            //context.PriceItems.Add( new PriceItem() { Price = 3600, VehicleId = 5, PriceListId = 2 });
            //context.PriceItems.Add(new PriceItem() { Price = 7200, VehicleId = 6, PriceListId = 2 });
            //context.PriceItems.Add(new PriceItem() { Price = 5600, VehicleId = 7, PriceListId = 3 });
            //context.PriceItems.Add(new PriceItem() { Price = 8000, VehicleId = 8, PriceListId = 2 });
            //context.PriceItems.Add(new PriceItem() { Price = 6200, VehicleId = 9, PriceListId = 2 });

            /*context.SaveChanges();

            context.Gradings.Add(new Grading() { Comment = "Servis 1 odlican.", Grade = 5, ServiceId = 33, AppUserId = 3 });
            context.Gradings.Add(new Grading() { Comment = "Servis 2 dobar.", Grade = 3, ServiceId = 34, AppUserId = 3 });

            context.SaveChanges();*/


            var userStore = new UserStore<RAIdentityUser>(context);
            var userManager = new UserManager<RAIdentityUser>(userStore);

            if (!context.Users.Any(u => u.UserName == "admin"))
            {
                var _appUser = context.AppUsers.FirstOrDefault(a => a.FullName == "Admin Adminovic");
                var user = new RAIdentityUser() { Id = "admin", UserName = "admin", Email = "admin@yahoo.com", PasswordHash = RAIdentityUser.HashPassword("admin"), AppUserId = _appUser.Id };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "Admin");
            }

            if (!context.Users.Any(u => u.UserName == "appu"))
            {

                var _appUser = context.AppUsers.FirstOrDefault(a => a.FullName == "AppUser AppUserovic");
                var user = new RAIdentityUser() { Id = "appu", UserName = "appu", Email = "appu@yahoo.com", PasswordHash = RAIdentityUser.HashPassword("appu"), AppUserId = _appUser.Id };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "AppUser");

            }

            if (!context.Users.Any(u => u.UserName == "marko"))
            {

                var _appUser = context.AppUsers.FirstOrDefault(a => a.FullName == "Marko Markovic");
                var user = new RAIdentityUser() { Id = "marko", UserName = "marko", Email = "marko__@yahoo.com", PasswordHash = RAIdentityUser.HashPassword("marko"), AppUserId = _appUser.Id };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "Manager");

            }
            

        }
    }
}
