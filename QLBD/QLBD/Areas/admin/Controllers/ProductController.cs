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

namespace QLBD.Areas.admin.Controllers
{
    public class ProductController : Controller
    {
        private ModelDbNhom db = new ModelDbNhom();

        // GET: /admin/Product/
        [HasCredentialAttb(ID = "VIEW_PRO")]
        public ActionResult Index()
        {
            var mathangs = db.Dias.Include(m => m.TheLoaiDia);
            return View(mathangs.ToList());
        }

        // GET: /admin/Product/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dia mathang = db.Dias.Find(id);
            if (mathang == null)
            {
                return HttpNotFound();
            }
            return View(mathang);
        }

        // GET: /admin/Product/Create
        [HasCredentialAttb(ID = "ADD_PRO")]
        public ActionResult Create()
        {
            ViewBag.MaHDH = new SelectList(db.Dias, "MaHDH", "TenHDH");
            ViewBag.MaLoaiHang = new SelectList(db.TheLoaiDias, "MaLoaiHang", "TenLoaiMH");
            string dateAsString = DateTime.Now.ToString("MM/dd/yyyy");
            ViewBag.date = dateAsString;

            return View();
        }

        // POST: /admin/Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaMH,TenMh,GiaThanh,GiaKhuyenMai,Soluong,MaLoaiHang,NgayNhap,NgaySuaDoi,Image,status,ManHinh,Ram,CameraT,CameraS,Cpu,Gpu,BoNho,DungLuongPin,luotXem,MaHDH")] MatHang mathang)
        {
            var imgNV = Request.Files["Image"];
            //Lấy thông tin từ input type=file có tên Avatar
            string postedFileName = System.IO.Path.GetFileName(imgNV.FileName);
            //Lưu hình đại diện về Server
            object s = mathang.TheLoaiDia;
            var path = Server.MapPath("/Images/" + postedFileName);
            string ngay = Request["ngayNhap"];
            if (postedFileName.Length > 0)
            {
                imgNV.SaveAs(path);

                if (ngay.Length > 0)
                {

                    if (ModelState.IsValid)
                    {
                        DateTime ngaynhap = DateTime.Parse(ngay);
                        mathang.NgayNhap = ngaynhap;
                        mathang.Image = postedFileName;
                        db.Dias.Add(mathang);
                        db.SaveChanges();
                        ModelState.AddModelError("", "Thêm thành công");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm không thành công");
                    }


                }
                else
                {
                    ModelState.AddModelError("", "Ngày nhập sản phẩm còn trống !");
                }


            }
            else
            {
                ModelState.AddModelError("", "Chưa có một ảnh mô tả sản phẩm");
            }

            ViewBag.MaHDH = new SelectList(db.Dias, "MaHDH", "TenHDH", mathang.MaHDH);
            ViewBag.MaLoaiHang = new SelectList(db.TheLoaiDias, "MaLoaiHang", "TenLoaiMH", mathang.MaLoaiHang);
            return View(mathang);
        }




        // GET: /admin/Product/Edit/5
        [HasCredentialAttb(ID = "EDIT_PRO")]
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dia mathang = db.Dias.Find(id);
            if (mathang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHDH = new SelectList(db.TheLoaiDias, "MaHDH", "TenHDH", mathang.MaTheLoai); 
            return View(mathang);
        }

        // POST: /admin/Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaMH,TenMh,GiaThanh,GiaKhuyenMai,Soluong,MaLoaiHang,NgayNhap,NgaySuaDoi,Image,status,ManHinh,Ram,CameraT,CameraS,Cpu,Gpu,BoNho,DungLuongPin,luotXem,MaHDH")] MatHang mathang)
        {
            string ngay = Request["ngaysuadoi"];
            if (ngay.Length > 0)
            {
                if (ModelState.IsValid)
                {
                    var imgNV = Request.Files["Image"];
                    //Lấy thông tin từ input type=file có tên Avatar
                    string postedFileName = System.IO.Path.GetFileName(imgNV.FileName);
                    if (postedFileName.Length > 0)
                    {
                        var path = Server.MapPath("/Images/" + postedFileName);
                        imgNV.SaveAs(path);
                        mathang.Image = postedFileName;
                    }
                    DateTime ngaysua = DateTime.Parse(ngay);
                    mathang.NgaySuaDoi = ngaysua;
                    db.Entry(mathang).State = EntityState.Modified;
                    db.SaveChanges();
                    ModelState.AddModelError("", "Chỉnh sửa thành Công");
                }
                else
                {
                    ModelState.AddModelError("", "Chỉnh sửa Không thành Công");
                }
            }
            else
            {
                ModelState.AddModelError("", "Ngày Chỉnh sửa còn trống");
            }
            ViewBag.MaHDH = new SelectList(db.HeDieuHanhs, "MaHDH", "TenHDH", mathang.MaHDH);
            ViewBag.MaLoaiHang = new SelectList(db.LoaiMatHangs, "MaLoaiHang", "TenLoaiMH", mathang.MaLoaiHang);
            return View(mathang);
        }

        // GET: /admin/Product/Delete/5
        [HasCredentialAttb(ID = "DELETE_PRO")]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatHang mathang = db.MatHangs.Find(id);
            if (mathang == null)
            {
                return HttpNotFound();
            }
            return View(mathang);
        }

        // POST: /admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            MatHang mathang = db.MatHangs.Find(id);
            db.MatHangs.Remove(mathang);
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