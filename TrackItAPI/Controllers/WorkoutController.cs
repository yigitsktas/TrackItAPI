using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TrackItAPI.Entities;
using TrackItAPI.Helpers;
using TrackItAPI.UnitOfWork;
using static Azure.Core.HttpHeader;

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
        public IActionResult GetMSWorkouts(int id)
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
		[Route("GetMSWorkout/{id}")]
		public IActionResult GetMSWorkout(Guid id)
		{
			var data = _unitOfWork.MemberSpecificWorkouts.GetWhere(x => x.GUID == id);

			if (data != null)
			{
				var specWorkout = data.FirstOrDefault();

				return Ok(specWorkout);
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
					if (!string.IsNullOrEmpty(workout.Link))
					{
						workout.Link = "https://www.youtube.com/watch?v=" + workout.Link;
					}
					else
					{
						workout.Link = string.Empty;
					}

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
		[Route("CreateMWorkoutLog/{name}/{notes}/{id}")]
		public IActionResult CreateMWorkoutLog(string name, string notes, int id)
		{
			if (!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(notes))
			{
				MemberWorkoutLog memberWorkoutLog = new();

				memberWorkoutLog.MemberID = id;
				memberWorkoutLog.MemberWorkoutName = name;
				if (notes == "null")
				{
					memberWorkoutLog.Notes = "";
				}
				else
				{
					memberWorkoutLog.Notes = notes;
				}
				memberWorkoutLog.isDone = false;
				memberWorkoutLog.CreatedDate = DateTime.Now;
				memberWorkoutLog.UpdatedDate = DateTime.Now;

				_unitOfWork.MemberWorkoutLogs.Add(memberWorkoutLog);
				_unitOfWork.SaveAsync();

				return Ok(memberWorkoutLog.MemberWorkoutID);
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpGet]
		[Route("GetMWorkoutLogs/{id}")]
		public IActionResult GetMWorkoutLogs(int id)
		{
			var data = _unitOfWork.MemberWorkoutLogs.GetWhere(x => x.MemberID == id);

			if (data != null)
			{
				var memberWorkoutLogs = data.ToList();

				return Ok(memberWorkoutLogs);
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpGet]
		[Route("GetMWorkoutLog/{id}")]
		public IActionResult GetMWorkoutLog(Guid id)
		{
			var data = _unitOfWork.MemberWorkoutLogs.GetWhere(x => x.GUID == id);

			if (data != null)
			{
				var workoutLog = data.FirstOrDefault();

				return Ok(workoutLog);
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpGet]
        [Route("DeleteMSWorkout/{id}")]
        public IActionResult DeleteMSWorkout(Guid id)
        {
            var data = _unitOfWork.MemberSpecificWorkouts.GetWhere(x => x.GUID == id);

            if (data != null)
            {
				var deleteId = data.FirstOrDefault().MemberSpecificWorkoutID;

                _unitOfWork.MemberSpecificWorkouts.DeleteById(deleteId);
                _unitOfWork.SaveAsync();

                return Ok();
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
		public IActionResult GetWorkout(Guid id)
		{
			var data = _unitOfWork.Workouts.GetWhere(x => x.GUID == id);

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

		[HttpGet]
		[Route("GetRandomWorkout")]
		public IActionResult GetRandomWorkout()
		{
			var data = _unitOfWork.Workouts.GetWhere(x => x.WorkoutID > 0);

			data = data.OrderBy(x => Guid.NewGuid()).Take(3);

			if (data != null)
			{
				var workout = data.ToList();

				return Ok(workout);
			}
			else
			{
				return BadRequest();
			}
		}
	}
}
