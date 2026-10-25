using Microsoft.EntityFrameworkCore;
using TaskDistribution.Application.Abstractions;
using TaskDistribution.Domain.Entities;
using TaskDistribution.Infrastructure.Persistence;

namespace TaskDistribution.Infrastructure.Repositories
{
	public class WorkTaskRepository : IWorkTaskRepository
	{
		private readonly ApplicationDbContext _context;

		public WorkTaskRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(WorkTaskEntity workTask,CancellationToken cancellationToken = default)
		{
			await _context.WorkTasks.AddAsync(workTask,cancellationToken);
		}

		public async Task<List<WorkTaskEntity>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			return await _context.WorkTasks
				.AsNoTracking()
				.Include(x => x.OperationType)
				.Include(x => x.AssignedPersonnel)
				.OrderByDescending(x => x.CreatedDate)
				.ThenByDescending(x => x.Id)
				.ToListAsync(cancellationToken);
		}

		public async Task<List<WorkTaskEntity>> GetAssignedTasksAsync(CancellationToken cancellationToken = default)
		{
			return await _context.WorkTasks
				.AsNoTracking()
				.Include(x => x.OperationType)
				.Where(x => x.AssignedPersonnelId != null)
				.OrderByDescending(x => x.AssignedDate)
				.ThenByDescending(x => x.Id)
				.ToListAsync(cancellationToken);
		}
		public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			await _context.SaveChangesAsync(cancellationToken);
		}
	}
}