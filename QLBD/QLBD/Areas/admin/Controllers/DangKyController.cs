using ModelBb.EF;
using ModelBb.DB;
using QLBD.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBD.Areas.admin.Controllers
{
    public class DangKyController : Controller
    {
        // GET: admin/DangKy
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(User model)
        {
            if (ModelState.IsValid)
            {
                if (model.RePassWord == model.PassWord)
                {
                    var db = new UserDb();
                    var result = db.GetUserByEmail(model.Email);
                    if (model.Status == true)
                    {
                        if (result == null)
                        {

                            var password = Encryptor.MD5Hash(model.PassWord);// mã hóa pass
                            model.PassWord = password;
                            model.IDGroup = "CUSTOMER";
                            model.RePassWord = Encryptor.MD5Hash(model.RePassWord);
                            db.InsertUser(model);
                            TempData["create"] = "Tạo Tài Khoản Thành Công !";
                            //return RedirectToAction("Index", "UserLogin");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Email Đã Tồn Tại !");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Hãy Nhấn xác nhận đăng ký để thành công !");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Mật khẩu phải giống nhau chứ  !");
                }


            }

            return View();
        }
    }
}