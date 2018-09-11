using Helper.Core.Library;
using SuperManager.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.DAL
{
    public class AttachmentDAL
    {
        public bool Operater(DBAttachmentModel model)
        {
            string commandText = "insert into T_Attachment(AttachmentType,AttachmentName,AttachmentSize,AttachmentPath)values(@AttachmentType,@AttachmentName,@AttachmentSize,@AttachmentPath)";
            return DataBaseHelper.ExecuteNonQuery(commandText, new { AttachmentType = model.AttachmentType, AttachmentName = model.AttachmentName, AttachmentSize = model.AttachmentSize, AttachmentPath = model.AttachmentPath }) > 0;
        }

        public bool Delete(int identityID)
        {
            string commandText = "delete from T_Attachment where IdentityID=@IdentityID";
            return DataBaseHelper.ExecuteNonQuery(commandText, new { IdentityID = identityID }) > 0;
        }

        public bool DeleteMore(string identityIDList)
        {
            identityIDList = StringHelper.TrimChar(identityIDList, ",");

            string commandText = "delete from T_Attachment where IdentityID in (@IdentityIDList)";
            commandText = commandText.Replace("@IdentityIDList", identityIDList);

            return DataBaseHelper.ExecuteNonQuery(commandText) > 0;
        }

        public List<DBAttachmentModel> List(string identityIDList)
        {
            identityIDList = StringHelper.TrimChar(identityIDList);

            string commandText = string.Format("select IdentityID,AttachmentType,AttachmentName,AttachmentSize,AttachmentPath from T_Attachment with(nolock) where IdentityID in ({0})", identityIDList);
            return DataBaseHelper.ToEntityList<DBAttachmentModel>(commandText);
        }

        public List<DBAttachmentModel> Page(string searchKey, string attachmentType, int pageIndex, int pageSize, ref int totalCount, ref int pageCount)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (!string.IsNullOrEmpty(searchKey))
            {
                stringBuilder.Append(string.Format(" AttachmentName like '%{0}%' ", searchKey));
                stringBuilder.Append(" and ");
            }
            if (!string.IsNullOrEmpty(attachmentType))
            {
                stringBuilder.Append(" AttachmentType = '");
                stringBuilder.Append(attachmentType);
                stringBuilder.Append("' and ");
            }

            string whereSql = stringBuilder.ToString().TrimEnd().TrimEnd(new char[] { 'a', 'n', 'd' });

            Dictionary<string, object> parameterList = new Dictionary<string, object>();
            parameterList.Add("@FieldSql", "IdentityID, AttachmentType,AttachmentName,AttachmentSize,AttachmentPath,AttachmentDate");
            parameterList.Add("@Field", "");
            parameterList.Add("@TableName", "T_Attachment");
            parameterList.Add("@PrimaryKey", "IdentityID");
            parameterList.Add("@PageIndex", pageIndex);
            parameterList.Add("@PageSize", pageSize);
            parameterList.Add("@WhereSql", whereSql);
            parameterList.Add("@OrderSql", "IdentityID asc");

            return DataBaseHelper.ToEntityList<DBAttachmentModel>("", parameterList, ref pageCount, ref totalCount, null, "PageCount", "TotalCount");
        }
    }
}
