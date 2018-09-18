using Helper.Core.Library;
using SuperManager.DAL;
using SuperManager.ENUM;
using SuperManager.FILTER;
using SuperManager.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SuperManager.UI.Areas.Manager.Controllers
{
    public class MenuController : BaseManagerListController
    {
        [RoleMenuFilter]
        public ActionResult List(string searchKey = "")
        {
            searchKey = StringHelper.FilterSpecChar(searchKey);

            List<ViewTreeMenuModel> dataList = TreeHelper.ToMenuList<ViewTreeMenuModel>(DALFactory.Menu.All(searchKey));
            this.InitViewData(searchKey, 0, "");

            ViewBag.ActionTypeList = DALFactory.ActionType.List();
            ViewBag.ModuleList = DALFactory.Module.List();
            return View(dataList);
        }

        [RoleActionFilter]
        public ActionResult Add(int parentID = 0)
        {
            return this.Edit(0, parentID);
        }

        [RoleActionFilter]
        public ActionResult Edit(int identityID = 0, int parentID = 0)
        {
            DBMenuModel model = null;
            if (identityID > 0)
            {
                model = DALFactory.Menu.Select(identityID);
            }

            List<DBModuleModel> moduleList = DALFactory.Module.List();

            ViewBag.ParentID = parentID;
            ViewBag.TreeMenuList = TreeHelper.ToMenuList<ViewTreeMenuModel>(DALFactory.Menu.All(""));
            ViewBag.ModuleList = moduleList;

            if (model != null && !string.IsNullOrEmpty(model.BelongModule) && moduleList.Count > 0)
            {
                DBModuleModel moduleModel = moduleList.Where(p => p.ModuleCode == model.BelongModule).FirstOrDefault();
                if (moduleModel != null)
                {
                    ViewBag.ActionTypeList = DALFactory.ActionType.List(moduleModel.ActionList);
                }
            }
            ViewBag.FlowStepList = DALFactory.FlowStep.List();
            return View("Edit", model);
        }

        [RoleActionFilter]
        public ActionResult DeleteOperater(int identityID)
        {
            return this.Delete(() =>
            {
                return DALFactory.Menu.Delete(identityID);
            }, Url.Action("List"));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleActionFilter]
        public ActionResult AddOperater(DBMenuModel model)
        {
            return this.AddOrEditOperater(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleActionFilter]
        public ActionResult EditOperater(DBMenuModel model)
        {
            return this.AddOrEditOperater(model);
        }

        [HttpPost]
        [RoleActionFilter]
        public ActionResult MoreOperater()
        {
            return this.More((string identityIDList) =>
            {
                return DALFactory.Menu.DeleteMore(identityIDList);
            }, null, Url.Action("List"));
        }

        #region 获取页面菜单 JSON 数据
        public ActionResult GetMenuJsonText()
        {
            List<DBModuleModel> moduleList = DALFactory.Module.List();
            if (moduleList == null) return this.Content("{}");

            List<DBActionTypeModel> actionList = DALFactory.ActionType.List();

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("{");
            int menuIndex = 0;
            foreach (DBModuleModel moduleModel in moduleList)
            {
                stringBuilder.Append("\\\"");
                stringBuilder.Append(moduleModel.ModuleCode);
                stringBuilder.Append("\\\":{");
                if (!string.IsNullOrEmpty(moduleModel.ActionList))
                {
                    int actionIndex = 0;
                    List<string> actionDataList = StringHelper.ToList<string>(moduleModel.ActionList, ",", true);
                    foreach (string actionData in actionDataList)
                    {
                        DBActionTypeModel actionModel = actionList.Where(p => p.TypeCode == actionData).FirstOrDefault();
                        if (actionModel != null)
                        {
                            stringBuilder.Append("\\\"");
                            stringBuilder.Append(actionData);
                            stringBuilder.Append("\\\":\\\"");
                            stringBuilder.Append(actionModel.TypeName);
                            stringBuilder.Append("\\\"");
                            if (actionIndex < actionDataList.Count - 1)
                            {
                                stringBuilder.Append(",");
                            }
                        }
                        actionIndex++;
                    }
                }
                stringBuilder.Append("}");
                if (menuIndex < moduleList.Count - 1)
                {
                    stringBuilder.Append(",");
                }
                menuIndex++;
            }
            stringBuilder.Append("}");

            return this.Content(stringBuilder.ToString());
        }
        #endregion

        [NonAction]
        private ActionResult AddOrEditOperater(DBMenuModel model)
        {
            string menuActionList = this.Request.Form["menuActionList"];
            model.ActionList = menuActionList;
            if (model.ParentID != 0) model.MenuIcon = "";

            return this.OperaterConfirm(() =>
            {
                return FilterFactory.Menu.Operater(model, menuActionList);
            }, null, () =>
            {
                return DALFactory.Menu.Operater(model);
            }, Url.Action("List"), model.IdentityID == 0 ? Url.Action("Add") : "", Url.Action("Edit", new { identityID = model.IdentityID }));
        }
    }
}