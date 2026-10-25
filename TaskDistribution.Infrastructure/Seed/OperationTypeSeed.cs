using Microsoft.EntityFrameworkCore;
using TaskDistribution.Domain.Entities;
using TaskDistribution.Infrastructure.Persistence;

namespace TaskDistribution.Infrastructure.Seed
{
	public class OperationTypeSeed
	{
		private readonly ApplicationDbContext _context;

		public OperationTypeSeed(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task SeedAsync()
		{
			var hasOperationTypes = await _context.OperationTypes.AnyAsync();

			if (hasOperationTypes)
			{
				return;
			}

			var operationTypes = new List<OperationTypeEntity>
			{
				new OperationTypeEntity
				{
					Name = "Metin güncelleme",
					DifficultyLevel = 1
				},
				new OperationTypeEntity
				{
					Name = "Görünüm düzenleme",
					DifficultyLevel = 2
				},
				new OperationTypeEntity
				{
					Name = "Form geliştirme",
					DifficultyLevel = 3
				},
				new OperationTypeEntity
				{
					Name = "Rapor hazırlama",
					DifficultyLevel = 4
				},
				new OperationTypeEntity
				{
					Name = "İş kuralı geliştirme",
					DifficultyLevel = 5
				},
				new OperationTypeEntity
				{
					Name = "Servis entegrasyonu",
					DifficultyLevel = 6
				},
				new OperationTypeEntity
				{
					Name = "Performans iyileştirme",
					DifficultyLevel = 7
				},
				new OperationTypeEntity
				{
					Name = "Veri taşıma",
					DifficultyLevel = 8
				}
			};

			await _context.OperationTypes.AddRangeAsync(operationTypes);
			await _context.SaveChangesAsync();
		}
	}
}