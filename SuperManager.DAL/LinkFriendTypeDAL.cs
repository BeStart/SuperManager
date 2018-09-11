using Helper.Core.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperManager.MODEL;

namespace SuperManager.DAL
{
    public class LinkFriendTypeDAL
    {
        public bool Operater(DBLinkFriendTypeModel model)
        {
            if (model.IdentityID == 0)
            {
                string commandText = "insert into T_LinkFriendType(TypeName, TypeSort)values(@TypeName, @TypeSort)";
                return DataBaseHelper.ExecuteNonQuery(commandText, new { model.TypeName, model.TypeSort }) > 0;
            }
            else
            {
                string commandText = "update T_LinkFriendType set TypeName=@TypeName, TypeSort=@TypeSort where IdentityID=@IdentityID";
                return DataBaseHelper.ExecuteNonQuery(commandText, new { model.TypeName, model.TypeSort, model.IdentityID }) > 0;
            }
        }
        public bool Exists(string typeName, int identityID)
        {
            string commandText = "select IdentityID from T_LinkFriendType with(nolock) where TypeName=@TypeName";
            int result = DataBaseHelper.ExecuteScalar<int>(commandText, new { TypeName = typeName });

            if (identityID == 0) return result > 0;
            return result == 0 ? false : (result != identityID);
        }
        public bool Delete(int identityID)
        {
            string commandText = "delete from T_LinkFriendType where IdentityID=@IdentityID";
            return DataBaseHelper.ExecuteNonQuery(commandText, new { IdentityID = identityID }) > 0;
        }
        public bool DeleteMore(string identityIDList)
        {
            identityIDList = StringHelper.TrimChar(identityIDList, ",");

            string commandText = "delete from T_LinkFriendType where IdentityID in (@IdentityIDList)";
            commandText = commandText.Replace("@IdentityIDList", identityIDList);

            return DataBaseHelper.ExecuteNonQuery(commandText) > 0;
        }
        public DBLinkFriendTypeModel Select(int identityID)
        {
            string commandText = "select IdentityID, TypeName, TypeSort from T_LinkFriendType with(nolock) where IdentityID=@IdentityID";
            return DataBaseHelper.ToEntity<DBLinkFriendTypeModel>(commandText, new { IdentityID = identityID });
        }
        public List<DBLinkFriendTypeModel> List()
        {
            string commandText = "select IdentityID, TypeName from T_LinkFriendType with(nolock) order by TypeSort desc";
            return DataBaseHelper.ToEntityList<DBLinkFriendTypeModel>(commandText);
        }
        public List<DBLinkFriendTypeModel> All(string searchKey)
        {
            searchKey = StringHelper.FilterSpecChar(searchKey);

            string commandText = "select IdentityID, TypeName, TypeSort from T_LinkFriendType with(nolock) {0} order by TypeSort desc";
            StringBuilder stringBuilder = new StringBuilder();
            if(!string.IsNullOrEmpty(searchKey))
            {
                stringBuilder.Append(" where TypeName like '%");
                stringBuilder.Append(searchKey);
                stringBuilder.Append("%' ");
            }
            commandText = string.Format(commandText, stringBuilder.ToString());
            return DataBaseHelper.ToEntityList<DBLinkFriendTypeModel>(commandText);
        }
    }
}
