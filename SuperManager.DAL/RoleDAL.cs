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
            menuList = StringHelper.PadChar(menuList, ",");
            actionList = StringHelper.PadChar(actionList, ",");

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

        public List<DBRoleModel> All(string searchKey)
        {
            string commandText = "select IdentityID, RoleName from T_Role with(nolock) {0}";
            StringBuilder stringBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(searchKey))
            {
                stringBuilder.Append(" where RoleName like '%");
                stringBuilder.Append(searchKey);
                stringBuilder.Append("%' ");
            }
            commandText = string.Format(commandText, stringBuilder.ToString());
            return DataBaseHelper.ToEntityList<DBRoleModel>(commandText);
        }
    }
}
