using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskDistribution.Domain.Common;

namespace TaskDistribution.Domain.Entities
{
	public class OperationType : BaseEntity
	{
		public string Name { get; set; } = string.Empty;
		public int DifficultyLevel { get; set; }
		public ICollection<WorkTask> WorkTasks { get; set; } = new List<WorkTask>();

	}
}
