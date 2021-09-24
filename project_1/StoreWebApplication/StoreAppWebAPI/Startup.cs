using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreAppDBContext.Models;
using Microsoft.AspNetCore.Rewrite;
using StoreAppBusiness.Interfaces;
using StoreAppBusiness.Repositories;
using StoreAppModels.ViewModels;

namespace StoreAppWebAPI {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddScoped<IOrderRepo, OrderRepo>();
            services.AddScoped<ICustomerRepo, CustomerRepo>();
            services.AddScoped<IStoreRepo, StoreRepo>();
            services.AddScoped<IStoreInvRepo, StoreInventoryRepo>();
            services.AddControllers();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StoreAppWebAPI", Version = "v1" });
            });
            services.AddDbContext<StoreDBContext>(options => {
                //if db options is already configured, dont do anything otherwise use the connection string I have in secrets.json
                if (!options.IsConfigured) {
                    options.UseSqlServer(Configuration.GetConnectionString("MyDB"));
                }
            });//end addition here
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StoreAppWebAPI v1"));
            }
            //Thank you, Davian
            //added by Blake on 9/14/2021-- this is to enable default text-only handlers for common error status codes
            //to show the different status eg 400, 200, 300
            app.UseStatusCodePages();

            app.UseHttpsRedirection();
            //Thank you, Davian
            //added by Blake on 9/14/2021
            // use this to redirect to the index HTML for any random path
            app.UseRewriter(new RewriteOptions()
                .AddRedirect("^$", "index.html"));
            //Thank you, Davian
            //all are referenced in vid on sep 10 2021 at 22:00
            //added by Blake on 9/14/2021
            //use the .js static files (find out what 'static' means )
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
