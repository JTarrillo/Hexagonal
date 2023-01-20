using Sicv1.Domain.Contracts.Services;
using Sicv1.Domain.Entities;
using Sicv1.Infrastructure.Cross;
using Sicv1.Presentation.Utils;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sicv1.Presentation.Controllers
{
	public class CategoryCardController : Controller
	{
		private readonly ICategoryCardService _categoryCardService;

		public CategoryCardController(ICategoryCardService categoryCardService)
		{
			this._categoryCardService = categoryCardService;
		}

		[HttpPost]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<JsonResult> Save(CategoryCard categoryCard)
		{
			try
			{
				categoryCard.CREATED_USER = Util.MyUser.ID;
				var resultado = await _categoryCardService.Save(categoryCard);
				return new HandleResults().ValidateResult(resultado);
			}
			catch (Exception)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso" });
			}
		}

		[HttpPost]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<JsonResult> Update(CategoryCard categoryCard)
		{
			try
			{
				categoryCard.UPDATED_USER = Util.MyUser.ID;
				var resultado = await _categoryCardService.Update(categoryCard);

				//Si es menor a 2 es porque no tiene S y N (Sin y con tarjeta),
				//por lo tanto inserta.
				if (resultado < 2)
				{
					categoryCard.CREATED_USER = Util.MyUser.ID;
					return await Save(categoryCard);
				}
				return new HandleResults().ValidateResult(resultado);
			}
			catch (Exception)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso" });
			}
		}
	}
}