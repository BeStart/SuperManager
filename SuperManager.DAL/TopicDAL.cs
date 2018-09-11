using Helper.Core.Library;
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
        /// <summary>
        /// 添加/更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Operater(DBTopicModel model)
        {
            if (model.IdentityID == 0)
            {
                string commandText = "insert into T_Topic(TopicType,PositionTypeList,TopicTitle,TopicTags,TopicCoverImageUrl,TopicSummary,TopicContent,TopicOriginalWebsite,TopicOriginalUrl,TopicUserCode,TopicStatus)values(@TopicType,@PositionTypeList,@TopicTitle,@TopicTags,@TopicCoverImageUrl,@TopicSummary,@TopicContent,@TopicOriginalWebsite,@TopicOriginalUrl,@TopicUserCode,@TopicStatus)";
                return DataBaseHelper.ExecuteNonQuery(commandText, new { TopicType = model.TopicType, PositionTypeList = model.PositionTypeList, TopicTitle = model.TopicTitle, TopicTags = model.TopicTags, TopicCoverImageUrl = model.TopicCoverImageUrl, TopicSummary = model.TopicSummary, TopicContent = model.TopicContent, TopicOriginalWebsite = model.TopicOriginalWebsite, TopicOriginalUrl = model.TopicOriginalUrl, TopicUserCode = model.TopicUserCode, TopicStatus = model.TopicStatus }) > 0;
            }
            else
            {
                string commandText = "update T_Topic set TopicType=@TopicType,PositionTypeList=@PositionTypeList,TopicTitle=@TopicTitle,TopicTags=@TopicTags,TopicCoverImageUrl=@TopicCoverImageUrl,TopicSummary=@TopicSummary,TopicContent=@TopicContent,TopicOriginalWebsite=@TopicOriginalWebsite,TopicOriginalUrl=@TopicOriginalUrl,TopicUserCode=@TopicUserCode where IdentityID=@IdentityID";
                return DataBaseHelper.ExecuteNonQuery(commandText, new { TopicType = model.TopicType, PositionTypeList = model.PositionTypeList, TopicTitle = model.TopicTitle, TopicTags = model.TopicTags, TopicCoverImageUrl = model.TopicCoverImageUrl, TopicSummary = model.TopicSummary, TopicContent = model.TopicContent, TopicOriginalWebsite = model.TopicOriginalWebsite, TopicOriginalUrl = model.TopicOriginalUrl, TopicUserCode = model.TopicUserCode, IdentityID = model.IdentityID }) > 0;
            }
        }

        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="identityID"></param>
        /// <returns></returns>
        public bool Delete(int identityID)
        {
            string commandText = "delete from T_Topic where IdentityID=@IdentityID";
            return DataBaseHelper.ExecuteNonQuery(commandText, new { IdentityID = identityID }) > 0;
        }

        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <param name="identityIDList"></param>
        /// <returns></returns>
        public bool DeleteMore(string identityIDList)
        {
            identityIDList = StringHelper.TrimChar(identityIDList, ",");

            string commandText = "delete from T_Topic where IdentityID in (@IdentityIDList)";
            commandText = commandText.Replace("@IdentityIDList", identityIDList);

            return DataBaseHelper.ExecuteNonQuery(commandText) > 0;
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="identityIDList"></param>
        /// <param name="topicStatus"></param>
        /// <returns></returns>
        public bool StatusMore(string identityIDList, int topicStatus)
        {
            identityIDList = StringHelper.TrimChar(identityIDList, ",");

            string commandText = "update T_Topic set TopicStatus=@TopicStatus where IdentityID in (@IdentityIDList)";
            commandText = commandText.Replace("@IdentityIDList", identityIDList);

            return DataBaseHelper.ExecuteNonQuery(commandText, new { TopicStatus = topicStatus }) > 0;
        }

        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <param name="identityID"></param>
        /// <returns></returns>
        public DBTopicModel Select(int identityID)
        {
            string commandText = "select IdentityID, TopicType,PositionTypeList,TopicTitle,TopicTags,TopicCoverImageUrl,TopicSummary,TopicContent,TopicOriginalWebsite,TopicOriginalUrl,TopicUserCode,TopicStatus from T_Topic with(nolock) where IdentityID=@IdentityID";
            return DataBaseHelper.ToEntity<DBTopicModel>(commandText, new { IdentityID = identityID });
        }

        /// <summary>
        /// 管理后台分页显示
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="topicType"></param>
        /// <param name="topicPositionType"></param>
        /// <param name="topicStatus"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
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
