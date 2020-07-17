using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBD.Common
{
    public class UserSection
    {
        public bool? Admin { get; set; }// để biết phân quyên truy cập
        public string email { get; set; }// lưu email
        public string name { get; set; }// tên đăng nhập
        public string IDGroup { get; set; }// cấp bậc quyền
        public string pass { get; set; }
        public long IDUser { get; set; }// lấy mã khách hàng
    }
}