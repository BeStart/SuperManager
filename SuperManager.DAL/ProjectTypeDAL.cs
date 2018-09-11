using Helper.Core.Library;
using SuperManager.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.DAL
{
    public class ProjectTypeDAL
    {
        public bool Operater(DBProjectTypeModel model)
        {
            if (model.IdentityID == 0)
            {
                string commandText = "insert into T_ProjectType(TypeName, TypeSort)values(@TypeName, @TypeSort)";
                return DataBaseHelper.ExecuteNonQuery(commandText, new { TypeName = model.TypeName, TypeSort = model.TypeSort }) > 0;
            }
            else
            {
                string commandText = "update T_ProjectType set TypeName=@TypeName, TypeSort=@TypeSort where IdentityID=@IdentityID";
                return DataBaseHelper.ExecuteNonQuery(commandText, new { TypeName = model.TypeName, TypeSort = model.TypeSort, IdentityID = model.IdentityID }) > 0;
            }
        }

        public bool Exists(string typeName, int identityID)
        {
            string commandText = "select IdentityID from T_ProjectType with(nolock) where TypeName=@TypeName";
            int result = DataBaseHelper.ExecuteScalar<int>(commandText, new { TypeName = typeName });

            if (identityID == 0) return result > 0;
            return result == 0 ? false : (result != identityID);
        }

        public bool Delete(int identityID)
        {
            string commandText = "delete from T_ProjectType where IdentityID=@IdentityID";
            return DataBaseHelper.ExecuteNonQuery(commandText, new { IdentityID = identityID }) > 0;
        }

        public bool DeleteMore(string identityIDList)
        {
            identityIDList = StringHelper.TrimChar(identityIDList, ",");

            string commandText = "delete from T_ProjectType where IdentityID in (@IdentityIDList)";
            commandText = commandText.Replace("@IdentityIDList", identityIDList);

            return DataBaseHelper.ExecuteNonQuery(commandText) > 0;
        }

        public DBProjectTypeModel Select(int identityID)
        {
            string commandText = "select IdentityID, TypeName, TypeSort from T_ProjectType with(nolock) where IdentityID=@IdentityID";
            return DataBaseHelper.ToEntity<DBProjectTypeModel>(commandText, new { IdentityID = identityID });
        }

        public List<DBProjectTypeModel> List()
        {
            string commandText = "select IdentityID, TypeName from T_ProjectType with(nolock)";
            return DataBaseHelper.ToEntityList<DBProjectTypeModel>(commandText);
        }

        public List<DBProjectTypeModel> Page(string searchKey, int pageIndex, int pageSize, ref int totalCount, ref int pageCount)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (!string.IsNullOrEmpty(searchKey))
            {
                stringBuilder.Append(string.Format(" TypeName like '%{0}%' ", searchKey));
                stringBuilder.Append(" and ");
            }

            string whereSql = stringBuilder.ToString().TrimEnd().TrimEnd(new char[] { 'a', 'n', 'd' });

            Dictionary<string, object> parameterList = new Dictionary<string, object>();
            parameterList.Add("@FieldSql", "IdentityID, TypeName, TypeSort");
            parameterList.Add("@Field", "IdentityID, TypeName, TypeSort");
            parameterList.Add("@TableName", "T_ProjectType");
            parameterList.Add("@PrimaryKey", "IdentityID");
            parameterList.Add("@PageIndex", pageIndex);
            parameterList.Add("@PageSize", pageSize);
            parameterList.Add("@WhereSql", whereSql);
            parameterList.Add("@OrderSql", "IdentityID asc");

            return DataBaseHelper.ToEntityList<DBProjectTypeModel>("", parameterList, ref pageCount, ref totalCount, null, "PageCount", "TotalCount");
        }
    }
}
