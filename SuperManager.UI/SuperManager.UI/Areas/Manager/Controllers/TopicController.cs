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
    public class TopicController : BaseManagerListController
    {
        [RoleMenuFilter]
        public ActionResult List(string searchKey = "", int topicType = -1, int topicPositionType = -1, int topicStatus = -1, int pageIndex = 1)
        {
            searchKey = StringHelper.FilterSpecChar(searchKey);
            List<DBTopicFullModel> modelList = DALFactory.Topic.Page(searchKey, topicType, topicPositionType, topicStatus, pageIndex, this.PageSize, ref this.totalCount, ref this.pageCount);

            this.InitViewData(searchKey, pageIndex, Url.Action("List", new { PageIndex = -999, SearchKey = searchKey, TopicType = topicType, TopicPosition = topicPositionType, TopicStatus = topicStatus }));

            ViewBag.TopicTypeList = TreeHelper.ToMenuList<ViewTreeTopicTypeModel>(DALFactory.TopicType.All(""));
            ViewBag.PositionTypeList = DALFactory.TopicPositionType.List();
            ViewBag.StatusTypeList = new List<DBKeyValueModel>()
            {
                new DBKeyValueModel(){ Key = OperaterTypeEnum.DEFAULT, Value = "未审核" },
                new DBKeyValueModel(){ Key = OperaterTypeEnum.DELETE, Value = "已删除" },
                new DBKeyValueModel(){ Key = OperaterTypeEnum.CHECKED, Value = "已审核" },
            };
            ViewData["TopicType"] = topicType;
            ViewData["TopicPositionType"] = topicPositionType;
            ViewData["TopicStatus"] = topicStatus;
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
            ViewBag.TopicTypeList = TreeHelper.ToMenuList<ViewTreeTopicTypeModel>(DALFactory.TopicType.All(""));
            ViewBag.PositionTypeList = DALFactory.TopicPositionType.List(); ;
            return View("Edit", identityID > 0 ? DALFactory.Topic.Select(identityID) : null);
        }

        [RoleActionFilter]
        public ActionResult DeleteOperater(int identityID)
        {
            return this.Delete(() =>
            {
                return DALFactory.Topic.Delete(identityID);
            }, Url.Action("List"));
        }

        [HttpPost]
        public ActionResult UploadOperater(string type, string fromType, string CKEditorFuncNum = null)
        {
            // 上传文件
            return this.UploadOperater(() =>
            {
                return DataHelper.AuthAction(this.viewUserModel.RoleID.ToString(), "Topic", "Upload");
            }, type, fromType, CKEditorFuncNum);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [RoleActionFilter]
        public ActionResult AddOperater(DBTopicModel model)
        {
            return this.AddOrEditOperater(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [RoleActionFilter]
        public ActionResult EditOperater(DBTopicModel model)
        {
            return this.AddOrEditOperater(model);
        }

        [HttpPost]
        [RoleActionFilter]
        public ActionResult MoreOperater()
        {
            return this.More((string identityIDList) =>
            {
                return DALFactory.Topic.DeleteMore(identityIDList);
            }, (string identityIDList, string operaterType) =>
            {
                // 审核
                if(operaterType == OperaterTypeEnum.CHECKED)
                {
                    return DALFactory.Topic.StatusMore(identityIDList, int.Parse(operaterType));
                }
                return false;
            }, Url.Action("List"));
        }

        [NonAction]
        private ActionResult AddOrEditOperater(DBTopicModel model)
        {
            model.PositionTypeList = StringHelper.PadChar(model.PositionTypeList, ",");
            return this.OperaterConfirm(() =>
            {
                return FilterFactory.Topic.Operater(model);
            }, null, () =>
            {
                return DALFactory.Topic.Operater(model);
            }, Url.Action("List"), model.IdentityID == 0 ? Url.Action("Add") : "", Url.Action("Edit", new { identityID = model.IdentityID }));
        }
    }
}