using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TrackItAPI.DataModels;
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

					workout.GUID = Guid.NewGuid();

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

		[HttpPost]
		[Route("UpdateMSWorkout")]
		public IActionResult UpdateMSWorkout([FromBody] MemberSpecificWorkout info)
		{
			if (info != null)
			{
				_unitOfWork.MemberSpecificWorkouts.Update(info);
				_unitOfWork.SaveAsync();

				return Ok();
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
				memberWorkoutLog.GUID = Guid.NewGuid();
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

				return Ok(memberWorkoutLog.GUID);
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpPost]
		[Route("UpdateWorkoutLog")]
		public IActionResult UpdateWorkoutLog([FromBody] MemberWorkoutLog info)
		{
			if (info != null)
			{
				info.UpdatedDate = DateTime.Now;

				_unitOfWork.MemberWorkoutLogs.Update(info);
				_unitOfWork.SaveAsync();

				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpGet]
		[Route("DeleteMWorkouLog/{id}")]
		public IActionResult DeleteMWorkout(Guid id)
		{
			var data = _unitOfWork.MemberWorkoutLogs.GetWhere(x => x.GUID == id);

			if (data != null)
			{
				var deleteId = data.FirstOrDefault().MemberWorkoutID;

				_unitOfWork.MemberWorkoutLogs.DeleteById(deleteId);
				_unitOfWork.SaveAsync();

				return Ok();
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

		[HttpPost]
		[Route("CreateWorkoutLogStat")]
		public IActionResult CreateWorkoutLogStat([FromBody] MemberWorkoutLogStat info)
		{
			if (info != null)
			{
				MemberWorkoutLogStat stat = new();

				stat.MemberID = info.MemberID;
				stat.MemberWorkoutLogID = info.MemberWorkoutLogID;
				stat.ItemID = info.ItemID;
				stat.TableName = info.TableName;
				stat.Reps = info.Reps;
				stat.Weight = info.Weight;

				_unitOfWork.MemberWorkoutLogStat.Add(stat);
				_unitOfWork.SaveAsync();

				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpGet]
		[Route("GetMemberWorkoutLogStat/{memberId}/{id}")]
		public IActionResult GetMemberWorkoutLogStat(int memberId, int id)
		{
			if (memberId > 0 && id > 0)
			{
				var data = _unitOfWork.MemberWorkoutLogStat.GetWhere(x => x.MemberWorkoutLogID == id && x.MemberID == memberId);

				if (data != null)
				{
					var logStat = data.ToList();
					string workoutName = "";
					string muscleGroup = "";

					List<MemberWorkoutLogStat_DM> memberWorkoutLogStats = new();
					foreach (var item in logStat)
					{
						MemberWorkoutLogStat_DM memberWorkoutLogStat = new();

						memberWorkoutLogStat.MemberStatisticID = item.MemberStatisticsID;
						memberWorkoutLogStat.Reps = item.Reps;
						memberWorkoutLogStat.Weight = item.Weight;
						if (item.TableName == "Workout")
						{
							var a = _unitOfWork.Workouts.GetWhere(x => x.WorkoutID == item.ItemID);
							if (a.Any())
							{
								var z = a.FirstOrDefault();	
								 
								workoutName = a.FirstOrDefault().WorkoutName;
								muscleGroup = _unitOfWork.MuscleGroups.GetWhere(x => x.MuscleGroupID == z.MuscleGroupID).FirstOrDefault().Name;
							}
						}
						else if (item.TableName == "MemberWorkout")
						{
							var b = _unitOfWork.MemberSpecificWorkouts.GetWhere(x => x.MemberSpecificWorkoutID == item.ItemID && x.MemberID == memberId);
							if (b.Any())
							{
								var w = b.FirstOrDefault();

								workoutName = w.WorkoutName;
								muscleGroup = _unitOfWork.MuscleGroups.GetWhere(x => x.MuscleGroupID == w.MuscleGroupID).FirstOrDefault().Name;
							}
						}
						memberWorkoutLogStat.MuscleGroup = muscleGroup;
						memberWorkoutLogStat.WorkoutName = workoutName;

						memberWorkoutLogStats.Add(memberWorkoutLogStat);
					}

					return Ok(memberWorkoutLogStats);
				}
				else
				{
					return NotFound();
				}
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpGet]
		[Route("DeleteMemberWorkoutLogStat/{memberId}/{id}")]
		public IActionResult DeleteMemberWorkoutLogStat(int memberId, int id)
		{
			if (memberId > 0 && id > 0)
			{
				var data = _unitOfWork.MemberWorkoutLogStat.GetWhere(x => x.MemberStatisticsID == id && x.MemberID == memberId);

				if (data != null)
				{
					_unitOfWork.MemberWorkoutLogStat.Delete(data.FirstOrDefault());
					_unitOfWork.SaveAsync();

					return Ok();
				}
				else
				{
					return NotFound();
				}
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpGet]
		[Route("GetWorkoutAnalytics/{id}/{date}")]
		public IActionResult GetWorkoutAnalytics(int id, string date)
		{
			IEnumerable<MemberWorkoutLog> data = Enumerable.Empty<MemberWorkoutLog>();

			int days = 0;

			if (date == "weekly") { days = -7; }
			else if (date == "monthly") { days = -31; }
			else { return BadRequest(); }

			data = _unitOfWork.MemberWorkoutLogs.GetWhere(x => x.MemberID == id && x.CreatedDate > DateTime.Now.AddDays(days) && x.CreatedDate <= DateTime.Now);

			if (data.Any())
			{
				List<WorkoutAnalytics> workoutAnalyticsList = new();

				var list = data.ToList();

				foreach (var log in list)
				{
					WorkoutAnalytics workoutAnalytics = new();
					List<WorkoutLogStat> workoutLogStats = new();

					var stats = _unitOfWork.MemberWorkoutLogStat.GetWhere(x => x.MemberWorkoutLogID == log.MemberWorkoutID).ToList();

					foreach (var stat in stats)
					{
						WorkoutLogStat workoutLogStat = new();

						workoutLogStat.Weight = stat.Weight.ToString();
						workoutLogStat.Reps = stat.Reps.ToString();

						string str = "";
						if (stat.TableName == "Workout")
						{
							str = _unitOfWork.Workouts.GetWhere(x => x.WorkoutID == stat.ItemID).FirstOrDefault().WorkoutName;
							workoutLogStat.Name = str;
						}
						else if (stat.TableName == "MemberWorkout")
						{
							str = _unitOfWork.MemberSpecificWorkouts.GetWhere(x => x.MemberSpecificWorkoutID == stat.ItemID).FirstOrDefault().WorkoutName;
							workoutLogStat.Name = str;
						}
						workoutLogStats.Add(workoutLogStat);
					}

					workoutAnalytics.Name = log.MemberWorkoutName;
					workoutAnalytics.Logs = workoutLogStats;

					workoutAnalyticsList.Add(workoutAnalytics);
				}

				return Ok(workoutAnalyticsList);
			}
			else
			{
				return NotFound();
			}
		}
	}
}
