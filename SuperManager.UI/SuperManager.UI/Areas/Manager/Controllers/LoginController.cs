﻿using Helper.Core.Library;
using SuperManager.DAL;
using SuperManager.ENUM;
using SuperManager.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperManager.UI.Areas.Manager.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.UserCode = CookieHelper.GetCookie(ConfigHelper.TokenUserCode);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string userCode, string userPassword, string memberUserCode)
        {
            userPassword = EncryptHelper.MD5(userPassword, ConfigHelper.ConfuseKey);
            // 如果记住用户名
            if (!string.IsNullOrEmpty(this.Request["memberUserCode"]))
            {
                CookieHelper.SetCookie(ConfigHelper.TokenUserCode, userCode);
            }
            else
            {
                // 否则清除记录的数据
                CookieHelper.DeleteCookie(ConfigHelper.TokenUserCode);
            }

            DBUserFullModel model = DALFactory.User.Select(userCode, userPassword);
            // 记录登录日志
            DALFactory.UserLog.Operater(new DBUserLogModel() { UserCode = userCode, LoginIP = IPHelper.GetClientIP(), LoginStatus = (model == null ? LoginStatusTypeEnum.FAILED : LoginStatusTypeEnum.SUCCESS) });

            if (model == null)
            {
                return this.Content(string.Format("<script>alert('{0}');location.href='{1}'</script>", "用户名或者密码错误！", Url.Action("Index")));
            }
            else
            {
                // 设置登录信息
                ViewUserModel tokenModel = new ViewUserModel() { UserCode = model.UserCode, NickName = model.NickName, RoleID = model.RoleID, RoleName = model.RoleName };
                // 如果是 Cookie 方式存储
                if (ConfigHelper.TokenType == TokenTypeEnum.Cookie)
                {
                    CookieHelper.SetCookieT<ViewUserModel>(ConfigHelper.TokenName, tokenModel);
                }
                // 如果是 Session 方式存储
                else if (ConfigHelper.TokenType == TokenTypeEnum.Session)
                {
                    this.Session[ConfigHelper.TokenName] = tokenModel;
                }
                // 设置角色权限
                DataHelper.InitRoleMenuAndActionData(tokenModel.RoleID.ToString(), DALFactory.Menu.List(model.MenuList, MenuStatusTypeEnum.SHOW), model.ActionList, DALFactory.Module.All(""));
                // 跳转到管理后台首页
                return this.Redirect(Url.Action("Index", "Home"));
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            // 删除登录信息
            if (ConfigHelper.TokenType == TokenTypeEnum.Cookie)
            {
                CookieHelper.DeleteCookie(ConfigHelper.TokenName);
            }
            else
            {
                this.Session.Remove(ConfigHelper.TokenName);
            }
            return this.Redirect("Index");
        }
    }
}