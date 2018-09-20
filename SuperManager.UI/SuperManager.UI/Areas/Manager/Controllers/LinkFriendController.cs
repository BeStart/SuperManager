using Helper.Core.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SuperManager.DAL;
using SuperManager.FILTER;
using SuperManager.MODEL;

namespace SuperManager.UI.Areas.Manager.Controllers
{
    public class LinkFriendController : BaseManagerListController
    {
        [RoleMenuFilter]
        public ActionResult List(string searchKey = "", int linkFriendType = -1, int pageIndex = 1)
        {
            searchKey = this.FilterSpecChar(searchKey);

            List<DBLinkFriendFullModel> modelList = DALFactory.LinkFriend.Page(searchKey, linkFriendType, pageIndex, this.PageSize, ref this.totalCount, ref this.pageCount);

            this.InitViewData(searchKey, pageIndex, Url.Action("List", new { PageIndex = -999, SearchKey = searchKey, LinkFriendType = linkFriendType }));
            ViewData["LinkFriendType"] = linkFriendType;
            ViewBag.LinkFriendTypeList = DALFactory.LinkFriendType.List();
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
            ViewBag.LinkFriendTypeList = DALFactory.LinkFriendType.List();
            return View("Edit", DALFactory.LinkFriend.Select(identityID));
        }

        [HttpPost]
        public ActionResult UploadOperater(string type, string fromType, string CKEditorFuncNum = null)
        {
            // 上传文件
            return this.UploadOperater(() =>
            {
                return DataHelper.AuthAction(this.viewUserModel.RoleID.ToString(), "LinkFriend", "Upload");
            }, type, fromType, CKEditorFuncNum, "LinkFriends");
        }

        [RoleActionFilter]
        public ActionResult DeleteOperater(int identityID)
        {
            return this.Delete(() =>
            {
                return DALFactory.LinkFriend.Delete(identityID);
            }, Url.Action("List"));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleActionFilter]
        public ActionResult AddOperater(DBLinkFriendModel model)
        {
            return this.AddOrEditOperater(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleActionFilter]
        public ActionResult EditOperater(DBLinkFriendModel model)
        {
            return this.AddOrEditOperater(model);
        }

        [HttpPost]
        [RoleActionFilter]
        public ActionResult MoreOperater()
        {
            return this.More((string identityIDList) =>
            {
                return DALFactory.LinkFriend.DeleteMore(identityIDList);
            }, null, Url.Action("List"));
        }

        [NonAction]
        private ActionResult AddOrEditOperater(DBLinkFriendModel model)
        {
            return this.OperaterConfirm(() =>
            {
                return FilterFactory.LinkFriend.Operater(model);
            }, null, () =>
            {
                return DALFactory.LinkFriend.Operater(model);
            }, Url.Action("List"), model.IdentityID == 0 ? Url.Action("Add") : "", Url.Action("Edit", new { identityID = model.IdentityID }));
        }
    }
}