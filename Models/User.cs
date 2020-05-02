using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace infertilityAndBeyondProject.Models
{
    public class User
    {
        [Key]
        public int UserID {get; set;}

        [Required(ErrorMessage = "A First name is Required!")]
        [MinLength(2, ErrorMessage = "Name needs to be at least 2 characters long")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "A Last name is required")]
        [MinLength(2, ErrorMessage = "Name needs to be at least 2 characters long")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "An Email is Required!")]
        [EmailAddress(ErrorMessage = "Email is Invalid")]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Needs to be at least 8 characters long")]
        public string Password { get; set; }
        
        [NotMapped]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        public List<Message> CreatedMessages { get; set; } = new List<Message> ();

        public List<Comment> CreatedComments { get; set; } = new List<Comment> ();

        [Required]
        public DateTime created_at {get; set;} = DateTime.Now;
        [Required]
        public DateTime updated_at {get; set;} = DateTime.Now;



    }
}