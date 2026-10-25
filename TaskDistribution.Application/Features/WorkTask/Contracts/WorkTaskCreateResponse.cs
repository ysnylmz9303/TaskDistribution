namespace TaskDistribution.Application.Features.WorkTask.Contracts
{
	public class WorkTaskCreateResponse
	{
		public int Id { get; set; }
		public int? AssignedPersonnelId { get; set; }
	}
}