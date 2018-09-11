using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SuperManager.MODEL;

namespace SuperManager.UI.Areas.Manager.Controllers
{
    public class BaseManagerListController : BaseManagerController
    {
        #region 列表变量
        protected int totalCount = 0;
        protected int pageCount = 0;
        #endregion

        protected virtual void InitViewData(string searchKey, int pageIndex, string pageUrl)
        {
            ViewData.Add("SearchKey", searchKey);
            ViewData.Add("PageModel", new ViewPageModel()
            {
                PageIndex = pageIndex,
                PageCount = this.pageCount,
                TotalCount = this.totalCount,
                PageUrl = pageUrl
            });
        }
        /// <summary>
        /// 过滤关键字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        protected virtual string FilterSpecChar(string str)
        {
            return Helper.Core.Library.StringHelper.FilterSpecChar(str);
        }
    }
}