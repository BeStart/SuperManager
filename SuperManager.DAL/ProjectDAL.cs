using Helper.Core.Library;
using SuperManager.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.DAL
{
    public class ProjectDAL
    {
        public bool Operater(DBProjectModel model, int flowStepID = 0)
        {
            if (model.IdentityID == 0)
            {
                string commandText = "insert into T_Project(ProjectType, ProjectName, FlowID, FlowStepID)values(@ProjectType, @ProjectName, @FlowID, @FlowStepID)";
                return DataBaseHelper.ExecuteNonQuery(commandText, new { ProjectType = model.ProjectType, ProjectName = model.ProjectName, FlowID = model.FlowID, FlowStepID = model.FlowStepID }) > 0;
            }
            else
            {
                string commandText = "update T_Project set ProjectType = @ProjectType, ProjectName = @ProjectName, FlowID=@FlowID, FlowStepID=@FlowStepID where IdentityID=@IdentityID";
                return DataBaseHelper.ExecuteNonQuery(commandText, new { ProjectType = model.ProjectType, ProjectName = model.ProjectName, FlowID = model.FlowID, FlowStepID = model.FlowStepID, IdentityID = model.IdentityID }) > 0;
            }
        }

        public bool Delete(int identityID)
        {
            string commandText = "delete from T_Project where IdentityID=@IdentityID";
            return DataBaseHelper.ExecuteNonQuery(commandText, new { IdentityID = identityID }) > 0;
        }

        public bool DeleteMore(string identityIDList)
        {
            identityIDList = StringHelper.TrimChar(identityIDList, ",");

            string commandText = "delete from T_Project where IdentityID in (@IdentityIDList)";
            commandText = commandText.Replace("@IdentityIDList", identityIDList);

            return DataBaseHelper.ExecuteNonQuery(commandText) > 0;
        }

        public DBProjectModel Select(int identityID)
        {
            string commandText = "select IdentityID, ProjectType, ProjectName, FlowID, FlowStepID from T_Project with(nolock) where IdentityID=@IdentityID";
            return DataBaseHelper.ToEntity<DBProjectModel>(commandText, new { IdentityID = identityID });
        }

        public List<DBProjectFullModel> Page(string searchKey, int projectType, int flowID, int pageIndex, int pageSize, ref int totalCount, ref int pageCount)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (!string.IsNullOrEmpty(searchKey))
            {
                stringBuilder.Append(string.Format(" ProjectName like '%{0}%' ", searchKey));
                stringBuilder.Append(" and ");
            }
            if (projectType > 0)
            {
                stringBuilder.Append(" ProjectType = ");
                stringBuilder.Append(projectType);
                stringBuilder.Append(" and ");
            }
            if (flowID > 0)
            {
                stringBuilder.Append(" FlowID = ");
                stringBuilder.Append(flowID);
                stringBuilder.Append(" and ");
            }

            string whereSql = stringBuilder.ToString().TrimEnd().TrimEnd(new char[] { 'a', 'n', 'd' });

            Dictionary<string, object> parameterList = new Dictionary<string, object>();
            parameterList.Add("@FieldSql", "IdentityID, ProjectName, ProjectType, FlowStepID, (select TypeName from T_ProjectType with(nolock) where T_ProjectType.IdentityID=T.ProjectType) as ProjectTypeName, (select FlowName from T_Flow with(nolock) where T_Flow.IdentityID=T.FlowID) as FlowName, (select StepName from T_FlowStep with(nolock) where T_FlowStep.IdentityID = T.FlowStepID) as FlowStepName");
            parameterList.Add("@Field", "IdentityID, ProjectName, ProjectType, FlowID, FlowStepID");
            parameterList.Add("@TableName", "T_Project");
            parameterList.Add("@PrimaryKey", "IdentityID");
            parameterList.Add("@PageIndex", pageIndex);
            parameterList.Add("@PageSize", pageSize);
            parameterList.Add("@WhereSql", whereSql);
            parameterList.Add("@OrderSql", "IdentityID asc");

            return DataBaseHelper.ToEntityList<DBProjectFullModel>("", parameterList, ref pageCount, ref totalCount, null, "PageCount", "TotalCount");
        }
    }
}
