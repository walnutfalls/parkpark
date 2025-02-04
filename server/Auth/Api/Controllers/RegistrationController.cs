using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Auth.Core.Models;
using Auth.Core;
using Auth.Core.Interfaces;
using System;
using System.Linq;
using Auth.Api.Models;

namespace Auth.Api.Controllers
{
	public class RegistrationController : ControllerBase
	{
		private IRegistration _registration;
		private IConfiguration _configuration;

		public RegistrationController(IConfiguration configuration, IRegistration registration)
		{
			_registration = registration;
			_configuration = configuration;
		}

		private string Host => HttpContext.Request.Host.ToString();
		private Uri VerifyEmailBaseUri => new Uri($"{HttpContext.Request.Scheme}://{Host}{Url.Action("VerifyEmail")}");

		/// <summary>
		/// Register method that attempts to register user. 
		/// </summary>
		/// <param name="request">Registration form:
		/// 
		/// handle : string
		/// password : string
		/// phoneNumber : string
		/// email : string
		/// 
		/// </param>
		/// <returns>Http code indicating result</returns>
		[AllowAnonymous]
		[HttpPost]
		public async Task<IActionResult> Index([FromBody] RegistrationModel request)
		{
			if(!ModelState.IsValid)
			{
				var badProps = ModelState.Where(p => 
					p.Value.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid
				);

				return StatusCode(400, new { errors = badProps.Select(p => new ValidationError(p.Value, p.Key)) });
			}

			try
			{
				await _registration.Register(request, VerifyEmailBaseUri);
				return Ok();
			}
			catch (AuthenticationException ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		/// <summary>
		/// Url to verify account
		/// </summary>
		/// <param name="verificationTokenId">Pre-given verification token</param>
		/// <param name="userId">user id</param>
		/// <returns></returns>
		[AllowAnonymous]
		[HttpGet]
		public async Task<IActionResult> VerifyEmail(string verificationTokenId, int userId)
		{
			try
			{
				await _registration.VerifyEmail(verificationTokenId, userId);
				return Ok();
			}
			catch (AuthenticationException)
			{
				return NotFound();
			}
		}
	}
}