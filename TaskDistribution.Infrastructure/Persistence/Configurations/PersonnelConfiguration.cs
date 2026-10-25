using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskDistribution.Domain.Entities;

namespace TaskDistribution.Infrastructure.Persistence.Configurations
{
	public class PersonnelConfiguration : IEntityTypeConfiguration<PersonnelEntity>
	{
		public void Configure(EntityTypeBuilder<PersonnelEntity> builder)
		{
			builder.ToTable("Personnels");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.FirstName)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(x => x.LastName)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(x => x.IsActive)
				.IsRequired();
		}
	}
}
