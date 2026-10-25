using TaskDistribution.Application.Abstractions;

namespace TaskDistribution.Application.Features.TaskDistribution
{
	public class TaskDistributionService
	{
		private readonly IPersonnelRepository _personnelRepository;
		private readonly IWorkTaskRepository _workTaskRepository;

		public TaskDistributionService(
			IPersonnelRepository personnelRepository,
			IWorkTaskRepository workTaskRepository)
		{
			_personnelRepository = personnelRepository;
			_workTaskRepository = workTaskRepository;
		}

		public async Task<int?> SelectPersonnelAsync(int difficultyLevel,CancellationToken cancellationToken = default)
		{
			var personnels = await _personnelRepository.GetActiveAsync(cancellationToken);

			var assignedTasks = await _workTaskRepository.GetAssignedTasksAsync(cancellationToken);

			int? selectedPersonnelId = null;
			int lowestDifficultyTaskCount = int.MaxValue;
			int lowestTotalDifficulty = int.MaxValue;

			foreach (var personnel in personnels)
			{
				var personnelTasks = assignedTasks
					.Where(x => x.AssignedPersonnelId == personnel.Id)
					.ToList();

				var lastTask = personnelTasks.FirstOrDefault();

				if (lastTask != null)
				{
					var lastDifficultyLevel = lastTask.OperationType.DifficultyLevel;

					if (Math.Abs(lastDifficultyLevel - difficultyLevel) == 1)
					{
						continue;
					}
				}

				var difficultyTaskCount = personnelTasks
					.Count(x => x.OperationType.DifficultyLevel == difficultyLevel);

				var totalDifficulty = personnelTasks
					.Sum(x => x.OperationType.DifficultyLevel);

				if (difficultyTaskCount < lowestDifficultyTaskCount)
				{
					selectedPersonnelId = personnel.Id;
					lowestDifficultyTaskCount = difficultyTaskCount;
					lowestTotalDifficulty = totalDifficulty;
				}
				else if (difficultyTaskCount == lowestDifficultyTaskCount)
				{
					if (totalDifficulty < lowestTotalDifficulty)
					{
						selectedPersonnelId = personnel.Id;
						lowestTotalDifficulty = totalDifficulty;
					}
				}
			}

			return selectedPersonnelId;
		}
	}
}