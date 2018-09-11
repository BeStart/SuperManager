using Helper.Core.Library;
using SuperManager.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.DAL
{
    public class UserDAL
    {
        public bool Operater(DBUserModel model)
        {
            if (model.IdentityID == 0)
            {
                string commandText = "insert into T_User(UserCode, NickName, UserPassword, RoleID)values(@UserCode, @NickName, @UserPassword, @RoleID)";
                return DataBaseHelper.ExecuteNonQuery(commandText, new { UserCode = model.UserCode, NickName = model.NickName, UserPassword = model.UserPassword, RoleID = model.RoleID }) > 0;
            }
            else
            {
                string commandText = "update T_User set NickName=@NickName, UserCode=@UserCode where IdentityID=@IdentityID";
                return DataBaseHelper.ExecuteNonQuery(commandText, new { UserCode = model.UserCode, NickName = model.NickName, RoleID = model.RoleID, IdentityID = model.IdentityID }) > 0;
            }
        }

        public bool EditInfo(DBUserModel model)
        {
            string commandText = "update T_User set NickName = @NickName where UserCode=@UserCode";
            return DataBaseHelper.ExecuteNonQuery(commandText, new { NickName = model.NickName, UserCode = model.UserCode }) > 0;
        }

        public bool EditPassword(string userCode, string oldPassword, string newPassword)
        {
            string commandText = "update T_User set UserPassword=@NewPassword where UserCode=@UserCode and UserPassword=@OldPassword";
            return DataBaseHelper.ExecuteNonQuery(commandText, new { UserCode = userCode, NewPassword = newPassword, OldPassword = oldPassword }) > 0;
        }

        public bool Exists(string userCode, int identityID)
        {
            string commandText = "select IdentityID from T_User with(nolock) where UserCode=@UserCode";
            int result = DataBaseHelper.ExecuteScalar<int>(commandText, new { UserCode = userCode });

            if (identityID == 0) return result > 0;
            return result != identityID;
        }

        public bool Delete(int identityID)
        {
            string commandText = "delete from T_User where IdentityID=@IdentityID";
            return DataBaseHelper.ExecuteNonQuery(commandText, new { IdentityID = identityID }) > 0;
        }

        public bool DeleteMore(string identityIDList)
        {
            identityIDList = StringHelper.TrimChar(identityIDList, ",");

            string commandText = "delete from T_User where IdentityID in (@IdentityIDList)";
            commandText = commandText.Replace("@IdentityIDList", identityIDList);

            return DataBaseHelper.ExecuteNonQuery(commandText) > 0;
        }

        public DBUserFullModel Select(string userCode, string userPassword)
        {
            string commandText = "select T_User.IdentityID, UserCode, RoleID, NickName, T_Role.RoleName, T_Role.MenuList, T_Role.ActionList from T_User with(nolock) left join T_Role with(nolock) on T_Role.IdentityID=T_User.RoleID where UserCode=@UserCode and UserPassword=@UserPassword";
            return DataBaseHelper.ToEntity<DBUserFullModel>(commandText, new { UserCode = userCode, UserPassword = userPassword });
        }
        public DBUserModel Select(int identityID)
        {
            string commandText = "select IdentityID, UserCode, NickName, UserCode from T_User with(nolock) where IdentityID=@IdentityID";
            return DataBaseHelper.ToEntity<DBUserModel>(commandText, new { IdentityID = identityID });
        }
        public DBUserModel Select(string userCode)
        {
            string commandText = "select IdentityID, UserCode, NickName, UserCode from T_User with(nolock) where UserCode=@UserCode";
            return DataBaseHelper.ToEntity<DBUserModel>(commandText, new { UserCode = userCode });
        }
        public List<DBUserFullModel> Page(string searchKey, int roleID, int pageIndex, int pageSize, ref int totalCount, ref int pageCount)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (!string.IsNullOrEmpty(searchKey))
            {
                stringBuilder.Append(string.Format(" (UserCode like '%{0}%' or NickName like '%{0}%') ", searchKey));
                stringBuilder.Append(" and ");
            }
            if(roleID > 0)
            {
                stringBuilder.Append(" RoleID = ");
                stringBuilder.Append(roleID);
                stringBuilder.Append(" and ");
            }

            string whereSql = stringBuilder.ToString().TrimEnd().TrimEnd(new char[] { 'a', 'n', 'd' });

            Dictionary<string, object> parameterList = new Dictionary<string, object>();
            parameterList.Add("@FieldSql", "IdentityID, UserCode, NickName, RoleID, (select RoleName from T_Role with(nolock) where T_Role.IdentityID=T.RoleID) as RoleName");
            parameterList.Add("@Field", "IdentityID, UserCode, NickName, RoleID");
            parameterList.Add("@TableName", "T_User");
            parameterList.Add("@PrimaryKey", "IdentityID");
            parameterList.Add("@PageIndex", pageIndex);
            parameterList.Add("@PageSize", pageSize);
            parameterList.Add("@WhereSql", whereSql);
            parameterList.Add("@OrderSql", "IdentityID asc");

            return DataBaseHelper.ToEntityList<DBUserFullModel>("", parameterList, ref pageCount, ref totalCount, null, "PageCount", "TotalCount");
        }
    }
}
