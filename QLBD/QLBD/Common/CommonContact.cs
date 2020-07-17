using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBD.Common
{
    public class CommonContact
    {
        public static string USER_SESSION = "USER_SESSION";// tên để truy cập lưu trử session
        public static string SESTION_CREDENTIAL = "SESTION_CREDENTIAL";
        public static string Create_SESSION = "Create_SESSION";
        public static string ADMIN = "ADMIN";
        public static string MEMBER = "MEMBER";

        public static string CurrentCulture { get; set; }
    }
}