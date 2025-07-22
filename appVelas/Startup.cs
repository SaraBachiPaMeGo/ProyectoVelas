using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appVelas.Data;
using appVelas.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace appVelas
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
            String cadena = this.Configuration.GetConnectionString("proyektVelas");

            if (cadena ==null)
            {
                cadena = "Data Source=LAPTOP-SLC643FH;Initial Catalog=ProyektVelas;Persist Security Info=True;User ID=SA;Password=P@ssw0rdVelas1";
            }

            services.AddDbContext<Contexto>(options => options.UseSqlServer(cadena));
            services.AddTransient<RepositoryVelas>();
            services.AddMvc();
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc(
            //        name: "v1",
            //        new OpenApiInfo
            //        {
            //            Title = "Projecto Velas",
            //            Version = "v1",
            //            Description = "Confiamos en el futuro"
            //        }
            //        );
            //});

            //HelperToken helper = new HelperToken(this.Configuration);

            //services.AddAuthentication(helper.GetAuthOptions()).
            //    AddJwtBearer(helper.GetJwtOptions());
            services.AddControllers();
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

            //app.UseSwagger();

            //app.UseSwaggerUI(
            //    c =>
            //    {
            //        c.SwaggerEndpoint(
            //            url: "/swagger/v1/swagger.json",
            //            name: "Api v1"
            //            );
            //        c.RoutePrefix = "";
            //    });

            app.UseHttpsRedirection();

            app.UseRouting();
               
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
