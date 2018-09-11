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
        public bool Operater(DBRoleModel model, string menuList, string actionList)
        {
            menuList = StringHelper.PadChar(menuList, ",");
            actionList = StringHelper.PadChar(actionList, ",");

            if (model.IdentityID == 0)
            {
                string commandText = "insert into T_Role(RoleName, MenuList, ActionList)values(@RoleName, @MenuList, @ActionList)";
                return DataBaseHelper.ExecuteNonQuery(commandText, new { RoleName = model.RoleName, MenuList = menuList, ActionList = actionList }) > 0;
            }
            else
            {
                string commandText = "update T_Role set RoleName=@RoleName, MenuList=@MenuList, ActionList=@ActionList where IdentityID=@IdentityID";
                return DataBaseHelper.ExecuteNonQuery(commandText, new { RoleName = model.RoleName, MenuList = menuList, ActionList = actionList, IdentityID = model.IdentityID }) > 0;
            }
        }

        public bool Exists(string roleName, int identityID)
        {
            string commandText = "select identityID from T_Role with(nolock) where RoleName=@RoleName";
            int result = DataBaseHelper.ExecuteScalar<int>(commandText, new { RoleName = roleName });

            if (identityID == 0) return result > 0;
            return result == 0 ? false : (result != identityID);
        }

        public bool Delete(int identityID)
        {
            string commandText = "delete from T_Role where IdentityID=@IdentityID";
            return DataBaseHelper.ExecuteNonQuery(commandText, new { IdentityID = identityID }) > 0;
        }

        public bool DeleteMore(string identityIDList)
        {
            identityIDList = StringHelper.TrimChar(identityIDList, ",");

            string commandText = "delete from T_Role where IdentityID in (@IdentityIDList)";
            commandText = commandText.Replace("@IdentityIDList", identityIDList);

            return DataBaseHelper.ExecuteNonQuery(commandText) > 0;
        }

        public DBRoleModel Select(int identityID)
        {
            string commandText = "select IdentityID, RoleName, MenuList, ActionList from T_Role with(nolock) where IdentityID=@IdentityID";
            return DataBaseHelper.ToEntity<DBRoleModel>(commandText, new { IdentityID = identityID });
        }

        public List<DBRoleModel> List()
        {
            string commandText = "select IdentityID, RoleName from T_Role with(nolock)";
            return DataBaseHelper.ToEntityList<DBRoleModel>(commandText);
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
