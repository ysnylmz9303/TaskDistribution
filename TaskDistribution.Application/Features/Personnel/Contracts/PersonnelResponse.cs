namespace TaskDistribution.Application.Features.Personnel.Contracts
{
	public class PersonnelResponse
	{
		public int Id { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public bool IsActive { get; set; }
	}
}