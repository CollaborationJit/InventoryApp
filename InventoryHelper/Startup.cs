using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Model.Models;
using Model;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace InventoryHelper
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
           options
           .UseLazyLoadingProxies()
           .UseSqlServer("Server=tcp:inventoryhelper.database.windows.net,1433;Initial Catalog=InventoryHelper;Persist Security Info=False;User ID=inventoryadmin;Password=Huurb101!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
            services.AddMvc()
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("MVC was not used!");
            });
        }

        private void InitTestData(ApplicationDbContext context)
        {
            #region Add Contacts Test Data

            Contact johnWall = new Contact
            {
                FirstName = "John",
                LastName = "Wall",
                Email = "johnwall@printers.co",
                PhoneNumber = "+14152343121"
            };
            context.Contacts.Add(johnWall);

            Contact jerryMcJerrison = new Contact
            {
                FirstName = "Jerry",
                LastName = "McJerrison",
                Email = "jerry@McJerrisons.com",
                PhoneNumber = "+14153421354"
            };
            context.Contacts.Add(jerryMcJerrison);

            Contact susie = new Contact
            {
                FirstName = "Susie",
                LastName = "Serious",
                Email = "Susie@Asus.com",
                PhoneNumber = "+14153423454"
            };
            context.Contacts.Add(susie);

            Contact camille = new Contact
            {
                FirstName = "Camille",
                LastName = "Johnson",
                Email = "camille@microsoft.com",
                PhoneNumber = "+14153532153"
            };
            context.Contacts.Add(camille);

            Contact perry = new Contact
            {
                FirstName = "Perry",
                LastName = "Telecorny",
                Email = "perry@corporatephones.com",
                PhoneNumber = "+14152121354"
            };
            context.Contacts.Add(perry);

            #endregion Contacts

            #region Test Vendor Data
            Vendor printerCo = new Vendor
            {
                Name = "Printers.co",
                Contact = johnWall
            };
            context.Vendors.Add(printerCo);

            Vendor asus = new Vendor
            {
                Name = "Asus",
                Contact = susie
            };
            context.Vendors.Add(asus);

            Vendor mcJerrisons = new Vendor
            {
                Name = "The McJerrsions Co.",
                Contact = jerryMcJerrison
            };
            context.Vendors.Add(mcJerrisons);

            Vendor corporatePhoneCo = new Vendor
            {
                Name = "Corporate Phone Co.",
                Contact = perry
            };
            context.Vendors.Add(corporatePhoneCo);

            Vendor microsoft = new Vendor
            {
                Name = "Microsoft",
                Contact = camille
            };

            context.Vendors.Add(microsoft);
            #endregion

            #region Test Inventory Items
            context.Items.Add(new InventoryItem
            {
                Title = "1920x1600 4K Monitor",
                Vendor = asus,
                Type = InventoryItem.ItemType.MONITOR,
                SerialNo = "12312414352141",
                Quantity = 50,
                QuantityAvailable = 2
            });

            context.Items.Add(new InventoryItem
            {
                Title = "1280x800 Monitor",
                Vendor = asus,
                Type = InventoryItem.ItemType.MONITOR,
                SerialNo = "1231153634642",
                Quantity = 40,
                QuantityAvailable = 0
            });

            context.Items.Add(new InventoryItem
            {
                Title = "Optical Mouse",
                Vendor = asus,
                Type = InventoryItem.ItemType.ACCESSORIES,
                SerialNo = "1241352463436",
                Quantity = 200,
                QuantityAvailable = 42
            });

            context.Items.Add(new InventoryItem
            {
                Title = "ASUS Desktop",
                Vendor = asus,
                Type = InventoryItem.ItemType.DESKTOP,
                SerialNo = "123143264as323asasdfe2",
                Quantity = 150,
                QuantityAvailable = 9
            });

            context.Items.Add(new InventoryItem
            {
                Title = "Microsoft Office 2019 License Key",
                Vendor = microsoft,
                Type = InventoryItem.ItemType.DESKTOP,
                SerialNo = "sfasdfeaceasefafseaes",
                Quantity = 150,
                QuantityAvailable = 9
            });


            #endregion

            context.SaveChanges();
        }
    }
}
