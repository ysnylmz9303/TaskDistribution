
using TaskDistribution.Application.Abstractions;
using TaskDistribution.Domain.Entities;
using TaskDistribution.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace TaskDistribution.Infrastructure.Repositories
{

	public class PersonnelRepository : IPersonnelRepository
	{
		private readonly ApplicationDbContext _context;

		public PersonnelRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<PersonnelEntity>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			return await _context.Personnels
				.AsNoTracking()
				.ToListAsync(cancellationToken);
		}

		public async Task<PersonnelEntity?> GetByIdAsync(int id,CancellationToken cancellationToken = default)
		{
			return await _context.Personnels
				.AsNoTracking()
				.FirstOrDefaultAsync(
					x => x.Id == id,
					cancellationToken);
		}

		public async Task<PersonnelEntity?> GetByIdTrackingAsync(int id, CancellationToken cancellationToken = default)
		{
			return await _context.Personnels
				.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
		}

		public async Task AddAsync(PersonnelEntity personnel,CancellationToken cancellationToken = default)
		{
			await _context.Personnels.AddAsync(
				personnel,
				cancellationToken);
		}

		public Task UpdateAsync(PersonnelEntity personnel,CancellationToken cancellationToken = default)
		{
			_context.Personnels.Update(personnel);
			return Task.CompletedTask;
		}

		public async Task<bool> HasAssignedTasksAsync(int id, CancellationToken cancellationToken = default)
		{
			return await _context.WorkTasks
				.AnyAsync(
					x => x.AssignedPersonnelId == id,
					cancellationToken);
		}

		public async Task<List<PersonnelEntity>> GetActiveAsync(CancellationToken cancellationToken = default)
		{
			return await _context.Personnels
				.AsNoTracking()
				.Where(x => x.IsActive)
				.OrderBy(x => x.Id)
				.ToListAsync(cancellationToken);
		}

		public async Task DeleteAsync(int id,CancellationToken cancellationToken = default)
		{
			var personnel = await _context.Personnels
				.FirstOrDefaultAsync(
					x => x.Id == id,
					cancellationToken);

			if (personnel == null)
			{
				return;
			}

			_context.Personnels.Remove(personnel);
		}

		public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			await _context.SaveChangesAsync(cancellationToken);
		}
	}
}
