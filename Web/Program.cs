namespace Prizma.Web
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;

    /// <summary>
    /// The program class for the front-end.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main application method.
        /// </summary>
        /// <param name="args">
        /// The application arguments.
        /// </param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// This method creates the host builder.
        /// </summary>
        /// <param name="args">
        /// The method arguments.
        /// </param>
        /// <returns>
        /// The <see cref="IWebHostBuilder"/>.
        /// </returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}