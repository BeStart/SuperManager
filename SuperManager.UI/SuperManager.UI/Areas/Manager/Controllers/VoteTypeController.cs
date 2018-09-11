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
    public class VoteTypeController : BaseManagerListController
    {
        [RoleMenuFilter]
        public ActionResult List(string searchKey = "")
        {
            searchKey = StringHelper.FilterSpecChar(searchKey);

            this.InitViewData(searchKey, 0, "");
            
            return View(DALFactory.VoteType.All(searchKey));
        }

        [RoleActionFilter]
        public ActionResult Add()
        {
            return this.Edit();
        }

        [RoleActionFilter]
        public ActionResult Edit(int identityID = 0)
        {
            return View("Edit", identityID > 0 ? DALFactory.VoteType.Select(identityID) : null);
        }

        [RoleActionFilter]
        public ActionResult DeleteOperater(int identityID)
        {
            return this.Delete(() =>
            {
                return DALFactory.VoteType.Delete(identityID);
            }, Url.Action("List"));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleActionFilter]
        public ActionResult AddOperater(DBVoteTypeModel model)
        {
            return this.AddOrEditOperater(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleActionFilter]
        public ActionResult EditOperater(DBVoteTypeModel model)
        {
            return this.AddOrEditOperater(model);
        }

        [HttpPost]
        [RoleActionFilter]
        public ActionResult MoreOperater()
        {
            return this.More((string identityIDList) =>
            {
                return DALFactory.VoteType.DeleteMore(identityIDList);
            }, null, Url.Action("List"));
        }

        [NonAction]
        private ActionResult AddOrEditOperater(DBVoteTypeModel model)
        {
            return this.OperaterConfirm(() =>
            {
                return FilterFactory.VoteType.Operater(model);
            }, () =>
            {
                return DALFactory.VoteType.Exists(model.TypeName, model.IdentityID);
            }, () =>
            {
                return DALFactory.VoteType.Operater(model);
            }, Url.Action("List"), model.IdentityID == 0 ? Url.Action("Add") : "", Url.Action("Edit", new { identityID = model.IdentityID }), "投票类别已存在！");
        }
    }
}