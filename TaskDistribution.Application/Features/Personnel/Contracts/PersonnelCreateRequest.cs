namespace TaskDistribution.Application.Features.Personnel.Contracts
{
	public class PersonnelCreateRequest
	{
		public string FirstName { get; set; } = string.Empty;

		public string LastName { get; set; } = string.Empty;
	}
}