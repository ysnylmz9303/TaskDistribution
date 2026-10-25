namespace TaskDistribution.Application.Features.WorkTask.Contracts
{
	public class WorkTaskResponse
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string OperationTypeName { get; set; } = string.Empty;
		public int DifficultyLevel { get; set; }
		public string AssignedPersonnelName { get; set; } = string.Empty;
		public DateTime CreatedDate { get; set; }
	}
}