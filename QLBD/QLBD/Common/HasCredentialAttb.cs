using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBD.Common
{
    public class HasCredentialAttb: AuthorizeAttribute
    {
        public string ID { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //var isAuthorized = base.AuthorizeCore(httpContext);
            //if (!isAuthorized) { return false; }

            var section = (UserSection)HttpContext.Current.Session[CommonContact.USER_SESSION];
            if (section == null)
            {
                return false;
            }
            List<string> privi = this.GetcredentialByloggedInUser();// lấy ra danh sách quyền
            if (section.IDGroup == CommonContact.ADMIN)// nếu là admin thì có toàn quyền
            {
                return true;
            }
            if (privi.Contains(this.ID) && section.IDGroup == CommonContact.MEMBER)// là nhân viên thì có quyền, duyêt đơn hàng,xem đơn hàng, trả lời bình luận, xem sản phẩm, xem chi tiết, xem bình luận
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/shared/401.cshtml"
            };
        }
        private List<string> GetcredentialByloggedInUser()
        {
            return (List<string>)HttpContext.Current.Session[CommonContact.SESTION_CREDENTIAL];// lấy ra danh sách quyền

        }
    }
}