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
        private const string TABLE_NAME = "T_LinkFriendType";

        public bool Operater(DBLinkFriendTypeModel model)
        {
            if (model.IdentityID == 0)
            {
                return DataBaseHelper.Insert<DBLinkFriendTypeModel>(model, p => p.IdentityID, TABLE_NAME);
            }
            else
            {
                return DataBaseHelper.Update<DBLinkFriendTypeModel>(model, p => p.IdentityID == p.IdentityID, p => p.IdentityID, TABLE_NAME);
            }
        }
        public bool Exists(string typeName, int identityID)
        {
            return DataBaseHelper.Exists<DBLinkFriendTypeModel>(new { TypeName = typeName }, p => p.IdentityID, p => p.TypeName == p.TypeName, identityID, TABLE_NAME);
        }
        public bool Delete(int identityID)
        {
            return DataBaseHelper.Delete<DBLinkFriendTypeModel>(new { IdentityID = identityID }, p => p.IdentityID == p.IdentityID, TABLE_NAME);
        }
        public bool DeleteMore(string identityIDList)
        {
            List<int> dataList = StringHelper.ToList<int>(identityIDList, ",");
            return DataBaseHelper.Delete<DBLinkFriendTypeModel>(null, p => dataList.Contains(p.IdentityID), TABLE_NAME);
        }
        public DBLinkFriendTypeModel Select(int identityID)
        {
            return DataBaseHelper.Single<DBLinkFriendTypeModel>(new { IdentityID = identityID }, p => new { p.IdentityID, p.TypeName, p.TypeSort }, p => p.IdentityID == p.IdentityID, TABLE_NAME);
        }
        public List<DBLinkFriendTypeModel> List()
        {
            return DataBaseHelper.More<DBLinkFriendTypeModel>(null, p => new { p.IdentityID, p.TypeName }, null, p => p.TypeSort, true, TABLE_NAME);
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
