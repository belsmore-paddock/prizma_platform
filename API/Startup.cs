namespace Prizma.API
{
    using System;
    using System.Reflection;

    using AutoMapper;

    using Domain.Repositories;
    using Domain.Services.Implementations;
    using Domain.Services.Interfaces;

    using JsonApiDotNetCore.Extensions;
    using JsonApiDotNetCore.Models;
    using JsonApiDotNetCore.Services;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Prizma.API.Services.Adapters;
    using Prizma.API.Services.Implementations;
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

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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

            app.UseMvc();
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
        }

        /// <summary>
        /// Configures json api services with the service collection for IoC.
        /// </summary>
        /// <param name="services">
        /// The services.
        /// </param>
        protected virtual void ConfigureJsonApiServices(IServiceCollection services)
        {
            var mvcBuilder = services.AddMvcCore();
            services.AddJsonApi(
                options =>
                    {
                        options.Namespace = "api";
                        options.DefaultPageSize = 25;
                        options.RelativeLinks = true;
                        options.BuildContextGraph(
                            builder => { builder.AddResource<ProjectResource, Guid>("project"); });
                    },
                mvcBuilder);

            services.AddScoped<IResourceMapper, AutoMapperAdapter>();
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

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IProjectRepository, ProjectRepository>();
        }

        /// <summary>
        /// Configures the Domain services with the service collection for IoC.
        /// </summary>
        /// <param name="services">
        /// The services collection.
        /// </param>
        protected virtual void ConfigureDomainServices(IServiceCollection services)
        {
            services.AddTransient<IProjectService, ProjectService>();
        }

        /// <summary>
        /// Configures the API Application services with the service collection for IoC.
        /// </summary>
        /// <param name="services">
        /// The services.
        /// </param>
        protected virtual void ConfigureApiServices(IServiceCollection services)
        {
            services.AddTransient<IResourceService<ProjectResource, Guid>, ProjectResourceService>();
        }
    }
}