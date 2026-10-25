namespace TaskDistribution.Application.Features.OperationType.Contracts
{
	public class OperationTypeResponse
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public int DifficultyLevel { get; set; }
	}
}