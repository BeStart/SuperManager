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
    public class IndexMapperController : BaseManagerListController
    {
        [RoleMenuFilter]
        public ActionResult List(int indexType = -1, int pageIndex = 1)
        {
            List<DBIndexMapperModel> modelList = DALFactory.IndexMapper.Page(indexType, pageIndex, ConfigHelper.ManagerPageSize, ref this.totalCount, ref this.pageCount);

            this.InitViewData("", pageIndex, Url.Action("List", new { PageIndex = -999, IndexType = indexType }));

            ViewBag.IndexTypeList = ConstHelper.GetIndexMapperList();
            ViewData["IndexType"] = indexType;

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
            DBIndexMapperModel model = identityID > 0 ? DALFactory.IndexMapper.Select(identityID) : null;
            ViewBag.IndexTypeList = ConstHelper.GetIndexMapperList();
            if(model != null)
            {
                ViewBag.IndexIDList = ConstHelper.GetIndexMapperKeyValueList(model.IndexType);
            }
            else
            {
                ViewBag.IndexIDList = ConstHelper.GetIndexMapperKeyValueList(IndexMapperTypeEnum.Topic);
            }
            return View("Edit", model);
        }

        [RoleActionFilter]
        public ActionResult DeleteOperater(int identityID)
        {
            return this.Delete(() =>
            {
                return DALFactory.IndexMapper.Delete(identityID);
            }, Url.Action("List"));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleActionFilter]
        public ActionResult AddOperater(DBIndexMapperModel model)
        {
            return this.AddOrEditOperater(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleActionFilter]
        public ActionResult EditOperater(DBIndexMapperModel model)
        {
            return this.AddOrEditOperater(model);
        }

        [HttpPost]
        [RoleActionFilter]
        public ActionResult MoreOperater()
        {
            return this.More((string identityIDList) =>
            {
                return DALFactory.IndexMapper.DeleteMore(identityIDList);
            }, null, Url.Action("List"));
        }

        public ActionResult GetIndexJsonText()
        {
            Dictionary<int, List<DBKeyValueModel>> KeyValueDict = ConstHelper.GetIndexMapperKeyValueDict();

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("{");

            if (KeyValueDict != null && KeyValueDict.Count > 0)
            {
                int typeIndex = 0;
                foreach (KeyValuePair<int, List<DBKeyValueModel>> keyValueItem in KeyValueDict)
                {
                    stringBuilder.Append("\\\"");
                    stringBuilder.Append(keyValueItem.Key);
                    stringBuilder.Append("\\\":[");

                    List<DBKeyValueModel> keyValueList = keyValueItem.Value;
                    int keyValueCount = keyValueList.Count;
                    for (int index = 0; index < keyValueCount; index++)
                    {
                        stringBuilder.Append("\\\"key\\\":");
                        stringBuilder.Append(keyValueList[index].Key);
                        stringBuilder.Append(",\\\"value\\\":\\\"");
                        stringBuilder.Append(keyValueList[index].Value);
                        stringBuilder.Append("\\\"}");
                        if (index < keyValueCount - 1)
                        {
                            stringBuilder.Append(",");
                        }
                    }

                    stringBuilder.Append("]");
                    if (typeIndex < KeyValueDict.Count - 1)
                    {
                        stringBuilder.Append(",");
                    }
                    typeIndex++;
                }
            }

            stringBuilder.Append("}");
            return this.Content(stringBuilder.ToString());
        }

        [NonAction]
        private ActionResult AddOrEditOperater(DBIndexMapperModel model)
        {
            return this.OperaterConfirm(() =>
            {
                return FilterFactory.IndexMapper.Operater(model);
            }, null, () =>
            {
                return DALFactory.IndexMapper.Operater(model);
            }, Url.Action("List"), model.IdentityID == 0 ? Url.Action("Add") : "", Url.Action("Edit", new { identityID = model.IdentityID }));
        }
    }
}