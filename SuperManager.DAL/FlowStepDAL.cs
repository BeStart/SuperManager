using Helper.Core.Library;
using SuperManager.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.DAL
{
    public class FlowStepDAL
    {
        public List<DBFlowStepModel> List(int flowID)
        {
            string commandText = "select IdentityID, FlowID, StepCode, StepSymbol, StepName, StepAddrName, RoleList, StepList, NextStep, PositionTop, PositionLeft from T_FlowStep with(nolock) where FlowID=@FlowID";
            return DataBaseHelper.ToEntityList<DBFlowStepModel>(commandText, new { FlowID = flowID });
        }

        public List<DBFlowStepModel> List()
        {
            string commandText = "select IdentityID, StepCode, StepName from T_FlowStep with(nolock)";
            return DataBaseHelper.ToEntityList<DBFlowStepModel>(commandText);
        }

        public bool Operater(int flowID, List<DBFlowStepModel> modelList)
        {
            List<DataBaseTransactionItem> transactionItemList = new List<DataBaseTransactionItem>();
            if (modelList != null && modelList.Count > 0)
            {
                foreach (DBFlowStepModel model in modelList)
                {
                    transactionItemList.Add(new DataBaseTransactionItem()
                    {
                        CommandText = "update T_FlowStep set RoleList=@RoleList where IdentityID=@IdentityID",
                        ExecuteType = DataBaseExecuteTypeEnum.ExecuteNonQuery,
                        ParameterList = new { RoleList = StringHelper.PadChar(model.RoleList, ","), IdentityID = model.IdentityID }
                    });
                }
            }
            return DataBaseHelper.TransactionNonQuery(transactionItemList) > 0;
        }
    }
}
