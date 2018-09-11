using Helper.Core.Library;
using SuperManager.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.DAL
{
    public class MessageDAL
    {
        public bool Operater(DBMessageModel model)
        {
            string commandText = "insert into T_Message(ContactName, ContactTelphone, ContactEmail, MessageContent, ContactIP)values(@ContactName, @ContactTelphone, @ContactEmail, @MessageContent, @ContactIP)";
            return DataBaseHelper.ExecuteNonQuery(commandText, new { ContactName = model.ContactName, ContactTelphone = model.ContactTelphone, ContactEmail = model.ContactEmail, MessageContent = model.MessageContent, ContactIP = model.ContactIP }) > 0;
        }

        public bool Delete(int identityID)
        {
            string commandText = "delete from T_Message where IdentityID=@IdentityID";
            return DataBaseHelper.ExecuteNonQuery(commandText, new { IdentityID = identityID }) > 0;
        }

        public bool DeleteMore(string identityIDList)
        {
            identityIDList = StringHelper.TrimChar(identityIDList, ",");

            string commandText = "delete from T_Message where IdentityID in (@IdentityIDList)";
            commandText = commandText.Replace("@IdentityIDList", identityIDList);

            return DataBaseHelper.ExecuteNonQuery(commandText) > 0;
        }

        public bool Status(int identityID, int messageStatus)
        {
            string commandText = "update T_Message set MessageStatus=@MessageStatus where IdentityID=@IdentityID";
            return DataBaseHelper.ExecuteNonQuery(commandText, new { IdentityID = identityID, MessageStatus = messageStatus }) > 0;
        }

        public DBMessageFullModel FullSelect(int identityID)
        {
            string commandText = "select T_Message.IdentityID, ContactName, ContactTelphone, ContactEmail, MessageContent, ContactIP, MessageDate, MessageStatus, T_MessageReply.ReplyContent,T_MessageReply.UserCode,T_MessageReply.NickName,T_MessageReply.ReplyDate from T_Message with(nolock) left join T_MessageReply with(nolock) on T_Message.IdentityID=T_MessageReply.MessageID where T_Message.IdentityID=@IdentityID";
            return DataBaseHelper.ToEntity<DBMessageFullModel>(commandText, new { IdentityID = identityID });
        }

        public List<DBMessageModel> Page(string searchKey, int messageStatus, int pageIndex, int pageSize, ref int totalCount, ref int pageCount)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (!string.IsNullOrEmpty(searchKey))
            {
                stringBuilder.Append(string.Format(" (ContactName like '%{0}%' ", searchKey));
                stringBuilder.Append(string.Format(" or ContactTelphone like '%{0}%' ", searchKey));
                stringBuilder.Append(string.Format(" or ContactEmail like '%{0}%' ", searchKey));
                stringBuilder.Append(string.Format(" or ContactIP like '%{0}%' ", searchKey));
                stringBuilder.Append(") and ");
            }
            if (messageStatus != -1)
            {
                stringBuilder.Append(" MessageStatus = ");
                stringBuilder.Append(messageStatus);
                stringBuilder.Append(" and ");
            }

            string whereSql = stringBuilder.ToString().TrimEnd().TrimEnd(new char[] { 'a', 'n', 'd' });

            Dictionary<string, object> parameterList = new Dictionary<string, object>();
            parameterList.Add("@FieldSql", "IdentityID, ContactName, ContactTelphone, ContactEmail, MessageContent, ContactIP, MessageDate, MessageStatus");
            parameterList.Add("@Field", "");
            parameterList.Add("@TableName", "T_Message");
            parameterList.Add("@PrimaryKey", "IdentityID");
            parameterList.Add("@PageIndex", pageIndex);
            parameterList.Add("@PageSize", pageSize);
            parameterList.Add("@WhereSql", whereSql);
            parameterList.Add("@OrderSql", "IdentityID asc");

            return DataBaseHelper.ToEntityList<DBMessageModel>("", parameterList, ref pageCount, ref totalCount, null, "PageCount", "TotalCount");
        }
    }
}
