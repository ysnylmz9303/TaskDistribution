using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskDistribution.Domain.Common;

namespace TaskDistribution.Domain.Entities
{
	public class WorkTask : BaseEntity
	{
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;

		public int? AssignedPersonnelId { get; set; }
		public Personnel? AssignedPersonnel { get; set; }

		public int OperationTypeId { get; set; }
		public OperationType Operationtype { get; set; } = null;
		public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
	}
}
