using System.ComponentModel.DataAnnotations;

namespace Auth.Core.Models
{
    public class RegistrationModel
    {
        [StringLength(25)]
        public string Handle { get; set; }

        [StringLength(60, MinimumLength = 6)]
        public string Password {get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [StringLength(255)]
        public string Email { get; set; }
    }
}