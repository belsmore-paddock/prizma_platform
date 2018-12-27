namespace Prizma.API.Tests
{
    using System;
    using System.Net.Http;

    using Xunit;

    /// <summary>
    /// The endpoint base test class provides common access to resources for testing APU endpoints.
    /// </summary>
    public class EndpointBaseTest : IClassFixture<IntegrationTestsApplicationFactory<Startup>>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EndpointBaseTest"/> class.
        /// </summary>
        /// <param name="factory">
        /// The application factory.
        /// </param>
        public EndpointBaseTest(IntegrationTestsApplicationFactory<Startup> factory)
        {
            this.Client = factory.CreateClient();
            this.Services = factory.Server.Host.Services;
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets the service provider. This provides access to the IoC container.
        /// </summary>
        protected IServiceProvider Services { get; }

        /// <summary>
        /// Gets the http client to perform operations against target endpoints.
        /// </summary>
        protected HttpClient Client { get; }

        #endregion
    }
}