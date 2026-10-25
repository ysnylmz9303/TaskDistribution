using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskDistribution.Domain.Entities;

namespace TaskDistribution.Infrastructure.Persistence.Configurations
{
	public class OperationTypeConfiguration : IEntityTypeConfiguration<OperationTypeEntity>
	{
		public void Configure(EntityTypeBuilder<OperationTypeEntity> builder)
		{
			builder.ToTable("OperationTypes");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(150);

			builder.Property(x => x.DifficultyLevel)
				.IsRequired();
		}
	}
}