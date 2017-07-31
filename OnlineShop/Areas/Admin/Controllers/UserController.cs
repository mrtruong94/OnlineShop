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
        public ActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            var dao = new UserDao();
            var model = dao.ListAll(pageNumber, pageSize);
            return View(model);
        }

        [HttpGet] //Dành cho return 1 View
        public ActionResult Create()
        {
            return View();
        }  
 
        public ActionResult Edit(int id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);
        }  
        [HttpPost] //Dành cho post thông tin tạo mới user

        //phương thức tạo mới user
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
        [HttpPost]
        // phương thức edit user
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid) //Nếu có validate form
            {
                var dao = new UserDao();
                //kiểm tra mật khẩu nhập vào khác rỗng mới thực hiện update
                if (!string.IsNullOrEmpty(user.Password))
                {
                    var encrytedPass = Encrypt.MD5Hash(user.Password);
                    user.Password = encrytedPass;
                }
                var result = dao.Update(user);

                if (result)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật User không thành công!");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        //Xoa user
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}