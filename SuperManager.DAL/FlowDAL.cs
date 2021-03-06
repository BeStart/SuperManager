﻿using Helper.Core.Library;
using SuperManager.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.DAL
{
    public class FlowDAL
    {
        private const string TABLE_NAME = "T_Flow";

        public bool Operater(DBFlowModel model, List<DBFlowStepModel> stepModelList)
        {
            List<DataBaseTransactionItem> transactionItemList = new List<DataBaseTransactionItem>();
            if (model.IdentityID == 0)
            {
                transactionItemList.Add(new DataBaseTransactionItem()
                {
                    CommandText = "insert into T_Flow(FlowType, FlowName)values(@FlowType, @FlowName);select SCOPE_IDENTITY();",
                    ExecuteType = DataBaseExecuteTypeEnum.ExecuteScalar,
                    ParameterList = new { FlowType = model.FlowType, FlowName = model.FlowName },
                    OutputName = "FlowID"
                });
            }
            else
            {
                transactionItemList.Add(new DataBaseTransactionItem()
                {
                    CommandText = "update T_Flow set FlowType=@FlowType, FlowName=@FlowName where IdentityID=@IdentityID",
                    ExecuteType = DataBaseExecuteTypeEnum.ExecuteNonQuery,
                    ParameterList = new { FlowType = model.FlowType, FlowName = model.FlowName, IdentityID = model.IdentityID },
                });
                transactionItemList.Add(new DataBaseTransactionItem()
                {
                    CommandText = "delete from T_FlowStep where FlowID=@FlowID",
                    ExecuteType = DataBaseExecuteTypeEnum.ExecuteNonQuery,
                    ParameterList = new { FlowID = model.IdentityID }
                });
            }
            if (stepModelList != null && stepModelList.Count > 0)
            {
                foreach (DBFlowStepModel stepModel in stepModelList)
                {
                    transactionItemList.Add(new DataBaseTransactionItem()
                    {
                        CommandText = "insert into T_FlowStep(FlowID, StepCode,StepSymbol,StepName,StepAddrName,RoleList,StepList,NextStep,PositionTop,PositionLeft)values(@FlowID, @StepCode,@StepSymbol,@StepName,@StepAddrName,@RoleList,@StepList,@NextStep,@PositionTop,@PositionLeft)",
                        ExecuteType = DataBaseExecuteTypeEnum.ExecuteNonQuery,
                        ParameterList = new { FlowID = model.IdentityID, StepCode = stepModel.StepCode, StepSymbol = stepModel.StepSymbol, StepName = stepModel.StepName, StepAddrName = stepModel.StepAddrName, RoleList = StringHelper.PadChar(stepModel.RoleList, ","), StepList = StringHelper.PadChar(stepModel.StepList, ","), NextStep = stepModel.NextStep, PositionTop = stepModel.PositionTop, PositionLeft = stepModel.PositionLeft },
                        InputList = model.IdentityID == 0 ? new string[] { "FlowID" } : null
                    });
                }
            }
            return DataBaseHelper.TransactionNonQuery(transactionItemList) > 0;
        }
        public bool Edit(DBFlowModel model)
        {
            return DataBaseHelper.Update<DBFlowModel>(new { model.FlowType, model.FlowName, model.IdentityID }, p => p.IdentityID == p.IdentityID, p => p.IdentityID, TABLE_NAME);
        }
        public bool Delete(int identityID)
        {
            return (bool)DataBaseHelper.Transaction((con, transaction) =>
            {
                DataBaseHelper.TransactionDelete<DBFlowStepModel>(con, transaction, new { FlowID = identityID }, p => p.FlowID == p.FlowID, "T_FlowStep");
                return DataBaseHelper.TransactionDelete<DBFlowModel>(con, transaction, new { IdentityID = identityID }, p => p.IdentityID == p.IdentityID, TABLE_NAME);
            });
        }
        public bool DeleteMore(string identityIDList)
        {
            List<int> dataList = StringHelper.ToList<int>(identityIDList, ",");
            return (bool)DataBaseHelper.Transaction((con, transaction) =>
            {
                DataBaseHelper.Delete<DBFlowStepModel>(null, p => dataList.Contains(p.FlowID), "T_FlowStep");
                return DataBaseHelper.Delete<DBFlowModel>(null, p => dataList.Contains(p.IdentityID), TABLE_NAME);
            });
        }
        public DBFlowModel Select(int identityID)
        {
            return DataBaseHelper.Single<DBFlowModel>(new { IdentityID = identityID }, p => new { p.IdentityID, p.FlowType, p.FlowName }, p => p.IdentityID == p.IdentityID, TABLE_NAME);
        }
        public List<DBFlowModel> List()
        {
            return DataBaseHelper.More<DBFlowModel>(null, p => new { p.IdentityID, p.FlowName }, null, null, true, TABLE_NAME);
        }

        public List<DBFlowFullModel> Page(string searchKey, int flowType, int pageIndex, int pageSize, ref int totalCount, ref int pageCount)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (!string.IsNullOrEmpty(searchKey))
            {
                stringBuilder.Append(string.Format(" FlowName like '%{0}%' ", searchKey));
                stringBuilder.Append(" and ");
            }
            if (flowType > 0)
            {
                stringBuilder.Append(" FlowType = ");
                stringBuilder.Append(flowType);
                stringBuilder.Append(" and ");
            }

            string whereSql = stringBuilder.ToString().TrimEnd().TrimEnd(new char[] { 'a', 'n', 'd' });

            Dictionary<string, object> parameterList = new Dictionary<string, object>();
            parameterList.Add("@FieldSql", "IdentityID, FlowType, FlowName, (select TypeName from T_FlowType with(nolock) where T_FlowType.IdentityID=T.FlowType) as FlowTypeName");
            parameterList.Add("@Field", "IdentityID, FlowType, FlowName");
            parameterList.Add("@TableName", "T_Flow");
            parameterList.Add("@PrimaryKey", "IdentityID");
            parameterList.Add("@PageIndex", pageIndex);
            parameterList.Add("@PageSize", pageSize);
            parameterList.Add("@WhereSql", whereSql);
            parameterList.Add("@OrderSql", "IdentityID asc");

            return DataBaseHelper.ToEntityList<DBFlowFullModel>("", parameterList, ref pageCount, ref totalCount, null, "PageCount", "TotalCount");
        }
    }
}
