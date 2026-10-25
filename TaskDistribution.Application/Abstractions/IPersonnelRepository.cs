using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskDistribution.Domain.Entities;

namespace TaskDistribution.Application.Abstractions
{
	public interface IPersonnelRepository
	{
		Task<List<PersonnelEntity>> GetAllAsync(CancellationToken cancellationToken = default);
		Task<PersonnelEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

		Task<PersonnelEntity?> GetByIdTrackingAsync(int id, CancellationToken cancellationToken = default);
		Task AddAsync(PersonnelEntity personnel, CancellationToken cancellationToken = default);
		Task UpdateAsync(PersonnelEntity personnel, CancellationToken cancellationToken = default);
		Task<bool> HasAssignedTasksAsync(int id, CancellationToken cancellationToken = default);
		Task DeleteAsync(int id, CancellationToken cancellationToken = default);

		Task<List<PersonnelEntity>> GetActiveAsync(CancellationToken cancellationToken = default);
		Task SaveChangesAsync(CancellationToken cancellationToken = default);

	}
}
