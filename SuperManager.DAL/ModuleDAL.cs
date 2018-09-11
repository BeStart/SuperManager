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
        public bool Operater(DBModuleModel model)
        {
            if (model != null)
            {
                model.ActionList = StringHelper.PadChar(model.ActionList, ",");
            }
            if (model.IdentityID == 0)
            {
                string commandText = "insert into T_Module(ModuleCode, ModuleName, ActionList)values(@ModuleCode, @ModuleName, @ActionList)";
                return DataBaseHelper.ExecuteNonQuery(commandText, new { ModuleCode = model.ModuleCode, ModuleName = model.ModuleName, ActionList = model.ActionList }) > 0;
            }
            else
            {
                string commandText = "update T_Module set ModuleCode=@ModuleCode, ModuleName=@ModuleName, ActionList=@ActionList where IdentityID=@IdentityID";
                return DataBaseHelper.ExecuteNonQuery(commandText, new { ModuleCode = model.ModuleCode, ModuleName = model.ModuleName, ActionList = model.ActionList, IdentityID = model.IdentityID }) > 0;
            }
        }

        public bool Exists(string moduleCode, int identityID)
        {
            string commandText = "select IdentityID from T_Module with(nolock) where ModuleCode=@ModuleCode";
            int result = DataBaseHelper.ExecuteScalar<int>(commandText, new { ModuleCode = moduleCode });

            if (identityID == 0) return result > 0;
            return result == 0 ? false : (result != identityID);
        }

        public bool Delete(int identityID)
        {
            string commandText = "delete from T_Module where IdentityID=@IdentityID";
            return DataBaseHelper.ExecuteNonQuery(commandText, new { IdentityID = identityID }) > 0;
        }

        public bool DeleteMore(string identityIDList)
        {
            identityIDList = StringHelper.TrimChar(identityIDList, ",");

            string commandText = "delete from T_Module where IdentityID in (@IdentityIDList)";
            commandText = commandText.Replace("@IdentityIDList", identityIDList);

            return DataBaseHelper.ExecuteNonQuery(commandText) > 0;
        }

        public DBModuleModel Select(int identityID)
        {
            string commandText = "select IdentityID, ModuleCode, ModuleName, ActionList from T_Module with(nolock) where IdentityID=@IdentityID";
            return DataBaseHelper.ToEntity<DBModuleModel>(commandText, new { IdentityID = identityID });
        }
        public DBModuleModel Select(string moduleCode)
        {
            string commandText = "select IdentityID, ModuleCode, ModuleName, ActionList from T_Module with(nolock) where ModuleCode=@ModuleCode";
            return DataBaseHelper.ToEntity<DBModuleModel>(commandText, new { ModuleCode = moduleCode });
        }

        public List<DBModuleModel> List()
        {
            string commandText = "select IdentityID, ModuleCode, ModuleName from T_Module with(nolock)";
            return DataBaseHelper.ToEntityList<DBModuleModel>(commandText);
        }

        public List<DBModuleModel> All(string searchKey)
        {
            string commandText = "select IdentityID, ModuleCode, ModuleName, ActionList from T_Module with(nolock) {0}";
            StringBuilder stringBuilder = new StringBuilder();
            if(!string.IsNullOrEmpty(searchKey))
            {
                stringBuilder.Append(" where ModuleCode like '%");
                stringBuilder.Append(searchKey);
                stringBuilder.Append("%' or ModuleName like '%");
                stringBuilder.Append(searchKey);
                stringBuilder.Append("%' ");
            }
            commandText = string.Format(commandText, stringBuilder.ToString());
            return DataBaseHelper.ToEntityList<DBModuleModel>(commandText);
        }
    }
}
