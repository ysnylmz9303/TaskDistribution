using TaskDistribution.Application.Abstractions;
using TaskDistribution.Application.Features.TaskDistribution;
using TaskDistribution.Application.Features.WorkTask.Contracts;
using TaskDistribution.Domain.Entities;

namespace TaskDistribution.Application.Features.WorkTask
{
	public class WorkTaskService
	{
		private readonly IWorkTaskRepository _workTaskRepository;
		private readonly IOperationTypeRepository _operationTypeRepository;
		private readonly TaskDistributionService _taskDistributionService;

		public WorkTaskService(
			IWorkTaskRepository workTaskRepository,
			IOperationTypeRepository operationTypeRepository,
			TaskDistributionService taskDistributionService)
		{
			_workTaskRepository = workTaskRepository;
			_operationTypeRepository = operationTypeRepository;
			_taskDistributionService = taskDistributionService;
		}

		public async Task<WorkTaskCreateResponse> CreateAsync(WorkTaskCreateRequest request,CancellationToken cancellationToken = default)
		{
			var operationTypes = await _operationTypeRepository
				.GetAllAsync(cancellationToken);

			var operationType = operationTypes
				.FirstOrDefault(x => x.Id == request.OperationTypeId);

			if (operationType == null)
			{
				throw new InvalidOperationException("Seçilen işlem türü bulunamadı.");
			}

			var selectedPersonnelId = await _taskDistributionService
				.SelectPersonnelAsync(
					operationType.DifficultyLevel,
					cancellationToken);

			var workTask = new WorkTaskEntity
			{
				Title = request.Title.Trim(),
				Description = request.Description.Trim(),
				OperationTypeId = operationType.Id
			};

			if (selectedPersonnelId != null)
			{
				workTask.AssignedPersonnelId = selectedPersonnelId;
				workTask.AssignedDate = DateTime.UtcNow;
			}

			await _workTaskRepository.AddAsync(
				workTask,
				cancellationToken);

			await _workTaskRepository.SaveChangesAsync(cancellationToken);

			return new WorkTaskCreateResponse
			{
				Id = workTask.Id,
				AssignedPersonnelId = workTask.AssignedPersonnelId
			};
		}

		public async Task<IReadOnlyList<WorkTaskResponse>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			var workTasks = await _workTaskRepository.GetAllAsync(cancellationToken);

			var responses = new List<WorkTaskResponse>();

			foreach (var workTask in workTasks)
			{
				var assignedPersonnelName = "Henüz atanmadı";

				if (workTask.AssignedPersonnel != null)
				{
					assignedPersonnelName = workTask.AssignedPersonnel.FirstName
						+ " " + workTask.AssignedPersonnel.LastName;
				}

				var response = new WorkTaskResponse
				{
					Id = workTask.Id,
					Title = workTask.Title,
					Description = workTask.Description,
					OperationTypeName = workTask.OperationType.Name,
					DifficultyLevel = workTask.OperationType.DifficultyLevel,
					AssignedPersonnelName = assignedPersonnelName,
					CreatedDate = workTask.CreatedDate
				};

				responses.Add(response);
			}

			return responses;
		}
	}
}