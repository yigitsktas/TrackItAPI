﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Security.Claims;
using TrackItAPI.UnitOfWork;
using TrackItAPI.Helpers;
using TrackItAPI.Entities;
using Newtonsoft.Json;
using Microsoft.Net.Http.Headers;
using System.Collections.Specialized;
using System.Security.Cryptography;
using OpenAI_API.Completions;
using OpenAI_API;
using TrackItAPI.DataModels;

namespace TrackItAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("GetMember/{id}")]
        public IActionResult GetMember(string id)
        {
            var member = _unitOfWork.Members.GetByID(Convert.ToInt32(id));

            if (member != null)
            {
                return Ok(member);
            }

            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("CreateUser")]
        public IActionResult CreateUser([FromBody] Member info)
        {
            if (info == null)
            {
                return BadRequest();
            }
            else
            {
                var member = info;

                if (member != null)
                {
                    var isAny = _unitOfWork.Members.GetWhere(x => x.Username == member.Username || x.EMail == member.EMail).Any();

                    if (isAny == true)
                    {
                        return BadRequest();
                    }
                    else
                    {
                        member.Password = BCrypt.Net.BCrypt.HashPassword(member.Password);

                        _unitOfWork.Members.Add(member);
                        _unitOfWork.SaveAsync();

                        return Ok(member);
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        [HttpGet]
        [Route("Login/{username}/{password}")]
        public IActionResult Login(string username, string password)
        {
            var data = _unitOfWork.Members.GetWhere(x => (x.EMail == username || x.Username == username));

            if (data.Any() != false)
            {
                var member = data.FirstOrDefault();

                bool isValid = BCrypt.Net.BCrypt.Verify(password, member.Password);

                if (isValid == true)
                {
                    Response.Cookies.SetCookie("sid", member.MemberID.ToString(), 100);

                    return Ok(member);
                }
                else
                {
                    return BadRequest();
                }
            }

            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("Logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("sid");
            return Ok();
        }

        [HttpGet]
        [Route("GetMemberMetric/{id}")]
        public IActionResult GetMemberMetric(string id)
        {
            var data = _unitOfWork.MemberMetrics.GetWhere(x => x.MemberID == Convert.ToInt32(id));

            if (data != null)
            {
                var memberMetric = data.FirstOrDefault();

                return Ok(memberMetric);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("UpdateMember/{info}")]
        public IActionResult UpdateMemberMetric(string info)
        {
            if (!string.IsNullOrEmpty(info))
            {
                var memberMetric = JsonConvert.DeserializeObject<MemberUpdate_DM>(info);

                if (memberMetric != null)
                {
                    if (memberMetric.MemberMetricID > 0)
                    {
                        var data = _unitOfWork.MemberMetrics.GetByID(memberMetric.MemberMetricID);

                        if (data != null)
                        {
                            data.Weight = memberMetric.Weight;
                            data.Height = memberMetric.Height;
                            data.BMI = memberMetric.BMI;
                            data.CreatedDate = DateTime.Now;

                            _unitOfWork.MemberMetrics.Update(data);
							_unitOfWork.SaveAsync();
						}
					}
                    else
                    {
                        MemberMetric memberMetricAdd = new();

						memberMetricAdd.MemberID = memberMetric.MemberID;
						memberMetricAdd.Weight = memberMetric.Weight;
						memberMetricAdd.Height = memberMetric.Height;
						memberMetricAdd.BMI = memberMetric.BMI;
						memberMetricAdd.CreatedDate = DateTime.Now;    

						_unitOfWork.MemberMetrics.Add(memberMetricAdd);
                        _unitOfWork.SaveAsync();
                    }
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
		[Route("GetAnswer/{prompt}")]
		public IActionResult GetResult(string prompt)
		{
			string apiKey = "";
			string answer = string.Empty;
			var openai = new OpenAIAPI(apiKey);
			CompletionRequest completion = new CompletionRequest();
			completion.Prompt = prompt;
			completion.Model = "text-davinci-003";
			completion.MaxTokens = 4000;
			var result = openai.Completions.CreateCompletionAsync(completion);
			if (result != null)
			{
				foreach (var item in result.Result.Completions)
				{
					answer = item.Text;
				}
				return Ok(answer);
			}
			else
			{
				return BadRequest("Not found");
			}
		}

		[HttpPost]
		[Route("CreateChatLog")]
		public IActionResult CreateChatLog([FromBody]ChatLog info)
		{
			if (info != null)
			{
				var data = info;

				if (data != null)
				{
	                ChatLog chatLog = new ChatLog();

                    chatLog.GUID = Guid.NewGuid();  
                    chatLog.MemberID = data.MemberID;
                    chatLog.Answer = data.Answer;
                    chatLog.Prompt = data.Prompt;
                    chatLog.CreatedDate = DateTime.Now;

                    _unitOfWork.ChatLogs.Add(chatLog);
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
		[Route("GetChatLogs/{id}")]
		public IActionResult GetChatLogs(int id)
		{
            var data = _unitOfWork.ChatLogs.GetWhere(x => x.MemberID == id);

            if (data != null)
            {
                var chatLogs = data.ToList();

                return Ok(chatLogs);
            }
            else
            {
                return BadRequest();
            }
		}

		[HttpGet]
		[Route("GetChatLog/{id}")]
		public IActionResult GetChatLog(Guid id)
		{
			var data = _unitOfWork.ChatLogs.GetWhere(x => x.GUID == id);

			if (data != null)
			{
				var chatLogs = data.FirstOrDefault();

				return Ok(chatLogs);
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpGet]
		[Route("DeleteChatLog/{id}")]
		public IActionResult DeleteChatLog(Guid id)
		{
			var data = _unitOfWork.ChatLogs.GetWhere(x => x.GUID == id);

			if (data != null)
			{
				var deleteId = data.FirstOrDefault().LogID;

                _unitOfWork.ChatLogs.DeleteById(deleteId);
                _unitOfWork.SaveAsync();

				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}
	}
}
