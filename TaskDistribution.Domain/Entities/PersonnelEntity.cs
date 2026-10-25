using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskDistribution.Domain.Common;

namespace TaskDistribution.Domain.Entities
{
	public class PersonnelEntity : BaseEntity
	{
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public bool IsActive { get; set; } = true;
		public ICollection<WorkTaskEntity> WorkTasks { get; set; } = new List<WorkTaskEntity>();
	}
}
