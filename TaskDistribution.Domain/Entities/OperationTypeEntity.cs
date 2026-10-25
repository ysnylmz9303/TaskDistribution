using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskDistribution.Domain.Common;

namespace TaskDistribution.Domain.Entities
{
	public class OperationTypeEntity : BaseEntity
	{
		public string Name { get; set; } = string.Empty;
		public int DifficultyLevel { get; set; }
		public ICollection<WorkTaskEntity> WorkTasks { get; set; } = new List<WorkTaskEntity>();


	}
}
