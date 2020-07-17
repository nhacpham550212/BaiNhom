using ModelBb.Common;
using ModelBb.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelBb.DB
{
    public class UserDb
    {
        ModelDbNhom db = null;
        public UserDb()
        {
            db = new ModelDbNhom();
        }

        public User GetUserByEmail(string Email)// kiem tra user theo email
        {

            return db.Users.SingleOrDefault(x => x.Email == Email);
        }
        public KhachHang GetKhachhangByEmail(string Email)// kiem tra user theo email
        {

            return db.KhachHangs.SingleOrDefault(x => x.Email == Email);
        }
        public string InsertUser(User model)// insert user đen db
        {
            db.Users.Add(model);

            db.SaveChanges();
            return model.Email;
        }

        public List<string> GetListCredential(string Email)
        {
            var user = db.Users.Single(x => x.Email == Email);// lấy email vưa đăng nhập

            var data =
            (from a in db.Groups
             join b in db.Redentials on a.ID equals b.UserGroupID
             where user.Email == user.Email
             join c in db.Roles on b.RoleID equals c.ID
             select new { groupID = a.ID, RoleID = c.ID });
            return data.Select(x => x.RoleID).ToList();
            //var data = (from a in db.Groups//
            //            join b in db.Redentials on a.ID equals b.UserGroupID
            //            join c in db.Roles on b.RoleID equals c.ID // join các bản quyền với nhau
            //            where a.ID == user.IDGroup// xem email này thuộc quyền nào
            //            select new 
            //            {
            //                RoleID = c.ID,
            //                UserGroupID = a.ID
            //            }).AsEnumerable().Select(x => new Redential()
            //           {
            //               RoleID=x.RoleID,
            //               UserGroupID=x.UserGroupID
            //           });
            //return data.Select(x => x.RoleID).ToList();
        }

        // phan quyen
        public int LoginUser(string Email, string passWord)
        {
            var result = db.Users.SingleOrDefault(x => x.Email == Email);
            if (result == null)
            {
                return 0;// không tồn tại
            }
            else
            {       // nhân viên và chủ cửa hàng đăng nhập
                if (result.IDGroup == commonContants.ADMIN || result.IDGroup == commonContants.MEMBER)
                {
                    if (result.Status == false)
                    {
                        return -1;// khóa
                    }
                    else
                    {
                        if (result.PassWord == passWord)
                        {
                            return 1;
                        }
                        else
                        {
                            return -2;// sai pass
                        }
                    }
                }
                else// khách hàng đăng nhập
                {
                    if (result.Status == false)
                    {
                        return -1;
                    }
                    else
                    {
                        if (result.PassWord == passWord)
                        {
                            return 3;
                        }
                        else
                        {
                            return -2;
                        }
                    }
                }




            }

        }

        public long getNewDonHang(long p1, bool p2)
        {
            var datas = (from a in db.DonHangs
                         where a.MaKH == p1 && a.status == true
                         select new { SoHoaDon = a.SoHoaDon });
            return datas.Select(x => x.SoHoaDon).First();
        }

        public long getKhachHangByID(long p)
        {
            var data = (from a in db.KhachHangs
                        where a.IDUser == p
                        select new { MaKH = a.MaKH }).OrderByDescending(x => x.MaKH);

            return data.Select(x => x.MaKH).First();
        }
        public NhanVien getNhaVien(long id)
        {

            return db.NhanViens.Where(x => x.IDUser == id).First();

        }

    }
}
