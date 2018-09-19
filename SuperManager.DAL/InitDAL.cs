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
                // 向角色表添加数据
                int roleID = DataBaseHelper.TransactionScalar<int>(con, transaction, "insert into T_Role(RoleName, MenuList, ActionList)values(@RoleName, @MenuList, @ActionList);select SCOPE_IDENTITY();", new DBRoleModel() { RoleName = "超级管理员", ActionList = "", MenuList = "" });
                // 向用户添加数据
                bool result = DataBaseHelper.TransactionNonQuery(con, transaction, "insert into T_User(UserCode, NickName, UserPassword, RoleID)values(@UserCode, @NickName, @UserPassword, @RoleID)", new { UserCode = userCode, NickName = nickName, UserPassword = userPassword, RoleID = roleID }) > 0;
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
                // 添加 ModuleList
                List<DBModuleModel> moduleModelList = new List<DBModuleModel>()
                {
                    new DBModuleModel() { ModuleCode = "Topic", ModuleName = "主题", ActionList = ",Add,Check,Delete,Edit,Upload," },
                    new DBModuleModel() { ModuleCode = "Module", ModuleName = "模块", ActionList = ",Add,Delete,Edit," },
                    new DBModuleModel() { ModuleCode = "ActionType", ModuleName = "模块动作", ActionList = ",Add,Delete,Edit," },
                    new DBModuleModel() { ModuleCode = "TopicPositionType", ModuleName = "主题位置类别", ActionList = ",Add,Delete,Edit," },
                    new DBModuleModel() { ModuleCode = "TopicType", ModuleName = "主题类别", ActionList = ",Add,Delete,Edit," },
                    new DBModuleModel() { ModuleCode = "Role", ModuleName = "角色", ActionList = ",Add,Delete,Edit," },
                    new DBModuleModel() { ModuleCode = "Menu", ModuleName = "菜单", ActionList = ",Add,Delete,Edit," },
                    new DBModuleModel() { ModuleCode = "User", ModuleName = "用户", ActionList = ",Add,Delete,Edit,EditInfo,EditPassword," },
                    new DBModuleModel() { ModuleCode = "FlowSymbolType", ModuleName = "流程符号类别", ActionList = ",Add,Delete,Edit," },
                    new DBModuleModel() { ModuleCode = "FlowType", ModuleName = "流程类别", ActionList = ",Add,Delete,Edit," },
                    new DBModuleModel() { ModuleCode = "Flow", ModuleName = "流程", ActionList = ",Add,Delete,Edit,FlowAuth,FlowDesignAdd,FlowDesignEdit," },
                    new DBModuleModel() { ModuleCode = "ProjectType", ModuleName = "项目类别", ActionList = ",Add,Delete,Edit," },
                    new DBModuleModel() { ModuleCode = "Project", ModuleName = "项目", ActionList = ",Add,Delete,Edit," },
                    new DBModuleModel() { ModuleCode = "VoteType", ModuleName = "投票类别", ActionList = ",Add,Delete,Edit," },
                    new DBModuleModel() { ModuleCode = "Vote", ModuleName = "投票", ActionList = ",Add,Delete,Edit," },
                    new DBModuleModel() { ModuleCode = "UserLog", ModuleName = "登录日志", ActionList = ",Delete," },
                    new DBModuleModel() { ModuleCode = "Message", ModuleName = "留言", ActionList = ",Delete,Info,Reply,Upload," },
                    new DBModuleModel() { ModuleCode = "Attachment", ModuleName = "附件", ActionList = ",Delete," },
                    new DBModuleModel() { ModuleCode = "LinkFriendType", ModuleName = "友情链接类别", ActionList = ",Add,Delete,Edit," },
                    new DBModuleModel() { ModuleCode = "LinkFriend", ModuleName = "友情链接", ActionList = ",Add,Delete,Edit," },
                    new DBModuleModel() { ModuleCode = "IndexMapper", ModuleName = "类别映射", ActionList = ",Add,Delete,Edit," },
                    new DBModuleModel() { ModuleCode = "NoticeType", ModuleName = "公告类别", ActionList = ",Add,Delete,Edit," },
                    new DBModuleModel() { ModuleCode = "Notice", ModuleName = "公告", ActionList = ",Add,Delete,Edit,Upload," },
                };
                DataBaseHelper.TransactionEntityListBatchImport<DBModuleModel>(con, transaction, "T_Module", moduleModelList);
                return true;
            });
        }
    }
}
