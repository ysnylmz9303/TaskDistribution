using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskDistribution.Domain.Entities;

namespace TaskDistribution.Infrastructure.Persistence.Configurations
{
	public class WorkTaskConfiguration : IEntityTypeConfiguration<WorkTaskEntity>
	{
		public void Configure(EntityTypeBuilder<WorkTaskEntity> builder)
		{
			builder.ToTable("WorkTasks");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Title)
				.IsRequired()
				.HasMaxLength(200);

			builder.Property(x => x.Description)
				.HasMaxLength(1000);

			builder.Property(x => x.CreatedDate)
				.IsRequired();

			builder.HasOne(x => x.AssignedPersonnel)
				.WithMany(x => x.WorkTasks)
				.HasForeignKey(x => x.AssignedPersonnelId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(x => x.OperationType)
				.WithMany(x => x.WorkTasks)
				.HasForeignKey(x => x.OperationTypeId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}