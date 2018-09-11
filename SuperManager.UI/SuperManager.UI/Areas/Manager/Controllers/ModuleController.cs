using Helper.Core.Library;
using SuperManager.DAL;
using SuperManager.ENUM;
using SuperManager.FILTER;
using SuperManager.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperManager.UI.Areas.Manager.Controllers
{
    public class ModuleController : BaseManagerListController
    {
        [RoleMenuFilter]
        public ActionResult List(string searchKey = "")
        {
            searchKey = StringHelper.FilterSpecChar(searchKey);

            this.InitViewData(searchKey, 0, "");

            return View(DALFactory.Module.All(searchKey));
        }

        [RoleActionFilter]
        public ActionResult Add()
        {
            return this.Edit();
        }

        [RoleActionFilter]
        public ActionResult Edit(int identityID = 0)
        {
            ViewBag.ActionTypeList = DALFactory.ActionType.List();
            return View("Edit", identityID > 0 ? DALFactory.Module.Select(identityID) : null);
        }

        [RoleActionFilter]
        public ActionResult DeleteOperater(int identityID)
        {
            return this.Delete(() =>
            {
                return DALFactory.Module.Delete(identityID);
            }, Url.Action("List"));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleActionFilter]
        public ActionResult AddOperater(DBModuleModel model)
        {
            return this.AddOrEditOperater(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleActionFilter]
        public ActionResult EditOperater(DBModuleModel model)
        {
            return this.AddOrEditOperater(model);
        }

        [HttpPost]
        [RoleActionFilter]
        public ActionResult MoreOperater()
        {
            return this.More((string identityIDList) =>
            {
                return DALFactory.Module.DeleteMore(identityIDList);
            }, null, Url.Action("List"));
        }

        [NonAction]
        private ActionResult AddOrEditOperater(DBModuleModel model)
        {
            string actionList = this.Request["actionList"].ToString();
            model.ActionList = actionList;

            return this.OperaterConfirm(() =>
            {
                return FilterFactory.Module.Operater(model);
            }, () =>
            {
                return DALFactory.Module.Exists(model.ModuleCode, model.IdentityID);
            }, () =>
            {
                return DALFactory.Module.Operater(model);
            }, Url.Action("List"), model.IdentityID == 0 ? Url.Action("Add") : "", Url.Action("Edit", new { identityID = model.IdentityID }), "模块编号已存在！");
        }
    }
}