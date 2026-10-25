namespace TaskDistribution.Application.Features.WorkTask.Contracts
{
	public class WorkTaskCreateRequest
	{
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public int OperationTypeId { get; set; }
	}
}