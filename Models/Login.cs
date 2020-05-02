using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace infertilityAndBeyondProject.Models
{
    public class Login
    {
        [NotMapped]
        [Required(ErrorMessage = "An Email Input is Required")]
        [EmailAddress(ErrorMessage = "Email is Invalid")]
        public string Email { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "A Password Input is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}