using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data.Entity;
using TrackItAPI.DataModels;
using TrackItAPI.Entities;
using TrackItAPI.UnitOfWork;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TrackItAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NutrientController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;

		public NutrientController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		[Route("GetNutrients")]
		public IActionResult GetNutrients()
		{
			var data = _unitOfWork.Nutrients.GetAll();

			if (data != null)
			{
				var nutrients = data.ToList();
				var response = JsonConvert.SerializeObject(nutrients);

				return Ok(response);
			}

			else
			{
				return BadRequest();
			}
		}

		[HttpGet]
		[Route("GetNutrient/{name}")]
		public IActionResult GetNutrient(string name)
		{
			var data = _unitOfWork.Nutrients.GetWhere(x => x.NutrientName == name);

			if (data != null)
			{
				var nutrients = data.ToList();
				var response = JsonConvert.SerializeObject(nutrients);

				return Ok(response);
			}

			else
			{
				return BadRequest();
			}
		}

		[HttpGet]
		[Route("GetNutrientByID/{id}")]
		public IActionResult GetNutrientByID(int id)
		{
			var data = _unitOfWork.Nutrients.GetWhere(x => x.NutrientID == id);

			if (data != null)
			{
				var nutrient = data.FirstOrDefault();

				return Ok(nutrient);
			}

			else
			{
				return BadRequest();
			}
		}

		[HttpPost]
		[Route("CreateMemberNutrient")]
		public IActionResult CreateMemberNutrient([FromBody] MemberNutrient info)
		{
			if (info != null)
			{
				_unitOfWork.MemberNutrients.Add(info);
				_unitOfWork.SaveAsync();

				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpPost]
		[Route("UpdateMemberNutrient")]
		public IActionResult UpdateMemberNutrient([FromBody] MemberNutrient memberNutrient)
		{
			if (memberNutrient != null)
			{
				var a = _unitOfWork.MemberNutrients.GetWhere(x => x.MemberNutrientID == memberNutrient.MemberNutrientID).FirstOrDefault();

				a.ServingSize = memberNutrient.ServingSize;
				a.Notes = memberNutrient.Notes;

				_unitOfWork.MemberNutrients.Update(a);
				_unitOfWork.SaveAsync();

				return Ok();
			}
			else
			{

				return BadRequest();
			}
		}


		[HttpGet]
		[Route("DeleteMemberNutrient/{id}")]
		public IActionResult DeleteMemberNutrient(Guid id)
		{
			var data = _unitOfWork.MemberNutrients.GetWhere(x => x.GUID == id);

			if (data != null)
			{
				var deleteId = data.FirstOrDefault().MemberNutrientID;

				_unitOfWork.MemberNutrients.DeleteById(deleteId);
				_unitOfWork.SaveAsync();

				return Ok();
			}

			else
			{
				return BadRequest();
			}
		}

		[HttpPost]
		[Route("CreateRecipe")]
		public IActionResult CreateRecipe([FromBody] Recipe info)
		{
			if (info != null)
			{
				var recipe = info;

				recipe.GUID = Guid.NewGuid();

				if (recipe != null)
				{
					_unitOfWork.Recipes.Add(recipe);
					_unitOfWork.SaveAsync();

					return Ok();
				}

				else
				{
					return BadRequest();
				}
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpPost]
		[Route("UpdateRecipe")]
		public IActionResult UpdateRecipe([FromBody] Recipe recipe)
		{
			if (recipe != null)
			{
				_unitOfWork.Recipes.Update(recipe);
				_unitOfWork.SaveAsync();

				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpGet]
		[Route("DeleteRecipe/{id}")]
		public IActionResult DeleteDeleteRecipe(Guid id)
		{
			var data = _unitOfWork.Recipes.GetWhere(x => x.GUID == id);

			if (data != null)
			{
				var deleteId = data.FirstOrDefault().RecipeID;

				_unitOfWork.Recipes.DeleteById(deleteId);
				_unitOfWork.SaveAsync();

				return Ok();
			}

			else
			{
				return BadRequest();
			}
		}

		[HttpGet]
		[Route("GetRecipes")]
		public IActionResult GetRecipes()
		{
			var data = _unitOfWork.Recipes.GetWhere(x => x.isPublic == true);

			if (data != null)
			{
				var recipes = data.ToList();

				return Ok(recipes);
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpGet]
		[Route("GetRecipe/{id}")]
		public IActionResult GetRecipe(Guid id)
		{
			var data = _unitOfWork.Recipes.GetWhere(x => x.GUID == id);

			if (data != null)
			{
				var recipe = data.FirstOrDefault();

				return Ok(recipe);
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpGet]
		[Route("GetMemberRecipes/{id}")]
		public IActionResult GetMemberRecipes(int id)
		{
			var data = _unitOfWork.Recipes.GetWhere(X => X.MemberID == id);

			if (data != null)
			{
				var recipes = data.ToList();

				return Ok(recipes);
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpGet]
		[Route("GetMemberNutrientLog/{id}")]
		public IActionResult GetMemberNutrientLog(Guid id)
		{
			var data = _unitOfWork.MemberNutrients.GetWhere(x => x.GUID == id);

			if (data != null)
			{
				var nutrients = data.FirstOrDefault();

				return Ok(nutrients);
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpGet]
		[Route("GetMemberNutrientLogs/{id}")]
		public IActionResult GetMemberNutrientLogs(int id)
		{
			var data = _unitOfWork.MemberNutrients.GetWhere(x => x.MemberID == id);

			if (data != null)
			{
				var nutrients = data.ToList();

				return Ok(nutrients);
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpGet]
		[Route("GetRandomRecipe")]
		public IActionResult GetRandomRecipe()
		{
			var data = _unitOfWork.Recipes.GetWhere(x => x.RecipeID > 0 && x.isPublic == true);

			data = data.OrderBy(x => Guid.NewGuid()).Take(3);

			if (data != null)
			{
				var recipe = data.ToList();

				return Ok(recipe);
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpGet]
		[Route("GetNutrientAnalytics/{id}/{info}/{date}")]
		public IActionResult GetNutrientAnalytics(int id, string info, string date)
		{
			List<NutrientAnalytics> NAnalytics = new();

			if (date == "weekly")
			{
				for (int j = 7; j >= 0; j--)
				{
					DateTime MyDate = DateTime.Now.AddDays(j * -1);

					var data = _unitOfWork.MemberNutrients.SqlRaw(MyDate, id);

					if (data != null)
					{
						var list = data.ToList();

						if (info == "totalcarb")
						{
							NutrientAnalytics NAnalytic = new();
							double total = 0;

							foreach (var item in list)
							{
								var tc = _unitOfWork.Nutrients.GetWhere(x => x.NutrientID == item.NutrientID).FirstOrDefault();

								if (tc != null)
								{
									total += (tc.TotalCarb / 100) * item.ServingSize;
								}
								else
								{
									total = 0;
								}
							}

							NAnalytic.X = MyDate.ToString("d.MM");
							NAnalytic.Y = total.ToString("0.00");

							NAnalytics.Add(NAnalytic);
						}

						else if (info == "totalfat")
						{
							NutrientAnalytics NAnalytic = new();
							double total = 0;

							foreach (var item in list)
							{
								var tc = _unitOfWork.Nutrients.GetWhere(x => x.NutrientID == item.NutrientID).FirstOrDefault();

								if (tc != null)
								{
									total += (tc.TotalFat / 100) * item.ServingSize;
								}
								else
								{
									total = 0;
								}
							}

							NAnalytic.X = MyDate.ToString("d.MM");
							NAnalytic.Y = total.ToString("0.00");

							NAnalytics.Add(NAnalytic);
						}

						else if (info == "totalsugar")
						{
							NutrientAnalytics NAnalytic = new();
							double total = 0;

							foreach (var item in list)
							{
								var tc = _unitOfWork.Nutrients.GetWhere(x => x.NutrientID == item.NutrientID).FirstOrDefault();

								if (tc != null)
								{
									total += (tc.TotalSugar / 100) *item.ServingSize;
								}
								else
								{
									total = 0;
								}
							}

							NAnalytic.X = MyDate.ToString("d.MM");
							NAnalytic.Y = total.ToString("0.00");

							NAnalytics.Add(NAnalytic);
						}

						else if (info == "totalprotein")
						{
							NutrientAnalytics NAnalytic = new();
							double total = 0;

							foreach (var item in list)
							{
								var tc = _unitOfWork.Nutrients.GetWhere(x => x.NutrientID == item.NutrientID).FirstOrDefault();

								if (tc != null)
								{
									total += (tc.TotalProtein / 100) * item.ServingSize;
								}
								else
								{
									total = 0;
								}
							}

							NAnalytic.X = MyDate.ToString("d.MM");
							NAnalytic.Y = total.ToString("0.00");

							NAnalytics.Add(NAnalytic);
						}

						else if (info == "totalcalorie")
						{
							NutrientAnalytics NAnalytic = new();
							double total = 0;

							foreach (var item in list)
							{
								var tc = _unitOfWork.Nutrients.GetWhere(x => x.NutrientID == item.NutrientID).FirstOrDefault();

								if (tc != null)
								{
									total += (tc.Calorie / 100) * item.ServingSize;
								}
								else
								{
									total = 0;
								}
							}

							NAnalytic.X = MyDate.ToString("d.MM");
							NAnalytic.Y = total.ToString("0.00");

							NAnalytics.Add(NAnalytic);
						}
					}
					else
					{
						return NotFound();
					}
				}
			}

			else if (date == "monthly")
			{
				for (int j = 30; j >= 0; j--)
				{
					DateTime MyDate = DateTime.Now.AddDays(j * -1);

					var data = _unitOfWork.MemberNutrients.SqlRaw(MyDate, id);

					if (data != null)
					{
						var list = data.ToList();

						if (info == "totalcarb")
						{
							NutrientAnalytics NAnalytic = new();
							double total = 0;

							foreach (var item in list)
							{
								var tc = _unitOfWork.Nutrients.GetWhere(x => x.NutrientID == item.NutrientID).FirstOrDefault();

								if (tc != null)
								{
									total += tc.TotalCarb;
								}
								else
								{
									total = 0;
								}
							}

							NAnalytic.X = MyDate.ToString("d.MM");
							NAnalytic.Y = total.ToString("0.00");

							NAnalytics.Add(NAnalytic);
						}

						else if (info == "totalfat")
						{
							NutrientAnalytics NAnalytic = new();
							double total = 0;

							foreach (var item in list)
							{
								var tc = _unitOfWork.Nutrients.GetWhere(x => x.NutrientID == item.NutrientID).FirstOrDefault();

								if (tc != null)
								{
									total += tc.TotalFat;
								}
								else
								{
									total = 0;
								}
							}

							NAnalytic.X = MyDate.ToString("d.MM");
							NAnalytic.Y = total.ToString("0.00");

							NAnalytics.Add(NAnalytic);
						}

						else if (info == "totalsugar")
						{
							NutrientAnalytics NAnalytic = new();
							double total = 0;

							foreach (var item in list)
							{
								var tc = _unitOfWork.Nutrients.GetWhere(x => x.NutrientID == item.NutrientID).FirstOrDefault();

								if (tc != null)
								{
									total += tc.TotalSugar;
								}
								else
								{
									total = 0;
								}
							}

							NAnalytic.X = MyDate.ToString("d.MM");
							NAnalytic.Y = total.ToString("0.00");

							NAnalytics.Add(NAnalytic);
						}

						else if (info == "totalprotein")
						{
							NutrientAnalytics NAnalytic = new();
							double total = 0;

							foreach (var item in list)
							{
								var tc = _unitOfWork.Nutrients.GetWhere(x => x.NutrientID == item.NutrientID).FirstOrDefault();

								if (tc != null)
								{
									total += tc.TotalProtein;
								}
								else
								{
									total = 0;
								}
							}

							NAnalytic.X = MyDate.ToString("d.MM");
							NAnalytic.Y = total.ToString("0.00");

							NAnalytics.Add(NAnalytic);
						}

						else if (info == "totalcalorie")
						{
							NutrientAnalytics NAnalytic = new();
							double total = 0;

							foreach (var item in list)
							{
								var tc = _unitOfWork.Nutrients.GetWhere(x => x.NutrientID == item.NutrientID).FirstOrDefault();

								if (tc != null)
								{
									total += tc.Calorie;
								}
								else
								{
									total = 0;
								}
							}

							NAnalytic.X = MyDate.ToString("d.MM");
							NAnalytic.Y = total.ToString("0.00");

							NAnalytics.Add(NAnalytic);
						}
					}
					else
					{
						return NotFound();
					}
				}
			}

			return Ok(NAnalytics);
		}
	}
}
