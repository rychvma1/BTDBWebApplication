using System;
using System.Collections.Generic;
using BTDB.KVDBLayer;
using BTDB.ODBLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication3.Models;
using WebApplication3.Services;

namespace WebApplication3
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IKeyValueDB kvDb = new InMemoryKeyValueDB();
            IObjectDB db = new ObjectDB();
            db.Open(kvDb, false); // false means that dispose of IObjectDB will not dispose IKeyValueDB
            // IObjectDBTransaction tr = db.StartTransaction(); 
            services.AddControllersWithViews();
            services.AddSingleton<ICustomDataServices, CustomDataServices>();
            services.AddSingleton<ICustomVisitor, CustomVisitor>();
            services.AddSingleton<Func<IObjectDBTransaction, IUserTable>>(initDB(db));
            services.AddSingleton<IObjectDB>(db);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static Func<IObjectDBTransaction, IUserTable> initDB(IObjectDB db)
        {
            Func<IObjectDBTransaction, IUserTable> creator;
            using (var tr = db.StartTransaction())
            {
                creator = tr.InitRelation<IUserTable>("UserTable");
                var customObjTable = creator(tr);
                customObjTable.Insert(new User {Id = 1, Name = "admin", Age = 100, Gender = Gender.male, Addresses = new List<string>{"Na Brne"}});
                customObjTable.Insert(new User {Id = 2, Name = "Ema", Age = 25, Gender = Gender.female, Addresses = new List<string> { "Kosice"}});
                customObjTable.Insert(new User {Id = 3, Name = "Nick", Age = 26, Gender = Gender.male, Addresses = new List<string> { "London"}});

                var rootName = tr.Singleton<Id2UserClass>();
                var dict = rootName.Id2User;

                dict.Add(1, new User {Id = 1, Name = "Matus1", Age = 24, Gender = Gender.male, Addresses = new List<string> { "HK", "KE"}});
                dict.Add(2, new User {Id = 2, Name = "Matus2", Age = 24, Gender = Gender.male});
                dict.Add(3, new User {Id = 3, Name = "Matus3", Age = 24, Gender = Gender.male});
                dict.Add(4, new User {Id = 4, Name = "Matus4", Age = 24, Gender = Gender.male});
                dict.Add(5, new User {Id = 5, Name = "Matus5", Age = 24, Gender = Gender.male});

                rootName.Users = new List<User>
                {
                    new User {Id = 1, Name = "admin", Age = 100, Gender = Gender.male, Addresses = new List<string> {"BA"}},
                    new User {Id = 2, Name = "Ema", Age = 25, Gender = Gender.female},
                    new User {Id = 3, Name = "Nick", Age = 26, Gender = Gender.male}
                };

                tr.Commit();
            }

            return creator;
        }
    }
}