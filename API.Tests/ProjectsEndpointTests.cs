namespace Prizma.API.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using Prizma.Domain.Models;
    using Prizma.Domain.Models.Builders;
    using Prizma.Domain.Services.Interfaces;

    using Xunit;

    /// <summary>
    /// The projects endpoint test performs happy path integration behavior validations for the projects resource.
    /// </summary>
    public class ProjectsEndpointTests : EndpointBaseTest
    {
        /// <summary>
        /// The project service to be used for seeding data.
        /// </summary>
        private readonly IProjectService projectService;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsEndpointTest"/> class.
        /// </summary>
        /// <param name="factory">
        /// The factory.
        /// </param>
        public ProjectsEndpointTest(IntegrationTestsApplicationFactory<Startup> factory)
            : base(factory)
        {
            this.projectService = this.Services.GetService<IProjectService>();
        }

        #endregion

        /// <summary>
        /// Validates the behavior of the controller get endpoint when not providing an id.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Fact(DisplayName = "Get endpoint without providing id returns all entries.")]
        public async Task GetWithoutIdReturnsAllProjectEntries()
        {
            // Arrange
            var project1 = new ProjectBuilder().WithId().WithDescription("Greetings from Project 1.").Build();
            var project2 = new ProjectBuilder().WithId().WithDescription("Greetings from Project 2.").Build();

            var projectSet = new HashSet<Project>
                                  {
                                      project1,
                                      project2
                                  };

            this.projectService.CreateMany(projectSet);

            // Act
            var response = await this.Client.GetAsync("/api/project").ConfigureAwait(false);

            // Assert
            Assert.NotNull(response);
            var body = response.Content.ReadAsStringAsync().Result;
            var json = JObject.Parse(body);
            var data = json["data"].AsJEnumerable();
            var createdEntity1 = data.SingleOrDefault(j => j["id"].ToString() == project1.Id.ToString());
            var createdEntity2 = data.SingleOrDefault(j => j["id"].ToString() == project2.Id.ToString());

            Assert.NotNull(createdEntity1);
            Assert.NotNull(createdEntity2);

            Assert.Equal("Greetings from Project 1.", createdEntity1["attributes"]["description"]);
            Assert.Equal("Greetings from Project 2.", createdEntity2["attributes"]["description"]);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        /// <summary>
        /// Validates the behavior of the controller get endpoint when not providing an id.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Fact(DisplayName = "Get endpoint with invalid id returns Not Found Response.")]
        public async Task GetWithInvalidIdReturnsNotFound()
        {
            // Arrange
            var randomGuid = Guid.NewGuid();

            // Act
            var response = await this.Client.GetAsync($"/api/project/{randomGuid}").ConfigureAwait(false);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        /// <summary>
        /// Validates the behavior of the controller get endpoint when not providing an id.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Fact(DisplayName = "Get endpoint with valid id returns single entry.")]
        public async Task GetWithValidIdReturnsProjectEntry()
        {
            // Arrange
            var project = new ProjectBuilder().WithId().WithDescription("Single Project Entry.").Build();
            this.projectService.Create(project);

            // Act
            var response = await this.Client.GetAsync($"/api/project/{project.Id}").ConfigureAwait(false);

            // Assert
            Assert.NotNull(response);
            var body = response.Content.ReadAsStringAsync().Result;
            var json = JObject.Parse(body);
            var createdEntity = json["data"];

            Assert.NotNull(createdEntity);

            Assert.Equal(project.Id.ToString(), createdEntity["id"]);
            Assert.Equal("Single Project Entry.", createdEntity["attributes"]["description"]);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        /// <summary>
        /// Validates a post with a valid resource returns a created entity.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Fact(DisplayName = "Create with valid resource returns single entry.")]
        public async Task CreateWithValidEntryCreatesNewRecordAndReturnsCreatedResponse()
        {
            // Arrange
            var description = $"Create {DateTime.Now}";

            var content = new
                              {
                                  data = new
                                             {
                                                 type = "project",
                                                 attributes =
                                                     new Dictionary<string, object>
                                                         {
                                                            { "description", description }
                                                         }
                                             }
                              };

            var jsonObj = JsonConvert.SerializeObject(content);

            var httpContent = new StringContent(jsonObj);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.api+json");

            // Act
            var response = await this.Client.PostAsync("/api/project", httpContent).ConfigureAwait(false);

            // Assert
            Assert.NotNull(response);
            var body = response.Content.ReadAsStringAsync().Result;
            var json = JObject.Parse(body);
            var createdEntity = json["data"];

            Assert.NotNull(createdEntity);

            Assert.Equal(description, createdEntity["attributes"]["description"]);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        /// <summary>
        /// Validates a patch with a valid resource returns an updated entity.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Fact(DisplayName = "Patch with existing resource returns updated entry.")]
        public async Task PatchWithExistingEntryUpdatesRecordAndReturnsUpdatedEntry()
        {
            // Arrange
            var project = new ProjectBuilder().WithId().WithDescription("Patch me.").Build();
            this.projectService.Create(project);
            var description = $"Patch {DateTime.Now}";

            var content = new
                              {
                                  data = new
                                             {
                                                 id = project.Id,
                                                 type = "project",
                                                 attributes =
                                                     new Dictionary<string, object>
                                                         {
                                                             { "description", description }
                                                         }
                                             }
                              };

            var jsonObj = JsonConvert.SerializeObject(content);

            var httpContent = new StringContent(jsonObj);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.api+json");

            // Act
            var response = await this.Client.PatchAsync($"/api/project/{project.Id}", httpContent).ConfigureAwait(false);

            // Assert
            Assert.NotNull(response);
            var body = response.Content.ReadAsStringAsync().Result;
            var json = JObject.Parse(body);
            var createdEntity = json["data"];

            Assert.NotNull(createdEntity);

            Assert.Equal(description, createdEntity["attributes"]["description"]);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        /// <summary>
        /// Validates the behavior of the controller delete endpoint when providing an id.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Fact(DisplayName = "Delete endpoint with valid id returns no content.")]
        public async Task DeleteWithValidIdReturnNoContent()
        {
            // Arrange
            var project = new ProjectBuilder().WithId().WithDescription("Single Project Entry For Deletion.").Build();
            this.projectService.Create(project);

            // Act
            var response = await this.Client.DeleteAsync($"/api/project/{project.Id}").ConfigureAwait(false);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}