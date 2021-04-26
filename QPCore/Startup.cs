using AutomationAssistant.Models.AppConfig;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using QPCore.DAO;
using QPCore.Service;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using System.IO;
using AutoMapper;
using QPCore.AutoMapper;
using QPCore.Data;
using Microsoft.EntityFrameworkCore;
using QPCore.Service.Interfaces;
using QPCore.Middleware;
using QPCore.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace QPCore
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "AnyOrignPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin()
                                             .AllowAnyHeader()
                                             .AllowAnyMethod();


                                  });
            });

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // requires using Microsoft.Extensions.Options
            services.Configure<AADatabaseSettings>(
                Configuration.GetSection(nameof(AADatabaseSettings)));

            services.AddSingleton<IAADatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<AADatabaseSettings>>().Value);

            services.AddDbContext<QPContext>(options =>
                options.UseNpgsql(Configuration.GetValue<string>("AADatabaseSettings:ConnectionString"), o => o.SetPostgresVersion(9, 6)));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IEmailService), typeof(EmailService));
            services.AddTransient(typeof(IAccountService), typeof(AccountService));
            services.AddTransient(typeof(IOrganizationService), typeof(OrganizationService));
            services.AddTransient(typeof(IApplicationService), typeof(ApplicationService));

            // Auto Mapper Configurations
            services.AddAutoMapper(typeof(Startup));

            services.AddSingleton<PostgresDataBase>();
            services.AddSingleton<WebModelService>();

            services.AddSingleton<WebElementService>();
            services.AddSingleton<FeatureAppService>();
            services.AddTransient<StepService>();
            services.AddSingleton<CommandService>();
            services.AddTransient<TestFlowService>();
            services.AddSingleton<ConfigService>();
            services.AddSingleton<TestRunService>();
            services.AddControllers();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetValue<string>("AppSettings:Secret"))),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                        ClockSkew = TimeSpan.Zero
                    };
                });


            services.AddCors(c => { c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin()); });


            // Register the Swagger generator, defining 1 or more Swagger documents
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/spboyer"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, QPContext context)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            context.Database.Migrate();
            app.UseSwagger();


            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                //c.RoutePrefix = string.Empty;

            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(options => options.AllowAnyOrigin());

            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseMiddleware<JwtMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
