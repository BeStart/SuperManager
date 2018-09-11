using Helper.Core.Library;
using SuperManager.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.DAL
{
    public class FlowTypeDAL
    {
        public bool Operater(DBFlowTypeModel model)
        {
            if (model.IdentityID == 0)
            {
                string commandText = "insert into T_FlowType(TypeName, TypeSort)values(@TypeName, @TypeSort)";
                return DataBaseHelper.ExecuteNonQuery(commandText, new { TypeName = model.TypeName, TypeSort = model.TypeSort }) > 0;
            }
            else
            {
                string commandText = "update T_FlowType set TypeName=@TypeName, TypeSort=@TypeSort where IdentityID=@IdentityID";
                return DataBaseHelper.ExecuteNonQuery(commandText, new { TypeName = model.TypeName, TypeSort = model.TypeSort, IdentityID = model.IdentityID }) > 0;
            }
        }

        public bool Exists(string typeName, int identityID)
        {
            string commandText = "select IdentityID from T_FlowType with(nolock) where TypeName=@TypeName";
            int result = DataBaseHelper.ExecuteScalar<int>(commandText, new { TypeName = typeName });

            if (identityID == 0) return result > 0;
            return result == 0 ? false : (result != identityID);
        }

        public bool Delete(int identityID)
        {
            string commandText = "delete from T_FlowType where IdentityID=@IdentityID";
            return DataBaseHelper.ExecuteNonQuery(commandText, new { IdentityID = identityID }) > 0;
        }

        public bool DeleteMore(string identityIDList)
        {
            identityIDList = StringHelper.TrimChar(identityIDList, ",");

            string commandText = "delete from T_FlowType where IdentityID in (@IdentityIDList)";
            commandText = commandText.Replace("@IdentityIDList", identityIDList);

            return DataBaseHelper.ExecuteNonQuery(commandText) > 0;
        }

        public DBFlowTypeModel Select(int identityID)
        {
            string commandText = "select IdentityID, TypeName, TypeSort from T_FlowType with(nolock) where IdentityID=@IdentityID";
            return DataBaseHelper.ToEntity<DBFlowTypeModel>(commandText, new { IdentityID = identityID });
        }

        public List<DBFlowTypeModel> List()
        {
            string commandText = "select IdentityID, TypeName from T_FlowType with(nolock)";
            return DataBaseHelper.ToEntityList<DBFlowTypeModel>(commandText);
        }

        public List<DBFlowTypeModel> All(string searchKey)
        {
            string commandText = "select IdentityID, TypeName, TypeSort from T_FlowType with(nolock) {0} order by TypeSort desc";
            
            StringBuilder stringBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(searchKey))
            {
                stringBuilder.Append(" where TypeName like '%");
                stringBuilder.Append(searchKey);
                stringBuilder.Append("%' ");
            }

            commandText = string.Format(commandText, stringBuilder.ToString());
            return DataBaseHelper.ToEntityList<DBFlowTypeModel>(commandText);
        }
    }
}
