using Helper.Core.Library;
using SuperManager.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.DAL
{
    public class InitDAL
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="userCode">用户名</param>
        /// <param name="nickName">登录名</param>
        /// <param name="userPassword">密码</param>
        /// <returns></returns>
        public bool Init(string userCode, string nickName, string userPassword)
        {
            int count = DataBaseHelper.ExecuteScalar<int>("select count(1) from T_User with(nolock)");
            if (count > 0) return false;

            return (bool)DataBaseHelper.Transaction((con, transaction) =>
            {
                #region 添加角色信息
                // 向角色表添加数据
                int roleID = DataBaseHelper.TransactionScalar<int>(con, transaction, "insert into T_Role(RoleName, MenuList, ActionList)values(@RoleName, @MenuList, @ActionList);select SCOPE_IDENTITY();", new DBRoleModel() { RoleName = "超级管理员", ActionList = "", MenuList = "" });
                #endregion

                #region 添加用户信息
                // 向用户添加数据
                bool result = DataBaseHelper.TransactionNonQuery(con, transaction, "insert into T_User(UserCode, NickName, UserPassword, RoleID)values(@UserCode, @NickName, @UserPassword, @RoleID)", new { UserCode = userCode, NickName = nickName, UserPassword = userPassword, RoleID = roleID }) > 0;
                #endregion

                #region 添加权限数据
                // 添加 ActionList
                List<DBActionTypeModel> actionTypeModelList = new List<DBActionTypeModel>()
                {
                    new DBActionTypeModel() { TypeCode = "Add", TypeName = "添加", TypeSort = 0 },
                    new DBActionTypeModel() { TypeCode = "Check", TypeName = "审核", TypeSort = 0 },
                    new DBActionTypeModel() { TypeCode = "Delete", TypeName = "删除", TypeSort = 0 },
                    new DBActionTypeModel() { TypeCode = "Edit", TypeName = "编辑", TypeSort = 0 },
                    new DBActionTypeModel() { TypeCode = "EditInfo", TypeName = "编辑个人信息", TypeSort = 0 },
                    new DBActionTypeModel() { TypeCode = "EditPassword", TypeName = "修改密码", TypeSort = 0 },
                    new DBActionTypeModel() { TypeCode = "FlowAuth", TypeName = "步骤授权", TypeSort = 0 },
                    new DBActionTypeModel() { TypeCode = "FlowDesignAdd", TypeName = "步骤添加", TypeSort = 0 },
                    new DBActionTypeModel() { TypeCode = "FlowDesignEdit", TypeName = "步骤编辑", TypeSort = 0 },
                    new DBActionTypeModel() { TypeCode = "Info", TypeName = "查看详情", TypeSort = 0 },
                    new DBActionTypeModel() { TypeCode = "Reply", TypeName = "回复", TypeSort = 0 },
                    new DBActionTypeModel() { TypeCode = "Upload", TypeName = "上传", TypeSort = 0 },
                };
                DataBaseHelper.TransactionEntityListBatchImport<DBActionTypeModel>(con, transaction, "T_ActionType", actionTypeModelList);
                #endregion

                #region 添加模块数据

                Dictionary<string, string> moduleDict = new Dictionary<string, string>()
                {
                    { "Topic", ",Add,Check,Delete,Edit,Upload," },
                    { "Module", ",Add,Delete,Edit," },
                    { "ActionType", ",Add,Delete,Edit," },
                    { "TopicPositionType", ",Add,Delete,Edit," },
                    { "TopicType", ",Add,Delete,Edit," },
                    { "Role", ",Add,Delete,Edit," },
                    { "Menu", ",Add,Delete,Edit," },
                    { "User", ",Add,Delete,Edit,EditInfo,EditPassword," },
                    { "FlowSymbolType", ",Add,Delete,Edit," },
                    { "FlowType", ",Add,Delete,Edit," },
                    { "Flow", ",Add,Delete,Edit,FlowAuth,FlowDesignAdd,FlowDesignEdit," },
                    { "ProjectType", ",Add,Delete,Edit," },
                    { "Project", ",Add,Delete,Edit," },
                    { "VoteType", ",Add,Delete,Edit," },
                    { "Vote", ",Add,Delete,Edit," },
                    { "UserLog", ",Delete," },
                    { "Message", ",Delete,Info,Reply,Upload," },
                    { "Attachment", ",Delete," },
                    { "LinkFriendType", ",Add,Delete,Edit," },
                    { "LinkFriend", ",Add,Delete,Edit," },
                    { "IndexMapper", ",Add,Delete,Edit," },
                    { "NoticeType", ",Add,Delete,Edit," },
                    { "Notice", ",Add,Delete,Edit,Upload," }
                };

                // 添加 ModuleList
                List<DBModuleModel> moduleModelList = new List<DBModuleModel>()
                {
                    new DBModuleModel() { ModuleCode = "Topic", ModuleName = "主题", ActionList = moduleDict["Topic"] },
                    new DBModuleModel() { ModuleCode = "Module", ModuleName = "模块", ActionList = moduleDict["Module"] },
                    new DBModuleModel() { ModuleCode = "ActionType", ModuleName = "模块动作", ActionList = moduleDict["ActionType"] },
                    new DBModuleModel() { ModuleCode = "TopicPositionType", ModuleName = "主题位置类别", ActionList = moduleDict["TopicPositionType"] },
                    new DBModuleModel() { ModuleCode = "TopicType", ModuleName = "主题类别", ActionList = moduleDict["TopicType"] },
                    new DBModuleModel() { ModuleCode = "Role", ModuleName = "角色", ActionList = moduleDict["Role"] },
                    new DBModuleModel() { ModuleCode = "Menu", ModuleName = "菜单", ActionList = moduleDict["Menu"] },
                    new DBModuleModel() { ModuleCode = "User", ModuleName = "用户", ActionList = moduleDict["User"] },
                    new DBModuleModel() { ModuleCode = "FlowSymbolType", ModuleName = "流程符号类别", ActionList = moduleDict["FlowSymbolType"] },
                    new DBModuleModel() { ModuleCode = "FlowType", ModuleName = "流程类别", ActionList = moduleDict["FlowType"] },
                    new DBModuleModel() { ModuleCode = "Flow", ModuleName = "流程", ActionList = moduleDict["Flow"] },
                    new DBModuleModel() { ModuleCode = "ProjectType", ModuleName = "项目类别", ActionList = moduleDict["ProjectType"] },
                    new DBModuleModel() { ModuleCode = "Project", ModuleName = "项目", ActionList = moduleDict["Project"] },
                    new DBModuleModel() { ModuleCode = "VoteType", ModuleName = "投票类别", ActionList = moduleDict["VoteType"] },
                    new DBModuleModel() { ModuleCode = "Vote", ModuleName = "投票", ActionList = moduleDict["Vote"] },
                    new DBModuleModel() { ModuleCode = "UserLog", ModuleName = "登录日志", ActionList = moduleDict["UserLog"] },
                    new DBModuleModel() { ModuleCode = "Message", ModuleName = "留言", ActionList = moduleDict["Message"] },
                    new DBModuleModel() { ModuleCode = "Attachment", ModuleName = "附件", ActionList = moduleDict["Attachment"] },
                    new DBModuleModel() { ModuleCode = "LinkFriendType", ModuleName = "友情链接类别", ActionList = moduleDict["LinkFriendType"] },
                    new DBModuleModel() { ModuleCode = "LinkFriend", ModuleName = "友情链接", ActionList = moduleDict["LinkFriend"] },
                    new DBModuleModel() { ModuleCode = "IndexMapper", ModuleName = "类别映射", ActionList = moduleDict["IndexMapper"] },
                    new DBModuleModel() { ModuleCode = "NoticeType", ModuleName = "公告类别", ActionList = moduleDict["NoticeType"] },
                    new DBModuleModel() { ModuleCode = "Notice", ModuleName = "公告", ActionList = moduleDict["Notice"] },
                };
                DataBaseHelper.TransactionEntityListBatchImport<DBModuleModel>(con, transaction, "T_Module", moduleModelList);
                #endregion

                #region 添加菜单数据
                List<InitMenuModel> menuModelList = new List<InitMenuModel>()
                {
                    new InitMenuModel()
                    {
                        TrunkMenu = new DBMenuModel() { ParentID = 0, MenuName = "主题系统", MenuIcon = "glyphicon glyphicon-file", BelongModule = "-1", MenuUrl = "", ActionList = "", MenuSort = 0, MenuStatus = 1 },
                        NodeMenuList = new List<DBMenuModel>()
                        {
                            new DBMenuModel() { MenuName = "主题管理", MenuUrl = "/Manager/Topic/List", BelongModule = "Topic", ActionList = moduleDict["Topic"], MenuSort = 0, MenuIcon = "", MenuStatus = 1 },
                            new DBMenuModel() { MenuName = "主题类别", MenuUrl = "/Manager/TopicType/List", BelongModule = "TopicType", ActionList = moduleDict["TopicType"], MenuSort = 0, MenuIcon = "", MenuStatus = 1 },
                            new DBMenuModel() { MenuName = "主题位置", MenuUrl = "/Manager/TopicPositionType/List", BelongModule = "TopicPositionType", ActionList = moduleDict["TopicPositionType"], MenuSort = 0, MenuIcon = "", MenuStatus = 1 },
                        }
                    },
                    new InitMenuModel()
                    {
                        TrunkMenu = new DBMenuModel() { ParentID = 0, MenuName = "项目管理", MenuIcon = "glyphicon glyphicon-random", BelongModule = "-1", MenuUrl = "", ActionList = "", MenuSort = 0, MenuStatus = 1 },
                        NodeMenuList = new List<DBMenuModel>()
                        {
                            new DBMenuModel() { MenuName = "项目管理", MenuUrl = "/Manager/Project/List", BelongModule = "Project", ActionList = moduleDict["Project"], MenuSort = 0, MenuIcon = "", MenuStatus = 1 },
                            new DBMenuModel() { MenuName = "项目类别", MenuUrl = "/Manager/ProjectType/List", BelongModule = "ProjectType", ActionList = moduleDict["ProjectType"], MenuSort = 0, MenuIcon = "", MenuStatus = 1 },
                            new DBMenuModel() { MenuName = "流程管理", MenuUrl = "/Manager/Flow/List", BelongModule = "Flow", ActionList = moduleDict["Flow"], MenuSort = 0, MenuIcon = "", MenuStatus = 1 },
                            new DBMenuModel() { MenuName = "流程类别", MenuUrl = "/Manager/FlowType/List", BelongModule = "FlowType", ActionList = moduleDict["FlowType"], MenuSort = 0, MenuIcon = "", MenuStatus = 1 },
                            new DBMenuModel() { MenuName = "流程符号", MenuUrl = "/Manager/FlowSymbolType/List", BelongModule = "FlowSymbolType", ActionList = moduleDict["FlowSymbolType"], MenuSort = 0, MenuIcon = "", MenuStatus = 1 },
                        }
                    },
                    new InitMenuModel()
                    {
                        TrunkMenu = new DBMenuModel() { ParentID = 0, MenuName = "投票系统", MenuIcon = "glyphicon glyphicon-fileglyphicon glyphicon-stats", BelongModule = "-1", MenuUrl = "", ActionList = "", MenuSort = 0, MenuStatus = 1 },
                        NodeMenuList = new List<DBMenuModel>()
                        {
                            new DBMenuModel() { MenuName = "投票管理", MenuUrl = "/Manager/Vote/List", BelongModule = "Vote", ActionList = moduleDict["Vote"], MenuSort = 0, MenuIcon = "", MenuStatus = 1 },
                            new DBMenuModel() { MenuName = "投票类别", MenuUrl = "/Manager/VoteType/List", BelongModule = "VoteType", ActionList = moduleDict["VoteType"], MenuSort = 0, MenuIcon = "", MenuStatus = 1 },
                        }
                    },
                    new InitMenuModel()
                    {
                        TrunkMenu = new DBMenuModel() { ParentID = 0, MenuName = "友情链接", MenuIcon = "glyphicon glyphicon-link", BelongModule = "-1", MenuUrl = "", ActionList = "", MenuSort = 0, MenuStatus = 1 },
                        NodeMenuList = new List<DBMenuModel>()
                        {
                            new DBMenuModel() { MenuName = "链接管理", MenuUrl = "/Manager/LinkFriend/List", BelongModule = "LinkFriend", ActionList = moduleDict["LinkFriend"], MenuSort = 0, MenuIcon = "", MenuStatus = 1 },
                            new DBMenuModel() { MenuName = "链接类别", MenuUrl = "/Manager/LinkFriendType/List", BelongModule = "LinkFriendType", ActionList = moduleDict["LinkFriendType"], MenuSort = 0, MenuIcon = "", MenuStatus = 1 },
                        }
                    },
                    new InitMenuModel()
                    {
                        TrunkMenu = new DBMenuModel() { ParentID = 0, MenuName = "站内公告", MenuIcon = "glyphicon glyphicon-bullhorn", BelongModule = "-1", MenuUrl = "", ActionList = "", MenuSort = 0, MenuStatus = 1 },
                        NodeMenuList = new List<DBMenuModel>()
                        {
                            new DBMenuModel() { MenuName = "公告管理", MenuUrl = "/Manager/Notice/List", BelongModule = "Notice", ActionList = moduleDict["Notice"], MenuSort = 0, MenuIcon = "", MenuStatus = 1 },
                            new DBMenuModel() { MenuName = "公告类别", MenuUrl = "/Manager/NoticeType/List", BelongModule = "NoticeType", ActionList = moduleDict["NoticeType"], MenuSort = 0, MenuIcon = "", MenuStatus = 1 },
                        }
                    },
                    new InitMenuModel()
                    {
                        TrunkMenu = new DBMenuModel() { ParentID = 0, MenuName = "系统设置", MenuIcon = "glyphicon glyphicon-cog", BelongModule = "-1", MenuUrl = "", ActionList = "", MenuSort = 0, MenuStatus = 1 },
                        NodeMenuList = new List<DBMenuModel>()
                        {
                            new DBMenuModel() { MenuName = "权限管理", MenuUrl = "/Manager/ActionType/List", BelongModule = "ActionType", ActionList = moduleDict["ActionType"], MenuSort = 0, MenuIcon = "", MenuStatus = 1 },
                            new DBMenuModel() { MenuName = "模块管理", MenuUrl = "/Manager/Module/List", BelongModule = "Module", ActionList = moduleDict["Module"], MenuSort = 0, MenuIcon = "", MenuStatus = 1 },
                            new DBMenuModel() { MenuName = "菜单管理", MenuUrl = "/Manager/Menu/List", BelongModule = "Menu", ActionList = moduleDict["Menu"], MenuSort = 0, MenuIcon = "", MenuStatus = 1 },
                            new DBMenuModel() { MenuName = "角色管理", MenuUrl = "/Manager/Role/List", BelongModule = "Role", ActionList = moduleDict["Role"], MenuSort = 0, MenuIcon = "", MenuStatus = 1 },
                            new DBMenuModel() { MenuName = "用户管理", MenuUrl = "/Manager/User/List", BelongModule = "User", ActionList = moduleDict["User"], MenuSort = 0, MenuIcon = "", MenuStatus = 1 },
                            new DBMenuModel() { MenuName = "留言管理", MenuUrl = "/Manager/Message/List", BelongModule = "Message", ActionList = moduleDict["Message"], MenuSort = 0, MenuIcon = "", MenuStatus = 1 },
                            new DBMenuModel() { MenuName = "日志管理", MenuUrl = "/Manager/UserLog/List", BelongModule = "UserLog", ActionList = moduleDict["UserLog"], MenuSort = 0, MenuIcon = "", MenuStatus = 1 },
                            new DBMenuModel() { MenuName = "附件管理", MenuUrl = "/Manager/Attachment/List", BelongModule = "Attachment", ActionList = moduleDict["Attachment"], MenuSort = 0, MenuIcon = "", MenuStatus = 1 },
                            new DBMenuModel() { MenuName = "类别映射", MenuUrl = "/Manager/IndexMapper/List", BelongModule = "IndexMapper", ActionList = moduleDict["IndexMapper"], MenuSort = 0, MenuIcon = "", MenuStatus = 1 },
                        }
                    }
                };
                foreach(InitMenuModel menuItem in menuModelList)
                {
                    DBMenuModel menuModel = menuItem.TrunkMenu;
                    int menuID = DataBaseHelper.TransactionScalar<int>(con, transaction, "insert into T_Menu(ParentID, MenuName, MenuUrl, BelongModule, ActionList, MenuSort, MenuIcon, MenuStatus)values(@ParentID, @MenuName, @MenuUrl, @BelongModule, @ActionList, @MenuSort, @MenuIcon, @MenuStatus);select SCOPE_IDENTITY();", new { menuModel.ParentID, menuModel.MenuName, menuModel.MenuIcon, menuModel.BelongModule, menuModel.MenuUrl, menuModel.ActionList, menuModel.MenuSort, menuModel.MenuStatus });
                    for(int menuIndex = 0; menuIndex < menuItem.NodeMenuList.Count; menuIndex ++)
                    {
                        menuItem.NodeMenuList[menuIndex].ParentID = menuID;
                    }
                    DataBaseHelper.TransactionEntityListBatchImport<DBMenuModel>(con, transaction, "T_Menu", menuItem.NodeMenuList);
                }

                List<DBMenuModel> menuDataList = DataBaseHelper.TransactionEntityList<DBMenuModel>(con, transaction, "select IdentityID from T_Menu with(nolock)");
                string menuIDList = StringHelper.ToString<int>(menuDataList.Select(p => p.IdentityID).ToList(), ",");

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(",");
                foreach (KeyValuePair<string, string> keyValueItem in moduleDict)
                {
                    List<string> menuActionDataList = StringHelper.ToList<string>(keyValueItem.Value, ",", true);
                    foreach(string menuActionData in menuActionDataList)
                    {
                        stringBuilder.Append(string.Format("{0}:{1},", keyValueItem.Key, menuActionData));
                    }
                }
                #endregion

                #region 更新角色数据
                DataBaseHelper.TransactionNonQuery(con, transaction, "update T_Role set MenuList=@MenuList, ActionList=@ActionList where IdentityID=@IdentityID", new { MenuList = menuIDList, ActionList = stringBuilder.ToString(), IdentityID = roleID });
                #endregion

                return true;
            });
        }
    }

    class InitMenuModel
    {
        public DBMenuModel TrunkMenu { get; set; }
        public List<DBMenuModel> NodeMenuList { get; set; }
    }
}
