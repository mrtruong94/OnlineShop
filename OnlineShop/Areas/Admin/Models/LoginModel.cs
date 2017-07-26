using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace OnlineShop.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Nhập tên đăng nhập")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "Nhập mật khẩu!")]
        public string Password { set; get; }

        public bool RememberMe { set; get; }
    }
}