﻿using Helper.Core.Library;
using SuperManager.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.DAL
{
    public class UserLogDAL
    {
        private const string TABLE_NAME = "T_UserLog";

        public bool Operater(DBUserLogModel model)
        {
            return DataBaseHelper.Insert<DBUserLogModel>(model, p => new { p.IdentityID, p.LoginDate }, TABLE_NAME);
        }
        public bool Delete(int identityID)
        {
            return DataBaseHelper.Delete<DBUserLogModel>(new { IdentityID = identityID }, p => p.IdentityID == p.IdentityID, TABLE_NAME);
        }
        public bool DeleteMore(string identityIDList)
        {
            List<int> dataList = StringHelper.ToList<int>(identityIDList, ",");
            return DataBaseHelper.Delete<DBUserLogModel>(null, p => dataList.Contains(p.IdentityID), TABLE_NAME);
        }

        public List<DBUserLogModel> Page(string searchKey, int loginStatus, int pageIndex, int pageSize, ref int totalCount, ref int pageCount)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (!string.IsNullOrEmpty(searchKey))
            {
                stringBuilder.Append(string.Format(" (UserCode like '%{0}%' ", searchKey));
                stringBuilder.Append(" or ");
                stringBuilder.Append(string.Format(" LoginIP like '%{0}%' ", searchKey));
                stringBuilder.Append(") and ");
            }
            if(loginStatus != -1)
            {
                stringBuilder.Append(" LoginStatus = ");
                stringBuilder.Append(loginStatus);
                stringBuilder.Append(" and ");
            }

            string whereSql = stringBuilder.ToString().TrimEnd().TrimEnd(new char[] { 'a', 'n', 'd' });

            Dictionary<string, object> parameterList = new Dictionary<string, object>();
            parameterList.Add("@FieldSql", "IdentityID, UserCode, LoginIP, LoginDate, LoginStatus");
            parameterList.Add("@Field", "");
            parameterList.Add("@TableName", "T_UserLog");
            parameterList.Add("@PrimaryKey", "IdentityID");
            parameterList.Add("@PageIndex", pageIndex);
            parameterList.Add("@PageSize", pageSize);
            parameterList.Add("@WhereSql", whereSql);
            parameterList.Add("@OrderSql", "IdentityID asc");

            return DataBaseHelper.ToEntityList<DBUserLogModel>("", parameterList, ref pageCount, ref totalCount, null, "PageCount", "TotalCount");
        }
    }
}
