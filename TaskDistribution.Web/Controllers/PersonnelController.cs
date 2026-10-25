using Microsoft.AspNetCore.Mvc;
using TaskDistribution.Application.Features.Personnel;
using TaskDistribution.Application.Features.Personnel.Contracts;

namespace TaskDistribution.Web.Controllers
{
	public class PersonnelController : Controller
	{
		private readonly PersonnelService _personnelService;

		public PersonnelController(PersonnelService personnelService)
		{
			_personnelService = personnelService;
		}

		public async Task<IActionResult> Index ()
		{
			var personnels = await _personnelService.GetAllAsync();

			return View(personnels);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(PersonnelCreateRequest request)
		{
			if (!ModelState.IsValid)
			{
				return View(request);
			}

			await _personnelService.CreateAsync(request);

			return RedirectToAction("Index");
		}


		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var personnel = await _personnelService.GetByIdAsync(id);

			if (personnel == null)
			{
				return NotFound();
			}

			var request = new PersonnelUpdateRequest
			{
				Id = personnel.Id,
				FirstName = personnel.FirstName,
				LastName = personnel.LastName,
				IsActive = personnel.IsActive
			};

			return View(request);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(PersonnelUpdateRequest request)
		{
			if (!ModelState.IsValid)
			{
				return View(request);
			}

			var personnel = await _personnelService.UpdateAsync(request);

			if (personnel == null)
			{
				return NotFound();
			}

			return RedirectToAction("Index");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> UpdateStatus(int id, bool isActive)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var isUpdated = await _personnelService.UpdateStatusAsync(id, isActive);
			if (!isUpdated)
			{
				return NotFound();
			}

			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var personnel = await _personnelService.GetByIdAsync(id);

			if (personnel == null)
			{
				return NotFound();
			}

			return View(personnel);
		}

		[HttpPost]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			try
			{
				var isDeleted = await _personnelService.DeleteAsync(id);

				if (!isDeleted)
				{
					return NotFound();
				}

				return RedirectToAction("Index");
			}
			catch (InvalidOperationException)
			{
				var personnel = await _personnelService.GetByIdAsync(id);

				if (personnel == null)
				{
					return NotFound();
				}

				ModelState.AddModelError(
					"",
					"Üzerinde görev bulunan personel silinemez. Personeli pasife alabilirsiniz.");

				return View("Delete", personnel);
			}
		}
	}
}
