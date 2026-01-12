using System.Globalization;
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
using Microsoft.AspNetCore.Localization;

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
            services.AddScoped<RepositoryDocumentos>();
            services.AddScoped<RepositoryEndurecedores>();
            services.AddScoped<RepositoryFragancias>();
            services.AddScoped<RepositoryMechas>();
            services.AddScoped<RepositoryMoldes>();
            services.AddScoped<RepositoryPacks>();
            services.AddScoped<RepositoryPedidos>();
            services.AddScoped<RepositoryPigmentos>();
            services.AddScoped<RepositoryVelaFragancias>();
            services.AddScoped<RepositoryVelaPigmentos>();
            services.AddScoped<RepositoryVelasFinalizadas>();
            services.AddScoped<RepositoryInventarios>();

            // ✅ Configuración global del HttpClientAction<IServiceProvider, HttpClient> configureClient = (sp, client) =>

            string _baseUrl = "http://localhost:5000/"; //Configuration["ApiSettings: BaseUrl"] ??

            services.AddHttpClient("ApiClient", client =>
            {
                client.BaseAddress = new Uri(_baseUrl); //Configuration["ApiSettings: BaseUrl"]
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            });

            //services.AddHttpClient<ICeraService, CeraService>();
            services.AddScoped<ICeraService>(sp =>
            {
                var factory = sp.GetRequiredService<IHttpClientFactory>();
                var client = factory.CreateClient("ApiClient");
                return new CeraService(client);
            });

            //services.AddHttpClient<IClienteService, ClienteService>();
            services.AddScoped<IClienteService>(sp =>
            {
                var factory = sp.GetRequiredService<IHttpClientFactory>();
                var client = factory.CreateClient("ApiClient");
                return new ClienteService(client);
            });

            //services.AddHttpClient<IClienteService, ClienteService>();
            services.AddScoped<IDocumentoService>(sp =>
            {
                var factory = sp.GetRequiredService<IHttpClientFactory>();
                var client = factory.CreateClient("ApiClient");
                return new DocumentoService(client);
            });

            //services.AddHttpClient<IEndurecedorService, EndurecedorService>();
            services.AddScoped<IEndurecedorService>(sp =>
            {
                var factory = sp.GetRequiredService<IHttpClientFactory>();
                var client = factory.CreateClient("ApiClient");
                return new EndurecedorService(client);
            });
                        
            //services.AddHttpClient<IFraganciaService, FraganciaService>();
            services.AddScoped<IFraganciaService>(sp =>
            {
                var factory = sp.GetRequiredService<IHttpClientFactory>();
                var client = factory.CreateClient("ApiClient");
                return new FraganciaService(client);
            });

            //services.AddTransient<IMechaService, MechaService>();
            services.AddScoped<IMechaService>(sp =>
            {
                var factory = sp.GetRequiredService<IHttpClientFactory>();
                var client = factory.CreateClient("ApiClient");
                return new MechaService(client);
            });

            //services.AddHttpClient<IMoldeService, MoldeService>();
            services.AddScoped<IMoldeService>(sp =>
            {
                var factory = sp.GetRequiredService<IHttpClientFactory>();
                var client = factory.CreateClient("ApiClient");
                return new MoldeService(client);
            });

            //services.AddHttpClient<IPackService, PackService>();
            services.AddScoped<IPackService>(sp =>
            {
                var factory = sp.GetRequiredService<IHttpClientFactory>();
                var client = factory.CreateClient("ApiClient");
                return new PackService(client);
            });

            //services.AddHttpClient<IPedidoService, PedidoService>();
            services.AddScoped<IPedidoService>(sp =>
            {
                var factory = sp.GetRequiredService<IHttpClientFactory>();
                var client = factory.CreateClient("ApiClient");
                return new PedidoService(client);
            });

            //services.AddHttpClient<IPigmentoService, PigmentoService>();
            services.AddScoped<IPigmentoService>(sp =>
            {
                var factory = sp.GetRequiredService<IHttpClientFactory>();
                var client = factory.CreateClient("ApiClient");
                return new PigmentoService(client);
            });

            //services.AddHttpClient<IVelaFraganciaService, VelaFraganciaService>();
            services.AddScoped<IVelaFraganciaService>(sp =>
            {
                var factory = sp.GetRequiredService<IHttpClientFactory>();
                var client = factory.CreateClient("ApiClient");
                return new VelaFraganciaService(client);
            });

            //services.AddHttpClient<IVelaPigmentoService, VelaPigmentoService>();
            services.AddScoped<IVelaPigmentoService>(sp =>
            {
                var factory = sp.GetRequiredService<IHttpClientFactory>();
                var client = factory.CreateClient("ApiClient");
                return new VelaPigmentoService(client);
            });

            //services.AddHttpClient<IVelaService, VelaService>();
            services.AddScoped<IVelaService>(sp =>
            {
                var factory = sp.GetRequiredService<IHttpClientFactory>();
                var client = factory.CreateClient("ApiClient");
                return new VelaService(client);
            });

            //services.AddHttpClient<IVelaFinalizadaService, VelaFinalizadaService>();
            services.AddScoped<IVelaFinService>(sp =>
            {
                var factory = sp.GetRequiredService<IHttpClientFactory>();
                var client = factory.CreateClient("ApiClient");
                return new VelaFinalizadaService(client);
            });

            services.AddScoped<IInventarioService>(sp =>
            {
                var factory = sp.GetRequiredService<IHttpClientFactory>();
                var client = factory.CreateClient("ApiClient");
                return new InventarioService(client);
            });

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

            var supportedCultures = new[] { new CultureInfo("en-US") };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });


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
