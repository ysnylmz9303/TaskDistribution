using Microsoft.EntityFrameworkCore;
using TaskDistribution.Application.Abstractions;
using TaskDistribution.Domain.Entities;
using TaskDistribution.Infrastructure.Persistence;

namespace TaskDistribution.Infrastructure.Repositories
{
	public class OperationTypeRepository : IOperationTypeRepository
	{
		private readonly ApplicationDbContext _context;

		public OperationTypeRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<OperationTypeEntity>> GetAllAsync(
			CancellationToken cancellationToken = default)
		{
			return await _context.OperationTypes
				.AsNoTracking()
				.OrderBy(x => x.DifficultyLevel)
				.ToListAsync(cancellationToken);
		}
	}
}