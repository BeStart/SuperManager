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
    public class TopicTypeController : BaseManagerListController
    {
        [RoleMenuFilter]
        public ActionResult List(string searchKey = "")
        {
            searchKey = StringHelper.FilterSpecChar(searchKey);
            // 设置树形菜单数据
            List<ViewTreeTopicTypeModel> dataList = TreeHelper.ToMenuList<ViewTreeTopicTypeModel>(DALFactory.TopicType.All(searchKey));
            this.InitViewData(searchKey, 0, "");
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
            ViewBag.ParentID = parentID;
            ViewBag.TopicTypeList = DALFactory.TopicType.List(0);
            return View("Edit", identityID > 0 ? DALFactory.TopicType.Select(identityID) : null);
        }

        [RoleActionFilter]
        public ActionResult DeleteOperater(int identityID)
        {
            return this.Delete(() =>
            {
                return DALFactory.TopicType.Delete(identityID);
            }, Url.Action("List"));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleActionFilter]
        public ActionResult AddOperater(DBTopicTypeModel model)
        {
            return this.AddOrEditOperater(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleActionFilter]
        public ActionResult EditOperater(DBTopicTypeModel model)
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

        [NonAction]
        private ActionResult AddOrEditOperater(DBTopicTypeModel model)
        {
            return this.OperaterConfirm(() =>
            {
                return FilterFactory.TopicType.Operater(model);
            }, null, () =>
            {
                return DALFactory.TopicType.Operater(model);
            }, Url.Action("List"), model.IdentityID == 0 ? Url.Action("Add") : "", Url.Action("Edit", new { identityID = model.IdentityID, parentID = model.ParentID }));
        }
    }
}