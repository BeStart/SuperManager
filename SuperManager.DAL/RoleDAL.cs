using Helper.Core.Library;
using SuperManager.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.DAL
{
    public class RoleDAL
    {
        private const string TABLE_NAME = "T_Role";

        public bool Operater(DBRoleModel model, string menuList, string actionList)
        {
            model.ActionList = StringHelper.PadChar(actionList, ","); ;
            model.MenuList = StringHelper.PadChar(menuList, ",");

            if (model.IdentityID == 0)
            {
                return DataBaseHelper.Insert<DBRoleModel>(model, p => p.IdentityID, TABLE_NAME);
            }
            else
            {
                return DataBaseHelper.Update<DBRoleModel>(model, p => p.IdentityID == p.IdentityID, p => p.IdentityID, TABLE_NAME);
            }
        }
        public bool Exists(string roleName, int identityID)
        {
            return DataBaseHelper.Exists<DBRoleModel>(new { RoleName = roleName }, p => p.IdentityID, p => p.RoleName == p.RoleName, identityID, TABLE_NAME);
        }
        public bool Delete(int identityID)
        {
            return DataBaseHelper.Delete<DBRoleModel>(new { IdentityID = identityID }, p => p.IdentityID == p.IdentityID, TABLE_NAME);
        }
        public bool DeleteMore(string identityIDList)
        {
            List<int> dataList = StringHelper.ToList<int>(identityIDList, ",");
            return DataBaseHelper.Delete<DBRoleModel>(null, p => dataList.Contains(p.IdentityID), TABLE_NAME);
        }
        public DBRoleModel Select(int identityID)
        {
            return DataBaseHelper.Single<DBRoleModel>(new { IdentityID = identityID }, p => new { p.IdentityID, p.RoleName, p.MenuList, p.ActionList }, p => p.IdentityID == p.IdentityID, TABLE_NAME);
        }
        public List<DBRoleModel> List()
        {
            return DataBaseHelper.More<DBRoleModel>(null, p => new { p.IdentityID, p.RoleName }, null, null, true, TABLE_NAME);
        }

        public List<DBRoleModel> Page(string searchKey, int pageIndex, int pageSize, ref int totalCount, ref int pageCount)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(searchKey))
            {
                stringBuilder.Append(" RoleName like '%");
                stringBuilder.Append(searchKey);
                stringBuilder.Append("%' ");
            }
            string whereSql = stringBuilder.ToString().TrimEnd().TrimEnd(new char[] { 'a', 'n', 'd' });

            Dictionary<string, object> parameterList = new Dictionary<string, object>();
            parameterList.Add("@FieldSql", "IdentityID, RoleName");
            parameterList.Add("@Field", "");
            parameterList.Add("@TableName", "T_Role");
            parameterList.Add("@PrimaryKey", "IdentityID");
            parameterList.Add("@PageIndex", pageIndex);
            parameterList.Add("@PageSize", pageSize);
            parameterList.Add("@WhereSql", whereSql);
            parameterList.Add("@OrderSql", "IdentityID desc");

            return DataBaseHelper.ToEntityList<DBRoleModel>("", parameterList, ref pageCount, ref totalCount, null, "PageCount", "TotalCount");
        }
    }
}
