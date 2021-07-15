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
using Quartz.Spi;
using QPCore.Jobs;
using Quartz;
using Quartz.Impl;
using Microsoft.Extensions.Logging;

namespace QPCore
{
    public class Startup
    {
        //readonly string MyAllowSpecificOrigins = "AnyOrignPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            // services.AddCors(options =>
            // {
            //     options.AddPolicy(name: MyAllowSpecificOrigins,
            //                       builder =>
            //                       {
            //                           builder.AllowAnyOrigin()
            //                                  .AllowAnyHeader()
            //                                  .AllowAnyMethod();


            //                       });
            // });
            services.AddCors();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // requires using Microsoft.Extensions.Options
            services.Configure<AADatabaseSettings>(
                Configuration.GetSection(nameof(AADatabaseSettings)));

            services.AddSingleton<IAADatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<AADatabaseSettings>>().Value);

            services.AddDbContext<QPContext>(options =>
                options.UseNpgsql(Configuration.GetValue<string>("AADatabaseSettings:ConnectionString"), o => o.SetPostgresVersion(9, 6)));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient(typeof(IBaseService<,,,>), typeof(BaseService<,,,>));
            services.AddTransient(typeof(IBaseGroupService<,,,>), typeof(BaseGroupService<,,,>));
            services.AddTransient(typeof(IEmailService), typeof(EmailService));
            services.AddTransient(typeof(IAccountService), typeof(AccountService));
            services.AddTransient(typeof(IOrganizationService), typeof(OrganizationService));
            services.AddTransient(typeof(IApplicationService), typeof(ApplicationService));
            services.AddTransient(typeof(IRoleService), typeof(RoleService));
            services.AddTransient(typeof(IUserRoleService), typeof(UserRoleService));
            services.AddTransient(typeof(ITestPlanService), typeof(TestPlanService));
            services.AddTransient(typeof(ITestPlanTestCaseService), typeof(TestPlanTestCaseService));
            services.AddTransient(typeof(ITestFlowCategoryService), typeof(TestFlowCategoryService));
            services.AddTransient(typeof(ITestFlowCategoryAssocService), typeof(TestFlowCategoryAssocService));
            services.AddTransient(typeof(IWebPageGroupService), typeof(WebPageGroupService));
            services.AddTransient(typeof(IWebPageService), typeof(WebPageService));
            services.AddTransient(typeof(ICompositeWebElementService), typeof(CompositeWebElementService));
            services.AddTransient<IWorkItemTypeService,WorkItemTypeService>();
            services.AddTransient<IWorkItemService,WorkItemService>();
            services.AddTransient<IWorkItemTestcaseAssocService,WorkItemTestcaseAssocService>();
            
            // Auto Mapper Configurations
            services.AddAutoMapper(typeof(Startup));

            services.AddSingleton<PostgresDataBase>();
            services.AddSingleton<WebModelService>();

            services.AddTransient<WebElementService>();
            services.AddTransient<FeatureAppService>();
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

            services.AddSingleton<IJobFactory, QPCoreJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            // AddDbContext
            var sp = services.BuildServiceProvider();
            var dbContext = sp.GetRequiredService<QPContext>();
            var logger = sp.GetService<ILogger<ReactiveIdleLockedTestFlowJob>>();
            services.AddSingleton(typeof(ILogger), logger);
            services.AddSingleton(typeof(ReactiveIdleLockedTestFlowJob), new ReactiveIdleLockedTestFlowJob(logger, dbContext));

            services.AddSingleton(new JobMetadata(Guid.NewGuid(), typeof(ReactiveIdleLockedTestFlowJob), "Reactive Idle Locked TestFlow Job", "0 */5 * ? * *"));
            services.AddHostedService<QPCoreHostedService>();
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
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            //app.UseCors(MyAllowSpecificOrigins);
            

            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseMiddleware<JwtMiddleware>();

            // global cors policy
            app.UseCors(x => x
                .SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
