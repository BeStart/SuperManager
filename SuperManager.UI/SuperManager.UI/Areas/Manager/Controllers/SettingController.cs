using SuperManager.ENUM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperManager.UI.Areas.Manager.Controllers
{
    public class SettingController : BaseManagerController
    {
        [RoleActionFilter]
        public ActionResult Set()
        {
            return View();
        }

        [RoleActionFilter]
        public ActionResult Bak()
        {
            return this.Operater(null, null, () =>
            {
                return TaskHelper.ExecuteBakDataBase();
            }, Url.Action("Set"), Url.Action("Set"), null, "备份成功！", "备份失败！");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleActionFilter]
        public ActionResult SetOperater(string bakCron, string bakPath, string version, string managerTitle, string managerPageSize, string logOpenStatus, string authOpenStatus, string attachOpenStatus)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>()
            {
                { SettingTypeEnum.BakCron, bakCron },
                { SettingTypeEnum.BakPath, bakPath },
                { SettingTypeEnum.Version, version },
                { SettingTypeEnum.ManagerTitle, managerTitle },
                { SettingTypeEnum.ManagerPageSize, managerPageSize }
            };
            if(!string.IsNullOrEmpty(logOpenStatus))
            {
                dict.Add(SettingTypeEnum.LogOpenStatus, "1");
            }
            if(!string.IsNullOrEmpty(authOpenStatus))
            {
                dict.Add(SettingTypeEnum.AuthOpenStatus, "1");
            }
            if (!string.IsNullOrEmpty(attachOpenStatus))
            {
                dict.Add(SettingTypeEnum.AttachOpenStatus, "1");
            }
            return this.Operater(null, null, () =>
            {
                return SettingHelper.Set(dict);
            }, Url.Action("Set"), Url.Action("Set"));
        }
    }
}