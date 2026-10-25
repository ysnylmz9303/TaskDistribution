using TaskDistribution.Domain.Entities;

namespace TaskDistribution.Application.Abstractions
{
	public interface IOperationTypeRepository
	{
		Task<List<OperationTypeEntity>> GetAllAsync(
			CancellationToken cancellationToken = default);
	}
}