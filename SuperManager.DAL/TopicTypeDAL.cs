using Helper.Core.Library;
using SuperManager.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.DAL
{
    public class TopicTypeDAL
    {
        public bool Operater(DBTopicTypeModel model)
        {
            if(model.IdentityID == 0)
            {
                string commandText = "insert into T_TopicType(ParentID, TypeName, TypeSort)values(@ParentID, @TypeName, @TypeSort)";
                return DataBaseHelper.ExecuteNonQuery(commandText, new { ParentID = model.ParentID, TypeName = model.TypeName, TypeSort = model.TypeSort }) > 0;
            }
            else
            {
                string commandText = "update T_TopicType set ParentID=@ParentID, TypeName=@TypeName, TypeSort=@TypeSort where IdentityID=@IdentityID";
                return DataBaseHelper.ExecuteNonQuery(commandText, new { ParentID = model.ParentID, TypeName = model.TypeName, TypeSort = model.TypeSort, IdentityID = model.IdentityID }) > 0;
            }
        }

        public bool Delete(int identityID)
        {
            string commandText = "delete from T_TopicType where IdentityID=@IdentityID";
            return DataBaseHelper.ExecuteNonQuery(commandText, new { IdentityID = identityID }) > 0;
        }

        public bool DeleteMore(string identityIDList)
        {
            identityIDList = StringHelper.TrimChar(identityIDList, ",");

            string commandText = "delete from T_TopicType where IdentityID in (@IdentityIDList)";
            commandText = commandText.Replace("@IdentityIDList", identityIDList);

            return DataBaseHelper.ExecuteNonQuery(commandText) > 0;
        }

        public DBTopicTypeModel Select(int identityID)
        {
            string commandText = "select IdentityID, ParentID, TypeName, TypeSort from T_TopicType with(nolock) where IdentityID=@IdentityID";
            return DataBaseHelper.ToEntity<DBTopicTypeModel>(commandText, new { IdentityID = identityID });
        }

        public List<DBTopicTypeModel> List(int parentID = -1)
        {
            if(parentID == -1)
            {
                string commandText = "select IdentityID, ParentID, TypeName from T_TopicType with(nolock)";
                return DataBaseHelper.ToEntityList<DBTopicTypeModel>(commandText);
            }
            else
            {
                string commandText = "select IdentityID, TypeName from T_TopicType with(nolock) where ParentID=@ParentID";
                return DataBaseHelper.ToEntityList<DBTopicTypeModel>(commandText, new { ParentID = parentID });
            }
        }

        public List<ViewTreeTopicTypeModel> All(string searchKey)
        {
            if (string.IsNullOrEmpty(searchKey))
            {
                string commandText = "select IdentityID, ParentID, TypeName, TypeSort from T_TopicType with(nolock) order by TypeSort desc";
                return DataBaseHelper.ToEntityList<ViewTreeTopicTypeModel>(commandText);
            }
            else
            {
                string commandText = "select IdentityID, 0 as ParentID, TypeName, TypeSort from T_TopicType with(nolock) where TypeName like '%{0}%' order by TypeSort desc";
                commandText = string.Format(commandText, searchKey);
                return DataBaseHelper.ToEntityList<ViewTreeTopicTypeModel>(commandText);
            }
        }
    }
}
