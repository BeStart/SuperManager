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
    public class NoticeController : BaseManagerListController
    {
        [RoleMenuFilter]
        public ActionResult List(string searchKey = "", int noticeType = -1, int pageIndex = 1)
        {
            searchKey = StringHelper.FilterSpecChar(searchKey);
            List<DBNoticeFullModel> modelList = DALFactory.Notice.Page(searchKey, noticeType, pageIndex, this.PageSize, ref this.totalCount, ref this.pageCount);

            this.InitViewData(searchKey, pageIndex, Url.Action("List", new { PageIndex = -999, SearchKey = searchKey, NoticeType = noticeType }));

            ViewBag.NoticeTypeList = DALFactory.NoticeType.List();
            ViewData["NoticeType"] = noticeType;
            return View(modelList);
        }

        [RoleActionFilter]
        public ActionResult Add()
        {
            return this.Edit();
        }

        [RoleActionFilter]
        public ActionResult Edit(int identityID = 0)
        {
            ViewBag.NoticeTypeList = DALFactory.NoticeType.List();
            return View("Edit", identityID > 0 ? DALFactory.Notice.Select(identityID) : null);
        }

        [RoleActionFilter]
        public ActionResult DeleteOperater(int identityID)
        {
            return this.Delete(() =>
            {
                return DALFactory.Notice.Delete(identityID);
            }, Url.Action("List"));
        }

        [HttpPost]
        public ActionResult UploadOperater(string type, string fromType, string CKEditorFuncNum = null)
        {
            // 上传文件
            return this.UploadOperater(() =>
            {
                return DataHelper.AuthAction(this.viewUserModel.RoleID.ToString(), "Notice", "Upload");
            }, type, fromType, CKEditorFuncNum, "Notices", (string attachmentType, string attachmentName, int attachmentSize, string attachmentPath) =>
            {
                return DALFactory.Attachment.Operater(new MODEL.DBAttachmentModel()
                {
                    AttachmentType = attachmentType,
                    AttachmentName = attachmentName,
                    AttachmentSize = attachmentSize,
                    AttachmentPath = attachmentPath
                });
            });
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [RoleActionFilter]
        public ActionResult AddOperater(DBNoticeModel model)
        {
            return this.AddOrEditOperater(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [RoleActionFilter]
        public ActionResult EditOperater(DBNoticeModel model)
        {
            return this.AddOrEditOperater(model);
        }

        [HttpPost]
        [RoleActionFilter]
        public ActionResult MoreOperater()
        {
            return this.More((string identityIDList) =>
            {
                return DALFactory.Notice.DeleteMore(identityIDList);
            }, null, Url.Action("List"));
        }

        [NonAction]
        private ActionResult AddOrEditOperater(DBNoticeModel model)
        {
            return this.OperaterConfirm(() =>
            {
                return FilterFactory.Notice.Operater(model);
            }, null, () =>
            {
                return DALFactory.Notice.Operater(model);
            }, Url.Action("List"), model.IdentityID == 0 ? Url.Action("Add") : "", Url.Action("Edit", new { identityID = model.IdentityID }));
        }
    }
}