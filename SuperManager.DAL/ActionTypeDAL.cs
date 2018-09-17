using Helper.Core.Library;
using SuperManager.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.DAL
{
    public class ActionTypeDAL
    {
        private const string TABLE_NAME = "T_ActionType";

        public bool Operater(DBActionTypeModel model)
        {
            if (model.IdentityID == 0)
            {
                return DataBaseHelper.Insert<DBActionTypeModel>(model, p => p.IdentityID, TABLE_NAME);
            }
            else
            {
                return DataBaseHelper.Update<DBActionTypeModel>(model, p => p.IdentityID == p.IdentityID, p => p.IdentityID, TABLE_NAME);
            }
        }
        public bool Exists(string typeCode, int identityID)
        {
            return DataBaseHelper.Exists<DBActionTypeModel>(new { TypeCode = typeCode }, p => p.IdentityID, p => p.TypeCode == p.TypeCode, identityID, TABLE_NAME);
        }
        public bool Delete(int identityID)
        {
            return DataBaseHelper.Delete<DBActionTypeModel>(new { IdentityID = identityID }, p => p.IdentityID == p.IdentityID, TABLE_NAME);
        }
        public bool DeleteMore(string identityIDList)
        {
            List<int> dataList = StringHelper.ToList<int>(identityIDList, ",");
            return DataBaseHelper.Delete<DBActionTypeModel>(null, p => dataList.Contains(p.IdentityID), TABLE_NAME);
        }
        public DBActionTypeModel Select(int identityID)
        {
            return DataBaseHelper.Single<DBActionTypeModel>(new { IdentityID = identityID }, p => new { p.IdentityID, p.TypeCode, p.TypeName, p.TypeSort }, p => p.IdentityID == p.IdentityID, TABLE_NAME);
        }
        public List<DBActionTypeModel> List()
        {
            return DataBaseHelper.More<DBActionTypeModel>(null, p => new { p.IdentityID, p.TypeCode, p.TypeName }, null, p => p.TypeSort, true, TABLE_NAME);
        }
        public List<DBActionTypeModel> List(string typeCodeList)
        {
            List<string> dataList = StringHelper.ToList<string>(typeCodeList, ",");
            return DataBaseHelper.More<DBActionTypeModel>(null, p => new { p.IdentityID, p.TypeCode, p.TypeName }, p => dataList.Contains(p.TypeCode), p => p.TypeSort, true, TABLE_NAME);
        }

        public List<DBActionTypeModel> All(string searchKey)
        {
            string commandText = "select IdentityID, TypeCode, TypeName from T_ActionType with(nolock) {0} order by TypeSort desc";
            StringBuilder stringBuilder = new StringBuilder();
            if(!string.IsNullOrEmpty(searchKey))
            {
                stringBuilder.Append(" where TypeCode like '%");
                stringBuilder.Append(searchKey);
                stringBuilder.Append("%' or TypeName like '%");
                stringBuilder.Append(searchKey);
                stringBuilder.Append("%' ");
            }
            commandText = string.Format(commandText, stringBuilder.ToString());
            return DataBaseHelper.ToEntityList<DBActionTypeModel>(commandText);
        }
    }
}
