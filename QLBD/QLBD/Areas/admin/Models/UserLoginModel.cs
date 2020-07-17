using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace QLBD.Areas.admin.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Mời nhập Email !")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mời nhập passWord! !")]
        public string PassWord { get; set; }
        public bool RememberMe { get; set; }
    }
}