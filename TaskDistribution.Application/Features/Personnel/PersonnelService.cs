using TaskDistribution.Application.Abstractions;
using TaskDistribution.Application.Features.Personnel.Contracts;
using TaskDistribution.Domain.Entities;

namespace TaskDistribution.Application.Features.Personnel
{
	public class PersonnelService
	{
		private readonly IPersonnelRepository _personnelRepository;

		public PersonnelService(IPersonnelRepository personnelRepository)
		{
			_personnelRepository = personnelRepository;
		}

		public async Task<PersonnelResponse> CreateAsync(PersonnelCreateRequest request,CancellationToken cancellationToken = default)
		{
			var personnel = new PersonnelEntity
			{
				FirstName = request.FirstName.Trim(),
				LastName = request.LastName.Trim(),
				IsActive = true
			};

			await _personnelRepository.AddAsync(personnel, cancellationToken);
			await _personnelRepository.SaveChangesAsync(cancellationToken);

			return new PersonnelResponse
			{
				Id = personnel.Id,
				FirstName = personnel.FirstName,
				LastName = personnel.LastName,
				IsActive = personnel.IsActive
			};
		}

		public async Task<PersonnelResponse?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
		{
			var personnel = await _personnelRepository.GetByIdAsync(id, cancellationToken);
			if (personnel is null)
			{
				return null;
			}

			return new PersonnelResponse
			{
				Id = personnel.Id,
				FirstName = personnel.FirstName,
				LastName = personnel.LastName,
				IsActive = personnel.IsActive
			};
		}

		public async Task<IReadOnlyList<PersonnelResponse>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			var personnels = await _personnelRepository.GetAllAsync(cancellationToken);

			return personnels
				.Select(x => new PersonnelResponse
				{
					Id = x.Id,
					FirstName = x.FirstName,
					LastName = x.LastName,
					IsActive = x.IsActive
				})
				.ToList();
		}


		public async Task<PersonnelResponse?> UpdateAsync(PersonnelUpdateRequest request, CancellationToken cancellationToken = default)
		{
			var personnel = await _personnelRepository.GetByIdTrackingAsync(request.Id, cancellationToken);
			if (personnel is null)
			{
				return null;
			}

			personnel.FirstName = request.FirstName.Trim();
			personnel.LastName = request.LastName.Trim();
			personnel.IsActive = request.IsActive;

			await _personnelRepository.SaveChangesAsync(cancellationToken);

			return new PersonnelResponse
			{
				Id = personnel.Id,
				FirstName = personnel.FirstName,
				LastName = personnel.LastName,
				IsActive = personnel.IsActive
			};
		}

		public async Task<bool> UpdateStatusAsync(int id, bool isActive, CancellationToken cancellationToken = default)
		{
			var personnel = await _personnelRepository.GetByIdTrackingAsync(id, cancellationToken);
			if (personnel == null)
			{
				return false;
			}

			personnel.IsActive = isActive;
			await _personnelRepository.SaveChangesAsync(cancellationToken);

			return true;
		}

		public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
		{
			var personnel = await _personnelRepository.GetByIdTrackingAsync(id, cancellationToken);
			if (personnel is null)
			{
				return false;
			}

			var hasAssignedTasks = await _personnelRepository.HasAssignedTasksAsync(id, cancellationToken);
			if (hasAssignedTasks)
			{
				throw new InvalidOperationException("Personnel with assigned tasks cannot be deleted. Deactivate the personnel instead.");
			}

			await _personnelRepository.DeleteAsync(id, cancellationToken);
			await _personnelRepository.SaveChangesAsync(cancellationToken);

			return true;
		}
	}
}
