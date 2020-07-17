using ModelBb.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace ModelBb.DB
{
    public  class ProductDB
    {
        ModelDbNhom db = null;
        public ProductDB()
        {
            db = new ModelDbNhom();
        }
        // lấy danh sách sản phẩm theo kiểu phân trang
        public IEnumerable<Dia> getPageListProduct(int page, int pageSize)// bắt đầu và kết thúc. tải packe topagelist
        {
            return db.Dias.OrderByDescending(x => x.NgayNhap).ToPagedList(page, pageSize);
        }
        // lấy danh sách sản phẩm

        public List<Dia> getlistAll()
        {
            return db.Dias.ToList();

        }
        // xem chi tiet sản phẩm.


        public Dia getProductByID(long? id)
        {
            return db.Dias.Find(id);

        }

        public List<Dia> ClientSearchPro(string p, string hdh, string giathanh)
        {
            if (giathanh == "desc")
            {
                return db.Dias.Where(x => x.TenDia.Contains(p) || x.MaTheLoai == hdh).OrderByDescending(x => x.GiaThanh).ToList();
            }
            else if (giathanh == "asc")
            {
                return db.Dias.Where(x => x.TenDia.Contains(p) || x.MaTheLoai == hdh).OrderBy(x => x.GiaThanh).ToList();
            }
            else
                if (hdh != null)
            {
                return db.Dias.Where(x => x.MaTheLoai == hdh).OrderBy(x => x.NgayNhap).ToList();
            }
            else
            {
                return db.Dias.Where(x => x.TenDia.Contains(p) || x.MaTheLoai == hdh).OrderByDescending(x => x.NgayNhap).ToList();
            }

        }



        public List<ChiTietDonHang> getProbyId(long? id)
        {
            var data = (from a in db.ChiTietDonHangs
                        where a.SoHoaDon == id
                        select a
                          ).ToList();
            return data;
        }

        public List<ChiTietDonHang> getListMatHangBySoHoaDon(long p)
        {
            var data = (from a in db.DonHangs
                        where a.SoHoaDon == p
                        join ct in db.ChiTietDonHangs on a.SoHoaDon equals ct.SoHoaDon
                        join mh in db.Dias on ct.MaDia equals mh.MaDia
                        select ct
                          ).ToList();
            return data;
        }

        public KhachHang getKhByMaKH(long p)
        {
            var data = (from a in db.KhachHangs where a.MaKH == p select a).First();
            return data;
        }

        public NhanVien getNhanVien(long p)
        {
            var data = (from a in db.NhanViens where a.IDUser == p select a).First();
            return data;
        }
    }
}
