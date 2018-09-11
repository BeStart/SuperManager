using Helper.Core.Library;
using SuperManager.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.DAL
{
    public class UserLogDAL
    {
        public bool Operater(DBUserLogModel model)
        {
            string commandText = "insert into T_UserLog(UserCode, LoginIP, LoginStatus)values(@UserCode, @LoginIP, @LoginStatus)";
            return DataBaseHelper.ExecuteNonQuery(commandText, new { UserCode = model.UserCode, LoginIP = model.LoginIP, LoginStatus = model.LoginStatus }) > 0;
        }
        public bool Delete(int identityID)
        {
            string commandText = "delete from T_UserLog where IdentityID=@IdentityID";
            return DataBaseHelper.ExecuteNonQuery(commandText, new { IdentityID = identityID }) > 0;
        }

        public bool DeleteMore(string identityIDList)
        {
            identityIDList = StringHelper.TrimChar(identityIDList, ",");

            string commandText = "delete from T_UserLog where IdentityID in (@IdentityIDList)";
            commandText = commandText.Replace("@IdentityIDList", identityIDList);

            return DataBaseHelper.ExecuteNonQuery(commandText) > 0;
        }
        public List<DBUserLogModel> Page(string searchKey, int loginStatus, int pageIndex, int pageSize, ref int totalCount, ref int pageCount)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (!string.IsNullOrEmpty(searchKey))
            {
                stringBuilder.Append(string.Format(" (UserCode like '%{0}%' ", searchKey));
                stringBuilder.Append(" or ");
                stringBuilder.Append(string.Format(" LoginIP like '%{0}%' ", searchKey));
                stringBuilder.Append(") and ");
            }
            if(loginStatus != -1)
            {
                stringBuilder.Append(" LoginStatus = ");
                stringBuilder.Append(loginStatus);
                stringBuilder.Append(" and ");
            }

            string whereSql = stringBuilder.ToString().TrimEnd().TrimEnd(new char[] { 'a', 'n', 'd' });

            Dictionary<string, object> parameterList = new Dictionary<string, object>();
            parameterList.Add("@FieldSql", "IdentityID, UserCode, LoginIP, LoginDate, LoginStatus");
            parameterList.Add("@Field", "");
            parameterList.Add("@TableName", "T_UserLog");
            parameterList.Add("@PrimaryKey", "IdentityID");
            parameterList.Add("@PageIndex", pageIndex);
            parameterList.Add("@PageSize", pageSize);
            parameterList.Add("@WhereSql", whereSql);
            parameterList.Add("@OrderSql", "IdentityID asc");

            return DataBaseHelper.ToEntityList<DBUserLogModel>("", parameterList, ref pageCount, ref totalCount, null, "PageCount", "TotalCount");
        }
    }
}
