namespace Prizma.API
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;

    using AutoMapper;

    using Domain.Repositories;
    using Domain.Services.Implementations;
    using Domain.Services.Interfaces;

    using JsonApiDotNetCore.Extensions;
    using JsonApiDotNetCore.Graph;
    using JsonApiDotNetCore.Models;
    using JsonApiDotNetCore.Services;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;

    using Prizma.API.Services.Adapters;
    using Prizma.API.Services.Implementations;
    using Prizma.API.Services.Interfaces;
    using Prizma.API.Services.Profiles;
    using Prizma.API.ViewModels;
    using Prizma.Persistence.Repositories;

    /// <summary>
    /// The application startup class. This class is where all the IoC behaviors are defined.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">
        /// The configuration.
        /// </param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">
        /// The services collection.
        /// </param>
        public void ConfigureServices(IServiceCollection services)
        {
            this.ConfigureDatabaseServices(services);
            this.ConfigureJsonApiServices(services);
            this.ConfigureAutoMapperServices(services);
            this.ConfigureApiServices(services);
            this.ConfigureDomainServices(services);

            services.AddIdentity<UserResource, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(x =>
                    {
                        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                .AddJwtBearer(x =>
                    {
                        x.Events = new JwtBearerEvents
                                       {
                                           OnTokenValidated = context =>
                                               {
                                                   var userService = context.HttpContext.RequestServices.GetRequiredService<IUserResourceService>();
                                                   var userId = Guid.Parse(context.Principal.Identity.Name);
                                                   var user = userService.Get(userId);
                                                   if (user == null)
                                                   {
                                                       context.Fail("Unauthorized");
                                                   }

                                                   return Task.CompletedTask;
                                               }
                                       };
                        x.RequireHttpsMetadata = false;
                        x.SaveToken = true;
                        x.TokenValidationParameters = new TokenValidationParameters
                                                          {
                                                              ValidateIssuerSigningKey = true,
                                                              // IssuerSigningKey = new SymmetricSecurityKey(key),
                                                              ValidateIssuer = false,
                                                              ValidateAudience = false
                                                          };
                    });

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">
        /// The app builder.
        /// </param>
        /// <param name="env">
        /// The hosting environment.
        /// </param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();
            app.UseJsonApi();
            app.UseAuthentication();
        }

        /// <summary>
        /// Configures AutoMapper with the service collection for IoC.
        /// </summary>
        /// <param name="services">
        /// The services collection.
        /// </param>
        protected virtual void ConfigureAutoMapperServices(IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile(new ProjectProfile()); });
            var mapper = config.CreateMapper();

            services.AddSingleton(mapper);
            services.AddScoped<IResourceMapper, AutoMapperAdapter>();
        }

        /// <summary>
        /// Configures json api services with the service collection for IoC.
        /// </summary>
        /// <param name="services">
        /// The <paramref name="services"/>.
        /// </param>
        protected virtual void ConfigureJsonApiServices(IServiceCollection services)
        {
            var mvcBuilder = services.AddMvcCore();

            services.AddCors(options =>
                    {
                        // Because why not? TODO: Create an appropriate policy
                        options.AddDefaultPolicy(builder => 
                            builder.AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowAnyOrigin());
                    });

            services.AddJsonApi(
                options =>
                    {
                        options.Namespace = "api";
                        options.DefaultPageSize = 25;
                        options.RelativeLinks = true;
                        options.ValidateModelState = true;
                        options.BuildResourceGraph(
                            builder => builder.AddResource<ProjectResource, Guid>("project")
                                .UseNameFormatter(new DefaultResourceNameFormatter()));
                    },
                mvcBuilder);
        }

        /// <summary>
        /// Configures the database services with the service collection for IoC.
        /// </summary>
        /// <param name="services">
        /// The services collection.
        /// </param>
        protected virtual void ConfigureDatabaseServices(IServiceCollection services)
        {
            var connectionString = this.Configuration.GetConnectionString("DatabaseContext");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Database connection string not configured.");
            }

            services.AddDbContext<DatabaseContext>(options =>
                {
                    options.UseSqlServer(connectionString, builder => builder.MigrationsAssembly(typeof(DatabaseContext).GetTypeInfo().Assembly.GetName().Name));
                });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        /// <summary>
        /// Configures the Domain services with the service collection for IoC.
        /// </summary>
        /// <param name="services">
        /// The services collection.
        /// </param>
        protected virtual void ConfigureDomainServices(IServiceCollection services)
        {
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IUserService, UserService>();
        }

        /// <summary>
        /// Configures the API Application services with the service collection for IoC.
        /// </summary>
        /// <param name="services">
        /// The <paramref name="services"/>.
        /// </param>
        protected virtual void ConfigureApiServices(IServiceCollection services)
        {
            services.AddTransient<IResourceService<ProjectResource, Guid>, ProjectResourceService>();
            services.AddTransient<IResourceService<UserResource, Guid>, UserResourceService>();
        }
    }
}