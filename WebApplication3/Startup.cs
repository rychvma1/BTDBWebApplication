using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BTDB.KVDBLayer;
using BTDB.ODBLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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
            var json = File.ReadAllText(@"App_Data\settings.json");
            var path = JsonConvert.DeserializeObject<Settings>(json);

            services.Configure<Settings>(Configuration);
            services.AddScoped(sp => sp.GetService<IOptionsSnapshot<Settings>>().Value);
            
            if (!path.DirPath.Equals(""))
            {
                IKeyValueDB kvDb = new KeyValueDB(new OnDiskFileCollection(path.DirPath));
                IObjectDB db = new ObjectDB();
                db.Open(kvDb, false); 
                services.AddSingleton(db);
                services.AddSingleton<IBaseDataService, BaseDataService>();
                // services.AddSingleton<ICustomVisitor, CustomVisitor>();
                services.AddSingleton<ISpecificSingletonDataService, SpecificSingletonSingletonDataService>();
                services.AddSingleton<ISpecificRelationDataService, SpecificRelationDataService>();
            }
//            services.AddSingleton<Func<IObjectDBTransaction, IUserTable>>(initDB(db));
            services.AddControllersWithViews();
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
                //table
                creator = tr.InitRelation<IUserTable>("UserTable");
                var customObjTable = creator(tr);

                customObjTable.Insert(new User
                {
                    UserId = 1, Name = "admin", Age = 100, Gender = Gender.Male, Addresses = new List<string> {"Brno"},
                    ByteArray = Encoding.ASCII.GetBytes("admin")
                });
                customObjTable.Insert(new User
                {
                    UserId = 2, Name = "Ema", Age = 25, Gender = Gender.Female, Addresses = new List<string> {"Kosice"},
                    ByteArray = Encoding.ASCII.GetBytes("Ema")
                });
                customObjTable.Insert(new User
                {
                    UserId = 3, Name = "Nick", Age = 26, Gender = Gender.Male, Addresses = new List<string> {"London"},
                    ByteArray = Encoding.ASCII.GetBytes("Nick")
                });

                // dictionary Id2UserClass
                var rootName = tr.Singleton<Id2UserClass>();
                var dict = rootName.Id2User;

                dict.Add(1,
                    new User
                    {
                        UserId = 1, Name = "Matus1", Age = 24, Gender = Gender.Male,
                        Addresses = new List<string> {"HK", "KE"}
                    });
                dict.Add(2, new User {UserId = 2, Name = "Matus2", Age = 24, Gender = Gender.Male});
                dict.Add(3, new User {UserId = 3, Name = "Matus3", Age = 24, Gender = Gender.Male});
                dict.Add(4, new User {UserId = 4, Name = "Matus4", Age = 24, Gender = Gender.Male});
                dict.Add(5, new User {UserId = 5, Name = "Matus5", Age = 24, Gender = Gender.Male});

                rootName.Users = new List<User>
                {
                    new User
                    {
                        UserId = 1, Name = "admin", Age = 100, Gender = Gender.Male, Addresses = new List<string> {"BA"}
                    },
                    new User {UserId = 2, Name = "Ema", Age = 25, Gender = Gender.Female},
                    new User {UserId = 3, Name = "Nick", Age = 26, Gender = Gender.Male}
                };

                // dictionary DateTimeToUserClass
                var dateToUser = tr.Singleton<DateTimeToUserClass>();
                var dateTimeToUserDictionary = dateToUser.DateTimeToUser;

                dateTimeToUserDictionary.Add(DateTime.MinValue, new User
                {
                    UserId = 1,
                    Name = "Matus1",
                    Age = 24,
                    Gender = Gender.Male,
                    Addresses = new List<string> {"HK", "KE"}
                });
                dateTimeToUserDictionary.Add(DateTime.Now,
                    new User {UserId = 2, Name = "Matus2", Age = 24, Gender = Gender.Male});

                // dictionary KeyObjToUserClass
                var keyObjToUser = tr.Singleton<KeyObjToUserClass>();
                var keyDict = keyObjToUser.KeyObjToUser;

                keyDict.Add(new KeyObj
                    {
                        DateTime = DateTime.Now,
                        Gender = Gender.Male,
                        Id = UInt64.MinValue
                    },
                    new User
                    {
                        UserId = 1,
                        Name = "Matus1",
                        Age = 24,
                        Gender = Gender.Male,
                        Addresses = new List<string> {"HK", "KE"}
                    });
                keyDict.Add(new KeyObj
                    {
                        DateTime = DateTime.MaxValue,
                        Gender = Gender.Female,
                        Id = UInt64.MaxValue
                    },
                    new User
                    {
                        UserId = 2,
                        Name = "Matus2",
                        Age = 124,
                        Gender = Gender.Male,
                        Addresses = new List<string> {"BA", "KE"}
                    });

                // dictionary UlongToStringClass
                var ulongToString = tr.Singleton<UlongToStringClass>();
                var ulongToStringDictionary = ulongToString.UlongToString;

                ulongToStringDictionary.Add(0, "test 0");
                ulongToStringDictionary.Add(1, "test 1");

                // list ListOfStringsClass
                var listOfStrings = tr.Singleton<ListOfStringsClass>();
                listOfStrings.ListOfStrings = new List<string>
                {
                    "test 0", 
                    "test 1"
                };

                // list ListOfIntegersClass
                var listOfIntegers = tr.Singleton<ListOfIntegersClass>();
                listOfIntegers.ListOfIntegers = new List<int>
                {
                   0,
                   1
                };

                tr.Commit();
            }

            return creator;
        }
    }
}