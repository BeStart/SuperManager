﻿using Helper.Core.Library;
using SuperManager.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.DAL
{
    public class NoticeTypeDAL
    {
        private const string TABLE_NAME = "T_NoticeType";

        public bool Operater(DBNoticeTypeModel model)
        {
            if (model.IdentityID == 0)
            {
                return DataBaseHelper.Insert<DBNoticeTypeModel>(model, p => p.IdentityID, TABLE_NAME);
            }
            else
            {
                return DataBaseHelper.Update<DBNoticeTypeModel>(model, p => p.IdentityID == p.IdentityID, p => p.IdentityID, TABLE_NAME);
            }
        }
        public bool Exists(string typeName, int identityID)
        {
            return DataBaseHelper.Exists<DBNoticeTypeModel>(new { TypeName = typeName }, p => p.IdentityID, p => p.TypeName == p.TypeName, identityID, TABLE_NAME);
        }
        public bool Delete(int identityID)
        {
            return DataBaseHelper.Delete<DBNoticeTypeModel>(new { IdentityID = identityID }, p => p.IdentityID == p.IdentityID, TABLE_NAME);
        }
        public bool DeleteMore(string identityIDList)
        {
            List<int> dataList = StringHelper.ToList<int>(identityIDList, ",");
            return DataBaseHelper.Delete<DBNoticeTypeModel>(null, p => dataList.Contains(p.IdentityID), TABLE_NAME);
        }
        public DBNoticeTypeModel Select(int identityID)
        {
            return DataBaseHelper.Single<DBNoticeTypeModel>(new { IdentityID = identityID }, p => new { p.IdentityID, p.TypeName, p.TypeSort }, p => p.IdentityID == p.IdentityID, TABLE_NAME);
        }
        public List<DBNoticeTypeModel> List()
        {
            return DataBaseHelper.More<DBNoticeTypeModel>(null, p => new { p.IdentityID, p.TypeName }, null, p => p.TypeSort, true, TABLE_NAME);
        }

        public List<DBNoticeTypeModel> Page(string searchKey, int pageIndex, int pageSize, ref int totalCount, ref int pageCount)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(searchKey))
            {
                stringBuilder.Append(" TypeName like '%");
                stringBuilder.Append(searchKey);
                stringBuilder.Append("%' ");
            }
            string whereSql = stringBuilder.ToString().TrimEnd().TrimEnd(new char[] { 'a', 'n', 'd' });

            Dictionary<string, object> parameterList = new Dictionary<string, object>();
            parameterList.Add("@FieldSql", "IdentityID, TypeName, TypeSort");
            parameterList.Add("@Field", "");
            parameterList.Add("@TableName", "T_NoticeType");
            parameterList.Add("@PrimaryKey", "IdentityID");
            parameterList.Add("@PageIndex", pageIndex);
            parameterList.Add("@PageSize", pageSize);
            parameterList.Add("@WhereSql", whereSql);
            parameterList.Add("@OrderSql", "TypeSort desc");

            return DataBaseHelper.ToEntityList<DBNoticeTypeModel>("", parameterList, ref pageCount, ref totalCount, null, "PageCount", "TotalCount");
        }
    }
}
