using Helper.Core.Library;
using SuperManager.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.DAL
{
    public class TopicPositionTypeDAL
    {
        private const string TABLE_NAME = "T_TopicPositionType";

        public bool Operater(DBTopicPositionTypeModel model)
        {
            if (model.IdentityID == 0)
            {
                return DataBaseHelper.Insert<DBTopicPositionTypeModel>(model, p => p.IdentityID, TABLE_NAME);
            }
            else
            {
                return DataBaseHelper.Update<DBTopicPositionTypeModel>(model, p => p.IdentityID == p.IdentityID, p => p.IdentityID, TABLE_NAME);
            }
        }
        public bool Exists(string typeName, int identityID)
        {
            return DataBaseHelper.Exists<DBTopicPositionTypeModel>(new { TypeName = typeName }, p => p.IdentityID, p => p.TypeName == p.TypeName, identityID, TABLE_NAME);
        }
        public bool Delete(int identityID)
        {
            return DataBaseHelper.Delete<DBTopicPositionTypeModel>(new { IdentityID = identityID }, p => p.IdentityID == p.IdentityID, TABLE_NAME);
        }
        public bool DeleteMore(string identityIDList)
        {
            List<int> dataList = StringHelper.ToList<int>(identityIDList, ",");
            return DataBaseHelper.Delete<DBTopicPositionTypeModel>(null, p => dataList.Contains(p.IdentityID), TABLE_NAME);
        }
        public DBTopicPositionTypeModel Select(int identityID)
        {
            return DataBaseHelper.Single<DBTopicPositionTypeModel>(new { IdentityID = identityID }, p => new { p.IdentityID, p.TypeName, p.TypeSort }, p => p.IdentityID == p.IdentityID, TABLE_NAME);
        }
        public List<DBTopicPositionTypeModel> List()
        {
            return DataBaseHelper.More<DBTopicPositionTypeModel>(null, p => new { p.IdentityID, p.TypeName }, null, p => p.TypeSort, true, TABLE_NAME);
        }

        public List<DBTopicPositionTypeModel> All(string searchKey)
        {
            string commandText = "select IdentityID, TypeName, TypeSort from T_TopicPositionType with(nolock) {0} order by TypeSort desc";
            StringBuilder stringBuilder = new StringBuilder();
            if(!string.IsNullOrEmpty(searchKey))
            {
                stringBuilder.Append(" where TypeName like '%");
                stringBuilder.Append(searchKey);
                stringBuilder.Append("%' ");
            }
            commandText = string.Format(commandText, stringBuilder.ToString());
            return DataBaseHelper.ToEntityList<DBTopicPositionTypeModel>(commandText);
        }
    }
}
