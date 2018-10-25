using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.FILTER
{
    /// <summary>
    /// 验证提交的表单数据是否正确，返回为 NULL 或者空字符串数据表示验证通过，否则提交返回值数据。
    /// </summary>
    public class FilterFactory
    {
        public static MenuFilter Menu = new MenuFilter();
        public static RoleFilter Role = new RoleFilter();
        public static TopicTypeFilter TopicType = new TopicTypeFilter();
        public static TopicPositionTypeFilter TopicPositionType = new TopicPositionTypeFilter();
        public static TopicFilter Topic = new TopicFilter();
        public static FlowFilter Flow = new FlowFilter();
        public static FlowTypeFilter FlowType = new FlowTypeFilter();
        public static ProjectTypeFilter ProjectType = new ProjectTypeFilter();
        public static ProjectFilter Project = new ProjectFilter();
        public static VoteTypeFilter VoteType = new VoteTypeFilter();
        public static VoteFilter Vote = new VoteFilter();
        public static UserFilter User = new UserFilter();
        public static FlowSymbolTypeFilter FlowSymbolType = new FlowSymbolTypeFilter();
        public static ActionTypeFilter ActionType = new ActionTypeFilter();
        public static ModuleFilter Module = new ModuleFilter();
        public static MessageFilter Message = new MessageFilter();
        public static MessageReplyFilter MessageReply = new MessageReplyFilter();
        public static LinkFriendTypeFilter LinkFriendType = new LinkFriendTypeFilter();
        public static LinkFriendFilter LinkFriend = new LinkFriendFilter();
        public static IndexMapperFilter IndexMapper = new IndexMapperFilter();
        public static NoticeFilter Notice = new NoticeFilter();
        public static NoticeTypeFilter NoticeType = new NoticeTypeFilter();
        public static ApplicationFilter Application = new ApplicationFilter();
    }
}
