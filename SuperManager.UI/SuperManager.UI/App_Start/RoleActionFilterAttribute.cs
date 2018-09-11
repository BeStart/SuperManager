using Helper.Core.Library;
using SuperManager.ENUM;
using SuperManager.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperManager.UI
{
    public class RoleActionFilterAttribute : RoleMenuFilterAttribute
    {
        protected override void Valid(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            ViewUserModel viewUserModel = CookieHelper.GetCookieT<ViewUserModel>(ConfigHelper.TokenName);
            if (viewUserModel == null)
            {
                this.RedirectToLoginUrl(filterContext);
            }
            // 如果以后缀 operater 出现
            if (actionName.EndsWith("operater"))
            {
                actionName = StringHelper.TrimEnd(actionName, "operater");
            }
            // 如果是批量操作
            if (actionName == "more")
            {
                // 获取操作类别
                string operaterType = filterContext.HttpContext.Request.Form["operaterType"];
                // 如果不包括 operaterType，则跳转
                if (string.IsNullOrEmpty(operaterType))
                {
                    this.RedirectToLoginUrl(filterContext);
                }
                if (operaterType == OperaterTypeEnum.DELETE)
                {
                    // 删除权限
                    actionName = ActionTypeEnum.Delete;
                }
                else if (operaterType == OperaterTypeEnum.CHECKED)
                {
                    actionName = ActionTypeEnum.Check;
                }
                // 取小写
                actionName = actionName.ToLower();
            }
            // 如果未通过授权验证，则跳转到登录页面
            bool authStatus = DataHelper.AuthAction(viewUserModel.RoleID.ToString(), controllerName, actionName);
            if (!authStatus)
            {
                this.RedirectToLoginUrl(filterContext);
            }
        }
    }
}