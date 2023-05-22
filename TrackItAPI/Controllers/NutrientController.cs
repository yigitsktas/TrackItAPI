using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TrackItAPI.Entities;
using TrackItAPI.UnitOfWork;

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

        [HttpPost]
        [Route("CreateRecipe/{info}")]
        public IActionResult CreateRecipe(string info)
        {     
            if (string.IsNullOrEmpty(info))
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
		public IActionResult GetRecipe(int id)
		{
			var data = _unitOfWork.Recipes.GetWhere(x => x.RecipeID == id);

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
		public IActionResult GetMemberNutrientLog(int id)
		{
			var data = _unitOfWork.MemberNutrients.GetWhere(x => x.MemberNutrientID == id);

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
	}
}
