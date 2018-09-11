using Helper.Core.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperManager.MODEL;

namespace SuperManager.DAL
{
    public class LinkFriendDAL
    {
        public bool Operater(DBLinkFriendModel model)
        {
            if (model.IdentityID == 0)
            {
                string commandText = "insert into T_LinkFriend(LinkFriendType, LinkFriendCoverImageUrl, LinkFriendName, LinkFriendUrl, LinkFriendSort)values(@LinkFriendType, @LinkFriendCoverImageUrl, @LinkFriendName, @LinkFriendUrl, @LinkFriendSort)";
                return DataBaseHelper.ExecuteNonQuery(commandText, new { model.LinkFriendType, model.LinkFriendCoverImageUrl, model.LinkFriendName, model.LinkFriendUrl, model.LinkFriendSort }) > 0;
            }
            else
            {
                string commandText = "update T_LinkFriend set LinkFriendType=@LinkFriendType, LinkFriendCoverImageUrl=@LinkFriendCoverImageUrl, LinkFriendName=@LinkFriendName, LinkFriendUrl=@LinkFriendUrl, LinkFriendSort=@LinkFriendSort where IdentityID=@IdentityID";
                return DataBaseHelper.ExecuteNonQuery(commandText, new { model.LinkFriendType, model.LinkFriendCoverImageUrl, model.LinkFriendName, model.LinkFriendUrl, model.LinkFriendSort, IdentityID = model.IdentityID }) > 0;
            }
        }
        public bool Delete(int identityID)
        {
            string commandText = "delete from T_LinkFriend where IdentityID=@IdentityID";
            return DataBaseHelper.ExecuteNonQuery(commandText, new { IdentityID = identityID }) > 0;
        }
        public bool DeleteMore(string identityIDList)
        {
            identityIDList = StringHelper.TrimChar(identityIDList, ",");

            string commandText = "delete from T_LinkFriend where IdentityID in (@IdentityIDList)";
            commandText = commandText.Replace("@IdentityIDList", identityIDList);

            return DataBaseHelper.ExecuteNonQuery(commandText) > 0;
        }
        public DBLinkFriendModel Select(int identityID)
        {
            string commandText = "select IdentityID, LinkFriendType, LinkFriendCoverImageUrl, LinkFriendName, LinkFriendUrl, LinkFriendSort from T_LinkFriend with(nolock) where IdentityID=@IdentityID";
            return DataBaseHelper.ToEntity<DBLinkFriendModel>(commandText, new { IdentityID = identityID });
        }
        public List<DBLinkFriendFullModel> Page(string searchKey, int linkFriendType, int pageIndex, int pageSize, ref int totalCount, ref int pageCount)
        {
            searchKey = StringHelper.FilterSpecChar(searchKey);

            StringBuilder stringBuilder = new StringBuilder();

            if (!string.IsNullOrEmpty(searchKey))
            {
                stringBuilder.Append(string.Format(" LinkFriendName like '%{0}%' ", searchKey));
                stringBuilder.Append(" and ");
            }
            if (linkFriendType > 0)
            {
                stringBuilder.Append(" LinkFriendType = ");
                stringBuilder.Append(linkFriendType);
                stringBuilder.Append(" and ");
            }

            string whereSql = stringBuilder.ToString().TrimEnd().TrimEnd(new char[] { 'a', 'n', 'd' });

            Dictionary<string, object> parameterList = new Dictionary<string, object>();
            parameterList.Add("@FieldSql", "IdentityID, LinkFriendType, LinkFriendCoverImageUrl, LinkFriendName, LinkFriendUrl, LinkFriendSort, (select TypeName from T_LinkFriendType with(nolock) where T_LinkFriendType.IdentityID=T.LinkFriendType) as LinkFriendTypeName");
            parameterList.Add("@Field", "IdentityID, LinkFriendType, LinkFriendCoverImageUrl, LinkFriendName, LinkFriendUrl, LinkFriendSort");
            parameterList.Add("@TableName", "T_LinkFriend");
            parameterList.Add("@PrimaryKey", "IdentityID");
            parameterList.Add("@PageIndex", pageIndex);
            parameterList.Add("@PageSize", pageSize);
            parameterList.Add("@WhereSql", whereSql);
            parameterList.Add("@OrderSql", "IdentityID asc");

            return DataBaseHelper.ToEntityList<DBLinkFriendFullModel>("", parameterList, ref pageCount, ref totalCount, null, "PageCount", "TotalCount");
        }
    }
}
