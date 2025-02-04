using System.ComponentModel.DataAnnotations;

namespace Auth.Core.Models
{
	public class User
	{
		public int UserId { get; set; }

		[StringLength(25)]
		public string Handle { get; set; }		

		[StringLength(15)]
		public string PhoneNumber { get; set; }

		[StringLength(255)]
		public string Email { get; set; }

		[StringLength(1024)]
		public string AvatarUrl { get; set; }
	}
}