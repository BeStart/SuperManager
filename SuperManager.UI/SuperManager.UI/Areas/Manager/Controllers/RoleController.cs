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
    public class RoleController : BaseManagerListController
    {
        [RoleMenuFilter]
        public ActionResult List(string searchKey = "", int pageIndex = 1)
        {
            searchKey = StringHelper.FilterSpecChar(searchKey);
            List<DBRoleModel> modelList = DALFactory.Role.Page(searchKey, pageIndex, ConfigHelper.ManagerPageSize, ref this.totalCount, ref this.pageCount);

            this.InitViewData(searchKey, pageIndex, Url.Action("List", new { PageIndex = -999, SearchKey = searchKey }));

            return View(modelList);
        }

        [RoleActionFilter]
        public ActionResult Add()
        {
            return this.Edit(0);
        }

        [RoleActionFilter]
        public ActionResult Edit(int identityID = 0)
        {
            List<ViewTreeMenuModel> dataList = TreeHelper.ToMenuList<ViewTreeMenuModel>(DALFactory.Menu.All(""));
            ViewBag.MenuList = dataList;
            ViewBag.ModuleList = DALFactory.Module.List();
            ViewBag.ActionTypeList = DALFactory.ActionType.List();
            return View("Edit", identityID > 0 ? DALFactory.Role.Select(identityID) : null);
        }

        [RoleActionFilter]
        public ActionResult DeleteOperater(int identityID)
        {
            return this.Delete(() =>
            {
                return DALFactory.Role.Delete(identityID);
            }, Url.Action("List"));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleActionFilter]
        public ActionResult AddOperater(DBRoleModel model)
        {
            return this.AddOrEditOperater(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleActionFilter]
        public ActionResult EditOperater(DBRoleModel model)
        {
            return this.AddOrEditOperater(model);
        }

        [HttpPost]
        [RoleActionFilter]
        public ActionResult MoreOperater()
        {
            return this.More((string identityIDList) =>
            {
                return DALFactory.Role.DeleteMore(identityIDList);
            }, null, Url.Action("List"));
        }

        [NonAction]
        private ActionResult AddOrEditOperater(DBRoleModel model)
        {
            string menuList = this.Request.Form["identityIDList"];
            string actionList = this.Request.Form["menuActionList"];

            return this.OperaterConfirm(() =>
            {
                return FilterFactory.Role.Operater(model.RoleName, menuList, actionList);
            }, () =>
            {
                return DALFactory.Role.Exists(model.RoleName, model.IdentityID);
            }, () =>
            {
                bool result = DALFactory.Role.Operater(model, menuList, actionList);
                // 如果保存数据成功，要替换掉内存中的角色配置数据
                if (result)
                {
                    DataHelper.InitRoleMenuAndActionData(model.IdentityID.ToString(), DALFactory.Menu.List(menuList), actionList, DALFactory.Module.List());
                }
                return result;
            }, Url.Action("List"), model.IdentityID == 0 ? Url.Action("Add") : "", Url.Action("Edit", new { identityID = model.IdentityID }), "角色名称已存在！");
        }
    }
}