using System.ComponentModel.DataAnnotations;
using TaskDistribution.Application.Features.OperationType.Contracts;

namespace TaskDistribution.Web.ViewModels
{
	public class WorkTaskCreateViewModel
	{
		[Required(ErrorMessage = "Başlık giriniz.")]
		[StringLength(200, ErrorMessage = "Başlık en fazla 200 karakter olabilir.")]
		public string Title { get; set; } = string.Empty;

		[Required(ErrorMessage = "Açıklama giriniz.")]
		[StringLength(1000, ErrorMessage = "Açıklama en fazla 1000 karakter olabilir.")]
		public string Description { get; set; } = string.Empty;

		[Range(1, int.MaxValue, ErrorMessage = "İşlem türü seçiniz.")]
		public int OperationTypeId { get; set; }

		public List<OperationTypeResponse> OperationTypes { get; set; }
			= new List<OperationTypeResponse>();
	}
}