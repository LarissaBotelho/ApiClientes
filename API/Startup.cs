using ApiCobranca.Models;
using ApiCobranca.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using StoneAPI.Models;
using StoneAPI.Services;
using System;
using System.IO;
using System.Reflection;

namespace StoneAPI
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
            services.Configure<CobrancaDatabaseSettings>(
                Configuration.GetSection(nameof(CobrancaDatabaseSettings)));
            services.AddSingleton<ICobrancaDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<CobrancaDatabaseSettings>>().Value);
            services.AddSingleton<CobrancaService>();

            services.Configure<ClienteDatabaseSettings>(
                Configuration.GetSection(nameof(ClienteDatabaseSettings)));
            services.AddSingleton<IClienteDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<ClienteDatabaseSettings>>().Value);
            services.AddSingleton<ClienteService>();

            services.AddSingleton<CalculoService>();

            services.AddControllers();

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                opt.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Ativa o Swagger
            app.UseSwagger();

            // Ativa o Swagger UI
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });
        }
    }
}
