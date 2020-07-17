using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ModelBb.EF;
using QLBD.Common;
using ModelBb.DB;

namespace QLBD.Areas.admin.Controllers
{
    public class ManagerUserController : Controller
    {
        private ModelDbNhom db = new ModelDbNhom();
        // GET: admin/ManagerUser
        [HasCredentialAttb(ID = "VIEW_USER")]
        public ActionResult Index()
        {
            var Users = db.Users.Include(x => x.IDGroup);
            return View(db.Users.ToList());

        }
        private ModelDbNhom db = new ModelDbNhom();

        // GET: /admin/ManagerUser/
        [HasCredentialAttb(ID = "VIEW_USER")]
        public ActionResult Index()
        {
            var Users = db.Users.Include(x => x.IDGroup);
            return View(db.Users.ToList());
        }

        // GET: /admin/ManagerUser/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: /admin/ManagerUser/Create
        [HasCredentialAttb(ID = "ADD_USER")]
        public ActionResult Create()
        {
            ViewBag.IDGroup = new SelectList(db.Groups, "ID", "Name");
            return View();
        }

        // POST: /admin/ManagerUser/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDUser,IDGroup,Email,HoTen,Phone,PassWord,RePassWord,Status,Admin")] User user)
        {
            if (ModelState.IsValid)
            {
                var us = new UserDb().GetUserByEmail(user.Email);
                ViewBag.IDGroup = new SelectList(db.Groups, "ID", "Name", user.IDGroup);
                if (us == null)
                {
                    user.PassWord = Encryptor.MD5Hash(user.PassWord);
                    user.RePassWord = Encryptor.MD5Hash(user.RePassWord);
                    db.Users.Add(user);
                    db.SaveChanges();

                    if (user.IDGroup == "MEMBER" || user.IDGroup == "ADMIN")
                    {
                        NhanVien nh = new NhanVien();
                        nh.Email = user.Email;
                        nh.TenNV = user.HoTen;
                        nh.IDUser = user.IDUser;
                        nh.SoDT = user.Phone;
                        db.NhanViens.Add(nh);
                        db.SaveChanges();
                        TempData["tk"] = "Đăng ký tài khoản thành công !";
                        return View();
                    }
                }
                else
                {
                    TempData["tk"] = "Email đã tồn tại !";
                    return View();
                }




                return RedirectToAction("Index");
            }


            return View(user);
        }

        // GET: /admin/ManagerUser/Edit/5
        [HasCredentialAttb(ID = "EDIT")]
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDGroup = new SelectList(db.Groups, "ID", "Name");
            return View(user);
        }


        // POST: /admin/ManagerUser/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDUser,IDGroup,Email,HoTen,Phone,PassWord,RePassWord,Status,Admin")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDGroup = new SelectList(db.Groups, "ID", "Name", user.IDGroup);
            return View(user);
        }

        // GET: /admin/ManagerUser/Delete/5
        [HasCredentialAttb(ID = "DELETE_USER")]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /admin/ManagerUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}