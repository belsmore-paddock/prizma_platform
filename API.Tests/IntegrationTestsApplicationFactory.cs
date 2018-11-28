namespace Prizma.API.Tests
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using Prizma.Persistence.Repositories;

    /// <summary>
    /// Defines a Web Application Factory to use in our integration tests.
    /// The default implementation does not provide access to the environmental used for connection strings.
    /// https://github.com/aspnet/Mvc/issues/7976
    /// </summary>
    /// <typeparam name="TEntryPoint">
    /// The application type being used for the tests
    /// </typeparam>
    public class IntegrationTestsApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
    {
        #region Protected Methods

        /// <summary>
        /// Overrides the base implementation for ConfigureWebHost to explicitly specify the environment we're running in.
        /// </summary>
        /// <param name="webHostBuilder">
        /// The web host builder.
        /// </param>
        protected override void ConfigureWebHost(IWebHostBuilder webHostBuilder)
        {
            base.ConfigureWebHost(webHostBuilder);
            webHostBuilder.UseEnvironment("Test");
        }

        /// <summary>
        /// Overrides the default CreateServer implementation to execute migrations on the database before
        /// returning the test server.
        /// </summary>
        /// <param name="webHostBuilder">
        /// The web host builder.
        /// </param>
        /// <returns>
        /// The <see cref="TestServer"/>.
        /// </returns>
        protected override TestServer CreateServer(IWebHostBuilder webHostBuilder)
        {
            var testServer = base.CreateServer(webHostBuilder);

            var databaseContext = testServer.Host.Services.GetService<DatabaseContext>();
            databaseContext.Database.Migrate();

            return testServer;
        }

        #endregion
    }
}