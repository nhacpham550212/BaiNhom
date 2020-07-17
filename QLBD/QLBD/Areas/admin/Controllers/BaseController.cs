using QLBD.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBD.Areas.admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var session = (UserSection)Session[CommonContact.USER_SESSION];// lấy sesstion đã lưu dán vào sess
            if (session == null)
            {// nếu sesstion không tôn tại thì chuyền về trang Login index

                filterContext.Result = new RedirectToRouteResult
                (new System.Web.Routing.RouteValueDictionary(new { Controller = "UserLogin", action = "Index", area = "Admin" }));
            }
            base.OnActionExecuted(filterContext);
        }
    }
}