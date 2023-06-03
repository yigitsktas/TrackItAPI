using Microsoft.AspNetCore.Authentication.Cookies;
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

        [HttpGet]
        [Route("CreateUser/{info}")]
        public IActionResult CreateUser(string info)
        {
            if (info == null)
            {
                return BadRequest();
            }
            else
            {
                var member = JsonConvert.DeserializeObject<Member>(info);

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
        [Route("CreateMemberMetric/{info}")]
        public IActionResult CreateMemberMetric(string info)
        {
            if (!string.IsNullOrEmpty(info))
            {
                var memberMetric = JsonConvert.DeserializeObject<MemberMetric>(info);

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
                        _unitOfWork.MemberMetrics.Add(memberMetric);
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
			string apiKey = "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";
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
	}
}
