using TaskDistribution.Domain.Entities;

namespace TaskDistribution.Application.Abstractions
{
	public interface IWorkTaskRepository
	{
		Task AddAsync(WorkTaskEntity workTask,CancellationToken cancellationToken = default);

		Task<List<WorkTaskEntity>> GetAllAsync(CancellationToken cancellationToken = default);

		Task<List<WorkTaskEntity>> GetAssignedTasksAsync(CancellationToken cancellationToken = default);
		Task SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}