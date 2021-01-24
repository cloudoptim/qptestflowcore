using AutomationAssistant.Models.AppConfig;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using QPCore.DAO;
using QPCore.Service;

namespace QPCore
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
            // requires using Microsoft.Extensions.Options
            services.Configure<AADatabaseSettings>(
                Configuration.GetSection(nameof(AADatabaseSettings)));

            services.AddSingleton<IAADatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<AADatabaseSettings>>().Value);


            services.AddSingleton<PostgresDataBase>();
            services.AddSingleton<WebModelService>();

            services.AddControllers();
            
            
            services.AddCors(c =>  {  
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());  
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
            app.UseCors(options => options.AllowAnyOrigin());  


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
