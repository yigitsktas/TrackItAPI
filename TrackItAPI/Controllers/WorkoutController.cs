using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TrackItAPI.Entities;
using TrackItAPI.Helpers;
using TrackItAPI.UnitOfWork;

namespace TrackItAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public WorkoutController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("GetMSWorkouts/{id}")]
        public IActionResult GetMSWorkout(int id)
        {
            if (id > 0)
            {
                var data = _unitOfWork.MemberSpecificWorkouts.GetWhere(x => x.MemberID == id);

                if (data != null)
                {
                    var specWorkout = data.ToList();

                    return Ok(specWorkout);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("CreateMSWorkout/{info}")]
        public IActionResult CreateMSWorkout(string info)
        {
            if (info != null)
            {
                var workout = JsonConvert.DeserializeObject<MemberSpecificWorkout>(info);

                if (workout != null)
                {
					workout.Link = "https://www.youtube.com/watch?v=" + workout.Link;

					_unitOfWork.MemberSpecificWorkouts.Add(workout);
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
		[Route("GetWorkouts")]
		public IActionResult GetWorkouts()
        {
			var data = _unitOfWork.Workouts.GetAll();

			if (data != null)
			{
				var workouts = data.ToList();

				return Ok(workouts);
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpGet]
		[Route("GetWorkout/{id}")]
		public IActionResult GetWorkout(int id)
		{
			var data = _unitOfWork.Workouts.GetWhere(x => x.WorkoutID == id);

			if (data != null)
			{
				var workout = data.FirstOrDefault();

				return Ok(workout);
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpGet]
		[Route("GetWorkoutType/{id}")]
		public IActionResult GetWorkoutType(int id)
		{
			var data = _unitOfWork.WorkoutTypes.GetWhere(x => x.WorkoutTypeID == id);

			if (data != null)
			{
				var workoutType = data.FirstOrDefault();

				return Ok(workoutType);
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpGet]
		[Route("GetMuscleGroup/{id}")]
		public IActionResult GetMuscleGroup(int id)
		{
			var data = _unitOfWork.MuscleGroups.GetWhere(x => x.MuscleGroupID == id);

			if (data != null)
			{
				var muscleGroup = data.FirstOrDefault();

				return Ok(muscleGroup);
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpGet]
		[Route("GetWorkoutTypes")]
		public IActionResult GetWorkoutTypes()
		{
			var data = _unitOfWork.WorkoutTypes.GetWhere(x => x.WorkoutTypeID > 0);

			if (data != null)
			{
				var workoutTypes = data.ToList();

				return Ok(workoutTypes);
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpGet]
		[Route("GetMuscleGroups")]
		public IActionResult GetMuscleGroups()
		{
			var data = _unitOfWork.MuscleGroups.GetWhere(x => x.MuscleGroupID > 0);

			if (data != null)
			{
				var muscleGroups = data.ToList();

				return Ok(muscleGroups);
			}
			else
			{
				return BadRequest();
			}
		}
	}
}
