using Helper.Core.Library;
using SuperManager.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.DAL
{
    public class ModuleDAL
    {
        private const string TABLE_NAME = "T_Module";

        public bool Operater(DBModuleModel model)
        {
            model.ActionList = StringHelper.PadChar(model.ActionList, ",");

            if (model.IdentityID == 0)
            {
                return DataBaseHelper.Insert<DBModuleModel>(model, p => p.IdentityID, TABLE_NAME);
            }
            else
            {
                return DataBaseHelper.Update<DBModuleModel>(model, p => p.IdentityID == p.IdentityID, p => p.IdentityID, TABLE_NAME);
            }
        }
        public bool Exists(string moduleCode, int identityID)
        {
            return DataBaseHelper.Exists<DBModuleModel>(new { ModuleCode = moduleCode }, p => p.IdentityID, p => p.ModuleCode == p.ModuleCode, identityID, TABLE_NAME);
        }
        public bool Delete(int identityID)
        {
            return DataBaseHelper.Delete<DBModuleModel>(new { IdentityID = identityID }, p => p.IdentityID == p.IdentityID, TABLE_NAME);
        }
        public bool DeleteMore(string identityIDList)
        {
            List<int> dataList = StringHelper.ToList<int>(identityIDList, ",");
            return DataBaseHelper.Delete<DBModuleModel>(null, p => dataList.Contains(p.IdentityID), TABLE_NAME);
        }
        public DBModuleModel Select(int identityID)
        {
            return DataBaseHelper.Single<DBModuleModel>(new { IdentityID = identityID }, p => new { p.IdentityID, p.ModuleCode, p.ModuleName, p.ActionList }, p => p.IdentityID == p.IdentityID, TABLE_NAME);
        }
        public DBModuleModel Select(string moduleCode)
        {
            return DataBaseHelper.Single<DBModuleModel>(new { ModuleCode = moduleCode }, p => new { p.IdentityID, p.ModuleCode, p.ModuleName, p.ActionList }, p => p.ModuleCode == p.ModuleCode, TABLE_NAME);
        }
        public List<DBModuleModel> List()
        {
            return DataBaseHelper.More<DBModuleModel>(null, p => new { p.IdentityID, p.ModuleCode, p.ModuleName, p.ActionList }, null, null, true, TABLE_NAME);
        }

        public List<DBModuleModel> Page(string searchKey, int pageIndex, int pageSize, ref int totalCount, ref int pageCount)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if(!string.IsNullOrEmpty(searchKey))
            {
                stringBuilder.Append(" ModuleCode like '%");
                stringBuilder.Append(searchKey);
                stringBuilder.Append("%' or ModuleName like '%");
                stringBuilder.Append(searchKey);
                stringBuilder.Append("%' ");
            }

            string whereSql = stringBuilder.ToString().TrimEnd().TrimEnd(new char[] { 'a', 'n', 'd' });

            Dictionary<string, object> parameterList = new Dictionary<string, object>();
            parameterList.Add("@FieldSql", "IdentityID, ModuleCode, ModuleName, ActionList");
            parameterList.Add("@Field", "");
            parameterList.Add("@TableName", "T_Module");
            parameterList.Add("@PrimaryKey", "IdentityID");
            parameterList.Add("@PageIndex", pageIndex);
            parameterList.Add("@PageSize", pageSize);
            parameterList.Add("@WhereSql", whereSql);
            parameterList.Add("@OrderSql", "IdentityID desc");

            return DataBaseHelper.ToEntityList<DBModuleModel>("", parameterList, ref pageCount, ref totalCount, null, "PageCount", "TotalCount");
        }
    }
}
