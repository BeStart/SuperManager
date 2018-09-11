using Helper.Core.Library;
using SuperManager.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.DAL
{
    public class MenuDAL
    {
        public bool Operater(DBMenuModel model)
        {
            if(model != null)
            {
                model.ActionList = StringHelper.PadChar(model.ActionList, ",");
            }
            if (model.IdentityID == 0)
            {
                string commandText = "insert into T_Menu(ParentID, MenuIcon, MenuName, MenuUrl, BelongModule, ActionList, MenuSort, MenuStatus)values(@ParentID, @MenuIcon, @MenuName, @MenuUrl, @BelongModule, @ActionList, @MenuSort, @MenuStatus)";
                return DataBaseHelper.ExecuteNonQuery(commandText, new { ParentID = model.ParentID, MenuIcon = model.MenuIcon, MenuName = model.MenuName, MenuUrl = model.MenuUrl, BelongModule = model.BelongModule, ActionList = model.ActionList, MenuSort = model.MenuSort, MenuStatus = model.MenuStatus }) > 0;
            }
            else
            {
                string commandText = "update T_Menu set ParentID=@ParentID, MenuIcon=@MenuIcon, MenuName=@MenuName, MenuUrl=@MenuUrl, BelongModule=@BelongModule, ActionList=@ActionList, MenuSort=@MenuSort, MenuStatus=@MenuStatus where IdentityID=@IdentityID";
                return DataBaseHelper.ExecuteNonQuery(commandText, new { ParentID = model.ParentID, MenuIcon = model.MenuIcon, MenuName = model.MenuName, MenuUrl = model.MenuUrl, BelongModule = model.BelongModule, ActionList = model.ActionList, MenuSort = model.MenuSort, MenuStatus = model.MenuStatus, IdentityID = model.IdentityID }) > 0;
            }
        }

        public bool Delete(int identityID)
        {
            string commandText = "delete from T_Menu where IdentityID=@IdentityID";
            return DataBaseHelper.ExecuteNonQuery(commandText, new { IdentityID = identityID }) > 0;
        }

        public bool DeleteMore(string identityIDList)
        {
            identityIDList = StringHelper.TrimChar(identityIDList, ",");

            string commandText = "delete from T_Menu where IdentityID in (@IdentityIDList)";
            commandText = commandText.Replace("@IdentityIDList", identityIDList);

            return DataBaseHelper.ExecuteNonQuery(commandText) > 0;
        }

        public DBMenuModel Select(int identityID)
        {
            string commandText = "select IdentityID, ParentID, MenuIcon, MenuName, MenuUrl, BelongModule, ActionList, MenuSort,MenuStatus from T_Menu with(nolock) where IdentityID=@IdentityID";
            return DataBaseHelper.ToEntity<DBMenuModel>(commandText, new { IdentityID = identityID });
        }

        public List<DBMenuModel> AuthList()
        {
            string commandText = "select MenuUrl, BelongModule, ActionList from T_Menu with(nolock)";
            return DataBaseHelper.ToEntityList<DBMenuModel>(commandText);
        }

        public List<DBMenuModel> RoleList(int menuStatus)
        {
            string commandText = "select IdentityID, ParentID, MenuName, MenuUrl, MenuIcon, MenuSort from T_Menu with(nolock) where MenuStatus=@MenuStatus";
            return DataBaseHelper.ToEntityList<DBMenuModel>(commandText, new { MenuStatus = menuStatus });
        }

        public List<DBMenuModel> List(string identityIDList, int menuStatus = -1)
        {
            if (string.IsNullOrEmpty(identityIDList)) identityIDList = "0";

            identityIDList = StringHelper.TrimChar(identityIDList, ",");
            StringBuilder stringBuilder = new StringBuilder();
            if (menuStatus != -1)
            {
                stringBuilder.Append(" and MenuStatus = ");
                stringBuilder.Append(menuStatus);
            }
            string commandText = string.Format("select IdentityID, ParentID, MenuName, MenuUrl, BelongModule, ActionList, MenuSort,MenuIcon from T_Menu with(nolock) where IdentityID in ({0}) {1} order by MenuSort desc", identityIDList, stringBuilder.ToString());
            return DataBaseHelper.ToEntityList<DBMenuModel>(commandText);
        }

        public List<ViewTreeMenuModel> All(string searchKey)
        {
            if (string.IsNullOrEmpty(searchKey))
            {
                string commandText = "select IdentityID, ParentID, MenuName, MenuUrl, MenuIcon, BelongModule, ActionList, MenuSort, MenuStatus from T_Menu with(nolock) order by MenuSort desc";
                return DataBaseHelper.ToEntityList<ViewTreeMenuModel>(commandText, null);
            }
            else
            {
                string commandText = "select IdentityID, 0 as ParentID, MenuName, MenuUrl, MenuIcon, BelongModule, ActionList, MenuSort, MenuStatus from T_Menu with(nolock) where MenuName like '%{0}%' order by MenuSort desc";
                commandText = string.Format(commandText, searchKey);
                return DataBaseHelper.ToEntityList<ViewTreeMenuModel>(commandText);
            }
        }
    }
}
