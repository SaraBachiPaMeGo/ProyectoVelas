using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using appVelas.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using appVelas.Service;
using appVelas.Service.Interfaces;
using appVelas.Services;
using System.Net.Http;
using System.Net.Http.Headers;

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

            services.AddScoped<RepositoryVelas>();
            services.AddScoped<RepositoryCeras>();
            services.AddScoped<RepositoryClientes>();
            services.AddScoped<RepositoryEndurecedores>();
            services.AddScoped<RepositoryFragancias>();
            services.AddScoped<RepositoryMechas>();
            services.AddScoped<RepositoryMoldes>();
            services.AddScoped<RepositoryPacks>();
            services.AddScoped<RepositoryPedidos>();
            services.AddScoped<RepositoryPigmentos>();
            services.AddScoped<RepositoryVelaFragancias>();
            services.AddScoped<RepositoryVelaPigmentos>();

            // ✅ Configuración global del HttpClientAction<IServiceProvider, HttpClient> configureClient = (sp, client) =>

            string _baseUrl =  "https://localhost:44346/api"; //Configuration["ApiSettings: BaseUrl"] ??

            services.AddHttpClient("ApiClient", client =>
            {
                client.BaseAddress = new Uri(Configuration["ApiSettings: BaseUrl"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            });

            services.AddHttpClient<ICeraService, CeraService>();
            services.AddHttpClient<IClienteService, ClienteService>();
            services.AddHttpClient<IEndurecedorService, EndurecedorService>();
            services.AddHttpClient<IFraganciaService, FraganciaService>();
            services.AddTransient<IMechaService, MechaService>();
            services.AddHttpClient<IMoldeService, MoldeService>();
            services.AddHttpClient<IPackService, PackService>();
            services.AddHttpClient<IPedidoService, PedidoService>();
            services.AddHttpClient<IPigmentoService, PigmentoService>();
            services.AddHttpClient<IVelaFraganciaService, VelaFraganciaService>();
            services.AddHttpClient<IVelaPigmentoService, VelaPigmentoService>();
            services.AddHttpClient<IVelaService, VelaService>();
                 

            // ✅ Manejador genérico que ignora certificados locales
            Func<HttpClientHandler> handler = () => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            // ✅ Registramos todos los servicios que usan HttpClient

            //services.AddHttpClient<IMechaService, MechaService>(configureClient)
            //        .ConfigurePrimaryHttpMessageHandler(handler);
            //services.AddHttpClient<IVelaService, VelaService>(configureClient)
            //        .ConfigurePrimaryHttpMessageHandler(handler);
            //services.AddHttpClient<ICeraService, CeraService>(configureClient)
            //        .ConfigurePrimaryHttpMessageHandler(handler);
            //services.AddHttpClient<IMoldeService, MoldeService>(configureClient)
            //        .ConfigurePrimaryHttpMessageHandler(handler);
            //services.AddHttpClient<IFraganciaService, FraganciaService>(configureClient)
            //        .ConfigurePrimaryHttpMessageHandler(handler);
            //services.AddHttpClient<IPigmentoService, PigmentoService>(configureClient)
            //        .ConfigurePrimaryHttpMessageHandler(handler);
            //services.AddHttpClient<IClienteService, ClienteService>(configureClient)
            //        .ConfigurePrimaryHttpMessageHandler(handler);
            //services.AddHttpClient<IEndurecedorService, EndurecedorService>(configureClient)
            //        .ConfigurePrimaryHttpMessageHandler(handler);
            //services.AddHttpClient<IPackService, PackService>(configureClient)
            //        .ConfigurePrimaryHttpMessageHandler(handler);
            //services.AddHttpClient<IVelaFraganciaService, VelaFraganciaService>(configureClient)
            //       .ConfigurePrimaryHttpMessageHandler(handler);
            //services.AddHttpClient<IVelaPigmentoService, VelaPigmentoService>(configureClient)
            //       .ConfigurePrimaryHttpMessageHandler(handler);

            services.AddMvc();

            services.AddControllersWithViews(); // Si no lo tienes

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
