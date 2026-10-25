using TaskDistribution.Application.Abstractions;
using TaskDistribution.Application.Features.OperationType.Contracts;

namespace TaskDistribution.Application.Features.OperationType
{
	public class OperationTypeService
	{
		private readonly IOperationTypeRepository _operationTypeRepository;

		public OperationTypeService(IOperationTypeRepository operationTypeRepository)
		{
			_operationTypeRepository = operationTypeRepository;
		}

		public async Task<IReadOnlyList<OperationTypeResponse>> GetAllAsync(
			CancellationToken cancellationToken = default)
		{
			var operationTypes = await _operationTypeRepository
				.GetAllAsync(cancellationToken);

			return operationTypes
				.Select(x => new OperationTypeResponse
				{
					Id = x.Id,
					Name = x.Name,
					DifficultyLevel = x.DifficultyLevel
				})
				.ToList();
		}
	}
}