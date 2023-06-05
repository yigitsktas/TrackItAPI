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

		[HttpGet]
		[Route("CreateMemberNutrient/{info}/{name}")]
		public IActionResult CreateMemberNutrient(string info, string name)
		{
			if (!string.IsNullOrEmpty(info))
			{
				var nutrient = JsonConvert.DeserializeObject<MemberNutrient>(info);

				if (string.IsNullOrEmpty(name))
				{
					nutrient.NutrientID = _unitOfWork.Nutrients.GetIDByName(name);
				}
				else if (name != "a")
				{
					nutrient.NutrientID = 1;
				}

				_unitOfWork.MemberNutrients.Add(nutrient);
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

		[HttpGet]
		[Route("CreateRecipe/{info}")]
		public IActionResult CreateRecipe(string info)
		{
			if (!string.IsNullOrEmpty(info))
			{
				var recipe = JsonConvert.DeserializeObject<Recipe>(info);

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
			var data = _unitOfWork.Recipes.GetAll();

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
		[Route("GetMemberNutrientFilteredLogs/{name}/{orderBy}/{id}")]
		public IActionResult GetMemberNutrientFilteredLogs(string name, string orderBy, int id)
		{
			var predicate = PredicateBuilder.True<MemberNutrient>();

			if (id > 0)
			{
				predicate = predicate.And(x => x.MemberID == id);
			}
			if (!string.IsNullOrEmpty(name))
			{
				var srch = _unitOfWork.Nutrients.GetWhere(x => x.NutrientName.Contains(name)).FirstOrDefault();
				int ntrid = 0;

				if (srch != null)
				{
				  ntrid = srch.NutrientID;
				}

				if (ntrid > 0)
				{
					predicate = predicate.And(x => x.NutrientID == ntrid);
				}
			}
	
			var data = _unitOfWork.MemberNutrients.GetWhere(predicate);

			if (data != null)
			{
				var response = new List<MemberNutrient>();

				if (!string.IsNullOrEmpty(orderBy))
				{
					if (orderBy == "date-asc")
					{
						var nutrients = data.OrderBy(x => x.CreatedDate).ToList();
						response = nutrients;
					}
					if (orderBy == "date-desc")
					{
						var nutrients = data.OrderByDescending(x => x.CreatedDate).ToList();
						response = nutrients;
					}
					if (orderBy == "0")
					{
						var nutrients = data.ToList();
						response = nutrients;
					}
				}

				return Ok(response);
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
			var data = _unitOfWork.Recipes.GetWhere(x => x.RecipeID > 0);

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
		public IActionResult GetRandomRecipe(int id, string info, string date)
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
