using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.DAO;
using OnlineShop.Common;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet] //Dành cho return 1 View
        public ActionResult Create()
        {
            return View();
        }    
        [HttpPost] //Dành cho post thông tin tạo mới user
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid) //Nếu có validate form
            {
                var dao = new UserDao();

                var encrytedPass = Encrypt.MD5Hash(user.Password);
                user.Password = encrytedPass;
                long id = dao.Insert(user);
                if (id > 0)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm User thành công!");
                }
            }
            return View("Index");
        }
    }
}