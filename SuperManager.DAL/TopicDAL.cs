﻿using Helper.Core.Library;
using SuperManager.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.DAL
{
    public class TopicDAL
    {
        private const string TABLE_NAME = "T_Topic";

        public bool Operater(DBTopicModel model)
        {
            if (model.IdentityID == 0)
            {
                return DataBaseHelper.Insert<DBTopicModel>(model, p => new { p.IdentityID, p.TopicVisitNum, p.TopicDateTime }, TABLE_NAME);
            }
            else
            {
                return DataBaseHelper.Update<DBTopicModel>(model, p => p.IdentityID == p.IdentityID, p => new { p.IdentityID, p.TopicVisitNum, p.TopicDateTime }, TABLE_NAME);
            }
        }
        public bool Delete(int identityID)
        {
            return DataBaseHelper.Delete<DBTopicModel>(new { IdentityID = identityID }, p => p.IdentityID == p.IdentityID, TABLE_NAME);
        }
        public bool DeleteMore(string identityIDList)
        {
            List<int> dataList = StringHelper.ToList<int>(identityIDList, ",");
            return DataBaseHelper.Delete<DBTopicModel>(null, p => dataList.Contains(p.IdentityID), TABLE_NAME);
        }
        public bool StatusMore(string identityIDList, int topicStatus)
        {
            List<int> dataList = StringHelper.ToList<int>(identityIDList, ",");
            return DataBaseHelper.Update<DBTopicModel>(new { TopicStatus = topicStatus }, p => dataList.Contains(p.IdentityID), null, TABLE_NAME);
        }
        public DBTopicModel Select(int identityID)
        {
            return DataBaseHelper.Single<DBTopicModel>(new { IdentityID = identityID }, p => new { p.IdentityID, p.TopicType, p.PositionTypeList, p.TopicTitle, p.TopicTags, p.TopicCoverImageUrl, p.TopicSummary, p.TopicContent, p.TopicOriginalWebsite, p.TopicOriginalUrl, p.TopicUserCode, p.TopicStatus }, p => p.IdentityID == p.IdentityID, TABLE_NAME);
        }

        public List<DBTopicFullModel> Page(string searchKey, int topicType, int topicPositionType, int topicStatus, int pageIndex, int pageSize, ref int totalCount, ref int pageCount)
        {
            StringBuilder stringBuilder = new StringBuilder();
            // 如果需要筛选状态
            if (topicStatus != -1)
            {
                stringBuilder.Append(string.Format(" TopicStatus = {0} and ", topicStatus));
            }

            if (!string.IsNullOrEmpty(searchKey))
            {
                stringBuilder.Append(string.Format(" TopicTitle like '%{0}%' ", searchKey));
                stringBuilder.Append(" and ");
            }
            if (topicType > 0)
            {
                stringBuilder.Append(" TopicType = ");
                stringBuilder.Append(topicType);
                stringBuilder.Append(" and ");
            }
            if (topicPositionType > 0)
            {
                stringBuilder.Append(" CHARINDEX(',");
                stringBuilder.Append(topicPositionType);
                stringBuilder.Append(",', PositionTypeList)>0 and ");
            }

            string whereSql = stringBuilder.ToString().TrimEnd().TrimEnd(new char[] { 'a', 'n', 'd' });

            Dictionary<string, object> parameterList = new Dictionary<string, object>();
            parameterList.Add("@FieldSql", "IdentityID, TopicType, PositionTypeList, TopicTitle, TopicCoverImageUrl, TopicStatus, TopicUserCode, TopicVisitNum, TopicDateTime, (select TypeName from T_TopicType with(nolock) where T_TopicType.IdentityID=T.TopicType) as TopicTypeName");
            parameterList.Add("@Field", "IdentityID, TopicType, PositionTypeList, TopicTitle, TopicCoverImageUrl, TopicStatus, TopicUserCode, TopicVisitNum, TopicDateTime");
            parameterList.Add("@TableName", "T_Topic");
            parameterList.Add("@PrimaryKey", "IdentityID");
            parameterList.Add("@PageIndex", pageIndex);
            parameterList.Add("@PageSize", pageSize);
            parameterList.Add("@WhereSql", whereSql);
            parameterList.Add("@OrderSql", "IdentityID asc");

            return DataBaseHelper.ToEntityList<DBTopicFullModel>("", parameterList, ref pageCount, ref totalCount, null, "PageCount", "TotalCount");
        }
    }
}
