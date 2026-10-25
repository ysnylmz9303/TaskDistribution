using Microsoft.AspNetCore.Mvc;
using TaskDistribution.Application.Features.OperationType;
using TaskDistribution.Application.Features.WorkTask;
using TaskDistribution.Application.Features.WorkTask.Contracts;
using TaskDistribution.Web.ViewModels;

namespace TaskDistribution.Web.Controllers
{
	public class WorkTaskController : Controller
	{
		private readonly OperationTypeService _operationTypeService;
		private readonly WorkTaskService _workTaskService;

		public WorkTaskController(
			OperationTypeService operationTypeService,
			WorkTaskService workTaskService)
		{
			_operationTypeService = operationTypeService;
			_workTaskService = workTaskService;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var workTasks = await _workTaskService.GetAllAsync();

			return View(workTasks);
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			var operationTypes = await _operationTypeService.GetAllAsync();

			var model = new WorkTaskCreateViewModel
			{
				OperationTypes = operationTypes.ToList()
			};

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(WorkTaskCreateViewModel model)
		{
			var operationTypes = await _operationTypeService.GetAllAsync();

			model.OperationTypes = operationTypes.ToList();

			var operationTypeExists = operationTypes
				.Any(x => x.Id == model.OperationTypeId);

			if (!operationTypeExists)
			{
				ModelState.AddModelError(
					"OperationTypeId",
					"Geçerli bir işlem türü seçiniz.");
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var request = new WorkTaskCreateRequest
			{
				Title = model.Title,
				Description = model.Description,
				OperationTypeId = model.OperationTypeId
			};

			var response = await _workTaskService.CreateAsync(request);

			if (response.AssignedPersonnelId == null)
			{
				TempData["WarningMessage"] =
					"Görev oluşturuldu ancak dağıtım kurallarına uygun aktif personel bulunamadığı için atanamadı. Görev numarası: "
					+ response.Id;
			}
			else
			{
				TempData["SuccessMessage"] =
					"Görev oluşturuldu ve personele atandı. Görev numarası: "
					+ response.Id;
			}

			return RedirectToAction("Index");
		}
	}
}