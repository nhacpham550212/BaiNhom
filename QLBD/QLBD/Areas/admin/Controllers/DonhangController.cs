using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ModelBb.EF;
using ModelBb.DB;
using QLBD.Common;

namespace QLBD.Areas.admin.Controllers
{
    public class DonHangController : Controller
    {
        private ModelDbNhom db = new ModelDbNhom();
        // GET: admin/Donhang
        public ActionResult Index()
        {
            var donhangs = db.DonHangs.Where(x => x.status == true).Include(d => d.KhachHang).Include(d => d.NhanVien);
            return View(donhangs.ToList());
        }
        // GET: /admin/DonHang/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donhang = db.DonHangs.Find(id);
            if (donhang == null)
            {
                return HttpNotFound();
            }
            return View(donhang);
        }

        // nhấn nút xác nhận đơn hàng
        public ActionResult XacNhan(long? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donhang = db.DonHangs.Find(id);
            if (donhang == null)
            {
                return HttpNotFound();
            }

            // lấy ra danh sách sẩm phẩm trong chi tiết đơn hàng

            ProductDB pro = new ProductDB();

            TempData["ChitietPro"] = pro.getProbyId(id);
            ViewBag.ngayGH = "";



            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", donhang.MaKH);
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "TenNV", donhang.MaNV);
            return View(donhang);
        }


        [HttpPost]
        public ActionResult XacNhans(DonHang donhang)
        {
            var user = (UserSection)Session["USER_SESSION"];
            var ngayGH = Request["ngayGH"];
            if (ngayGH.Length <= 0)
            {
                TempData["co"] = "Nhập ngày giao hàng";
                return RedirectToAction("XacNhan/" + donhang.SoHoaDon);
            }

            var mt = new ProductDB().getListMatHangBySoHoaDon(donhang.SoHoaDon);
            var kh = new ProductDB().getKhByMaKH(donhang.MaKH);
            var nv = new ProductDB().getNhanVien(user.IDUser);
            // gửi email 



            TempData["ChitietPro"] = mt;
            var body = "Họ Tên: " + kh.TenKH +
                        "Điện Thoại:" + kh.SoDT +
                        "Địa chỉ: " + kh.Diachi +
                        " Thông Tin Sản Sẩm đã Đặt Hàng Tại Lam Sơn" +
                        "";
            double tong = 0;
            foreach (var item in mt)
            {
                body += "Tên Sản Phẩm:" + item.Dia.TenDia +
                    "Mã Sản Phẩm:" + item.Dia.MaTheLoai + item.MaDia +
                    "Số Lượng: " + item.SoLuong +
                    "Giá Thành:" + item.GiaThanh;
                tong += (double)item.GiaThanh * (double)item.SoLuong;
            }
            bool check = false;
            body += "Tổng Tiền: " + tong;
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.From = new System.Net.Mail.MailAddress(user.email);
            mail.To.Add(kh.Email);
            mail.Subject = "Xác nhận giao vận cho đơn hàng" + donhang.SoHoaDon + "từ Lam Sơn";
            mail.Body = body;
            mail.IsBodyHtml = true;
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(user.email, user.pass);
            smtp.EnableSsl = true;
            smtp.Send(mail);
            if (smtp.EnableSsl)
            {
                check = true;
            }
            if (check)
            {
                DonHang dh = db.DonHangs.Find(donhang.SoHoaDon);

                dh.status = false;
                dh.MaNV = nv.MaNV;
                dh.NgayGH = DateTime.Parse(Request["ngayGH"]); ;
                db.Entry(dh).State = EntityState.Modified;
                db.SaveChanges();
                TempData["gh"] = "Thực hiện Giao Hàng Thành Công !";
            }
            else
            {
                TempData["gh"] = "Thực hiện Giao Hàng Thất bại !";
            }

            //return RedirectToAction("XacNhan/" + donhang.SoHoaDon);
            return View("XacNhan");
        }

        // GET: /admin/DonHang/Create
        public ActionResult Create()
        {
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH");
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "TenNV");
            return View();
        }

        // POST: /admin/DonHang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SoHoaDon,MaKH,MaNV,NgayDH,NgayGH,NoiGiaoHang,TongTien,ghichu,status")] DonHang donhang)
        {
            if (ModelState.IsValid)
            {
                db.DonHangs.Add(donhang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", donhang.MaKH);
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "TenNV", donhang.MaNV);
            return View(donhang);
        }

        // GET: /admin/DonHang/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donhang = db.DonHangs.Find(id);
            if (donhang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", donhang.MaKH);
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "TenNV", donhang.MaNV);
            return View(donhang);
        }

        // POST: /admin/DonHang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SoHoaDon,MaKH,MaNV,NgayDH,NgayGH,NoiGiaoHang,TongTien,ghichu,status")] DonHang donhang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donhang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", donhang.MaKH);
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "TenNV", donhang.MaNV);
            return View(donhang);
        }

        // GET: /admin/DonHang/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donhang = db.DonHangs.Find(id);
            if (donhang == null)
            {
                return HttpNotFound();
            }
            return View(donhang);
        }

        // POST: /admin/DonHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            var lct = db.ChiTietDonHangs.Where(x => x.SoHoaDon == id).ToList();
            foreach (var item in lct)
            {
                db.ChiTietDonHangs.Remove(item);
                db.SaveChanges();
            }

            DonHang donhang = db.DonHangs.Find(id);
            db.DonHangs.Remove(donhang);
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