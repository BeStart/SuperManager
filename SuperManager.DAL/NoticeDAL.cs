﻿using Helper.Core.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperManager.MODEL;

namespace SuperManager.DAL
{
    public class NoticeDAL
    {
        private const string TABLE_NAME = "T_Notice";

        public bool Operater(DBNoticeModel model)
        {
            if (model.IdentityID == 0)
            {
                return DataBaseHelper.Insert<DBNoticeModel>(model, p => new { p.IdentityID, p.NoticeDateTime }, TABLE_NAME);
            }
            else
            {
                return DataBaseHelper.Update<DBNoticeModel>(model, p => p.IdentityID == p.IdentityID, p => new { p.IdentityID, p.NoticeDateTime }, TABLE_NAME);
            }
        }
        public bool Delete(int identityID)
        {
            return DataBaseHelper.Delete<DBNoticeModel>(new { IdentityID = identityID }, p => p.IdentityID == p.IdentityID, TABLE_NAME);
        }
        public bool DeleteMore(string identityIDList)
        {
            List<int> dataList = StringHelper.ToList<int>(identityIDList, ",");
            return DataBaseHelper.Delete<DBNoticeModel>(null, p => dataList.Contains(p.IdentityID), TABLE_NAME);
        }
        public DBNoticeModel Select(int identityID)
        {
            return DataBaseHelper.Single<DBNoticeModel>(new { IdentityID = identityID }, p => new { p.IdentityID, p.NoticeType, p.NoticeTitle, p.NoticeContent }, p => p.IdentityID == p.IdentityID, TABLE_NAME);
        }
        public List<DBNoticeFullModel> Page(string searchKey, int noticeType, int pageIndex, int pageSize, ref int totalCount, ref int pageCount)
        {
            searchKey = StringHelper.FilterSpecChar(searchKey);

            StringBuilder stringBuilder = new StringBuilder();

            if (!string.IsNullOrEmpty(searchKey))
            {
                stringBuilder.Append(string.Format(" NoticeTitle like '%{0}%' ", searchKey));
                stringBuilder.Append(" and ");
            }
            if (noticeType > 0)
            {
                stringBuilder.Append(" NoticeType = ");
                stringBuilder.Append(noticeType);
                stringBuilder.Append(" and ");
            }

            string whereSql = stringBuilder.ToString().TrimEnd().TrimEnd(new char[] { 'a', 'n', 'd' });

            Dictionary<string, object> parameterList = new Dictionary<string, object>();
            parameterList.Add("@FieldSql", "IdentityID, NoticeType, NoticeTitle, NoticeDateTime, (select TypeName from T_NoticeType with(nolock) where T_NoticeType.IdentityID=T.NoticeType) as NoticeTypeName");
            parameterList.Add("@Field", "IdentityID, NoticeType, NoticeTitle, NoticeDateTime");
            parameterList.Add("@TableName", "T_Notice");
            parameterList.Add("@PrimaryKey", "IdentityID");
            parameterList.Add("@PageIndex", pageIndex);
            parameterList.Add("@PageSize", pageSize);
            parameterList.Add("@WhereSql", whereSql);
            parameterList.Add("@OrderSql", "IdentityID asc");

            return DataBaseHelper.ToEntityList<DBNoticeFullModel>("", parameterList, ref pageCount, ref totalCount, null, "PageCount", "TotalCount");
        }
    }
}
