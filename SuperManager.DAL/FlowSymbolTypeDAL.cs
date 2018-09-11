using Helper.Core.Library;
using SuperManager.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.DAL
{
    public class FlowSymbolTypeDAL
    {
        public bool Operater(DBFlowSymbolTypeModel model)
        {
            if (model.IdentityID == 0)
            {
                string commandText = "insert into T_FlowSymbolType(TypeCode, TypeName, TypeSort)values(@TypeCode, @TypeName, @TypeSort)";
                return DataBaseHelper.ExecuteNonQuery(commandText, new { TypeCode = model.TypeCode, TypeName = model.TypeName, TypeSort = model.TypeSort }) > 0;
            }
            else
            {
                string commandText = "update T_FlowSymbolType set TypeCode=@TypeCode, TypeName=@TypeName, TypeSort=@TypeSort where IdentityID=@IdentityID";
                return DataBaseHelper.ExecuteNonQuery(commandText, new { TypeCode = model.TypeCode, TypeName = model.TypeName, TypeSort = model.TypeSort, IdentityID = model.IdentityID }) > 0;
            }
        }

        public bool Exists(string typeCode, int identityID)
        {
            string commandText = "select IdentityID from T_FlowSymbolType with(nolock) where TypeCode=@TypeCode";
            int result = DataBaseHelper.ExecuteScalar<int>(commandText, new { TypeCode = typeCode });

            if (identityID == 0) return result > 0;
            return result == 0 ? false : (result != identityID);
        }

        public bool Delete(int identityID)
        {
            string commandText = "delete from T_FlowSymbolType where IdentityID=@IdentityID";
            return DataBaseHelper.ExecuteNonQuery(commandText, new { IdentityID = identityID }) > 0;
        }

        public bool DeleteMore(string identityIDList)
        {
            identityIDList = StringHelper.TrimChar(identityIDList, ",");

            string commandText = "delete from T_FlowSymbolType where IdentityID in (@IdentityIDList)";
            commandText = commandText.Replace("@IdentityIDList", identityIDList);

            return DataBaseHelper.ExecuteNonQuery(commandText) > 0;
        }

        public DBFlowSymbolTypeModel Select(int identityID)
        {
            string commandText = "select IdentityID, TypeCode, TypeName, TypeSort from T_FlowSymbolType with(nolock) where IdentityID=@IdentityID";
            return DataBaseHelper.ToEntity<DBFlowSymbolTypeModel>(commandText, new { IdentityID = identityID });
        }

        public List<DBFlowSymbolTypeModel> List()
        {
            string commandText = "select IdentityID, TypeCode, TypeName from T_FlowSymbolType with(nolock)";
            return DataBaseHelper.ToEntityList<DBFlowSymbolTypeModel>(commandText);
        }

        public List<DBFlowSymbolTypeModel> All(string searchKey)
        {
            string commandText = "select IdentityID, TypeCode, TypeName from T_FlowSymbolType with(nolock) {0} order by TypeSort desc";
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
            return DataBaseHelper.ToEntityList<DBFlowSymbolTypeModel>(commandText);
        }
    }
}
