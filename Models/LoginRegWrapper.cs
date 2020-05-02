  
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace infertilityAndBeyondProject.Models
{
    public class LoginRegWrapper
    {
        public User User { get; set; }
        public Login Login { get; set; }
    }
}