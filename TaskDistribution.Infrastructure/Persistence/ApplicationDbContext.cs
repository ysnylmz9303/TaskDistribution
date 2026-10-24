using Microsoft.EntityFrameworkCore;
using TaskDistribution.Domain.Entities;

namespace TaskDistribution.Infrastructure.Persistence
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(
			DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		
		public DbSet<Personnel> Personnels { get; set; }
		public DbSet<WorkTask> WorkTasks { get; set; }
		public DbSet<OperationType> OperationTypes { get; set; }
	}
}