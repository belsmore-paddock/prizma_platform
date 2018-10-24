namespace Prizma.Persistence.Repositories.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Prizma.Domain.Models;

    /// <summary>
    /// The entity framework project configuration.
    /// </summary>
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        /// <summary>
        /// Configures the project entity for use with entity framework.
        /// </summary>
        /// <param name="builder">
        /// The entity type builder being configured for the project entity.
        /// </param>
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Description).HasMaxLength(256).IsRequired();

            builder.Property(p => p.CreatedAt).IsRequired();
            builder.Property(p => p.UpdatedAt).IsRequired();
        }
    }
}