using Helper.Core.Library;
using SuperManager.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.DAL
{
    public class MessageReplyDAL
    {
        public bool Operater(DBMessageReplyModel model, int messageStatus)
        {
            List<DataBaseTransactionItem> transactionItemList = new List<DataBaseTransactionItem>()
            {
                new DataBaseTransactionItem(){
                    CommandText = "update T_Message set MessageStatus = @MessageStatus where IdentityID=@IdentityID",
                    ExecuteType = DataBaseExecuteTypeEnum.ExecuteNonQuery,
                    ParameterList = new { IdentityID = model.MessageID, MessageStatus = messageStatus }
                },
                new DataBaseTransactionItem(){
                    CommandText = "insert into T_MessageReply(MessageID, ReplyContent, UserCode, NickName)values(@MessageID, @ReplyContent, @UserCode, @NickName)",
                    ExecuteType = DataBaseExecuteTypeEnum.ExecuteNonQuery,
                    ParameterList = new { MessageID = model.MessageID, ReplyContent = model.ReplyContent, UserCode = model.UserCode, NickName = model.NickName }
                }
            };
            return DataBaseHelper.TransactionNonQuery(transactionItemList) > 0;
        }
    }
}
