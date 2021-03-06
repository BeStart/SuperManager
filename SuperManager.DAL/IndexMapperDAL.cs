﻿using Helper.Core.Library;
using SuperManager.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.DAL
{
    public class IndexMapperDAL
    {
        private const string TABLE_NAME = "T_IndexMapper";

        public bool Operater(DBIndexMapperModel model)
        {
            if (model.IdentityID == 0)
            {
                return DataBaseHelper.Insert<DBIndexMapperModel>(model, p => p.IdentityID, TABLE_NAME);
            }
            else
            {
                return DataBaseHelper.Update<DBIndexMapperModel>(model, p => p.IdentityID == p.IdentityID, p => p.IdentityID, TABLE_NAME);
            }
        }
        public bool Delete(int identityID)
        {
            return DataBaseHelper.Delete<DBIndexMapperModel>(new { IdentityID = identityID }, p => p.IdentityID == p.IdentityID, TABLE_NAME);
        }
        public bool DeleteMore(string identityIDList)
        {
            List<int> dataList = StringHelper.ToList<int>(identityIDList, ",");
            return DataBaseHelper.Delete<DBIndexMapperModel>(null, p => dataList.Contains(p.IdentityID), TABLE_NAME);
        }
        public DBIndexMapperModel Select(int identityID)
        {
            return DataBaseHelper.Single<DBIndexMapperModel>(new { IdentityID = identityID }, p => new { p.IdentityID, p.IndexType, p.IndexID, p.MapperID }, p => p.IdentityID == p.IdentityID, TABLE_NAME);
        }
        public List<DBIndexMapperModel> List()
        {
            return DataBaseHelper.More<DBIndexMapperModel>(null, p => new { p.IdentityID, p.IndexType, p.IndexID }, null, null, true, TABLE_NAME);
        }
        public List<DBIndexMapperModel> Page(int indexType, int pageIndex, int pageSize, ref int totalCount, ref int pageCount)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (indexType != -1)
            {
                stringBuilder.Append(" IndexType = ");
                stringBuilder.Append(indexType);
                stringBuilder.Append(" and ");
            }
            string whereSql = stringBuilder.ToString().TrimEnd().TrimEnd(new char[] { 'a', 'n', 'd' });

            Dictionary<string, object> parameterList = new Dictionary<string, object>();
            parameterList.Add("@FieldSql", "IdentityID, IndexType, IndexID, MapperID");
            parameterList.Add("@Field", "");
            parameterList.Add("@TableName", "T_IndexMapper");
            parameterList.Add("@PrimaryKey", "IdentityID");
            parameterList.Add("@PageIndex", pageIndex);
            parameterList.Add("@PageSize", pageSize);
            parameterList.Add("@WhereSql", whereSql);
            parameterList.Add("@OrderSql", "IdentityID desc");

            return DataBaseHelper.ToEntityList<DBIndexMapperModel>("", parameterList, ref pageCount, ref totalCount, null, "PageCount", "TotalCount");
        }
    }
}
