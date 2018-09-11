﻿using Helper.Core.Library;
using SuperManager.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.DAL
{
    public class TopicPositionTypeDAL
    {
        public bool Operater(DBTopicPositionTypeModel model)
        {
            if (model.IdentityID == 0)
            {
                string commandText = "insert into T_TopicPositionType(TypeName, TypeSort)values(@TypeName, @TypeSort)";
                return DataBaseHelper.ExecuteNonQuery(commandText, new { TypeName = model.TypeName, TypeSort = model.TypeSort }) > 0;
            }
            else
            {
                string commandText = "update T_TopicPositionType set TypeName=@TypeName, TypeSort=@TypeSort where IdentityID=@IdentityID";
                return DataBaseHelper.ExecuteNonQuery(commandText, new { TypeName = model.TypeName, TypeSort = model.TypeSort, IdentityID = model.IdentityID }) > 0;
            }
        }

        public bool Exists(string typeName, int identityID)
        {
            string commandText = "select IdentityID from T_TopicPositionType with(nolock) where TypeName=@TypeName";
            int result = DataBaseHelper.ExecuteScalar<int>(commandText, new { TypeName = typeName });

            if (identityID == 0) return result > 0;
            return result == 0 ? false : (result != identityID);
        }

        public bool Delete(int identityID)
        {
            string commandText = "delete from T_TopicPositionType where IdentityID=@IdentityID";
            return DataBaseHelper.ExecuteNonQuery(commandText, new { IdentityID = identityID }) > 0;
        }

        public bool DeleteMore(string identityIDList)
        {
            identityIDList = StringHelper.TrimChar(identityIDList, ",");

            string commandText = "delete from T_TopicPositionType where IdentityID in (@IdentityIDList)";
            commandText = commandText.Replace("@IdentityIDList", identityIDList);

            return DataBaseHelper.ExecuteNonQuery(commandText) > 0;
        }

        public DBTopicPositionTypeModel Select(int identityID)
        {
            string commandText = "select IdentityID, TypeName, TypeSort from T_TopicPositionType with(nolock) where IdentityID=@IdentityID";
            return DataBaseHelper.ToEntity<DBTopicPositionTypeModel>(commandText, new { IdentityID = identityID });
        }

        public List<DBTopicPositionTypeModel> List()
        {
            string commandText = "select IdentityID, TypeName from T_TopicPositionType with(nolock)";
            return DataBaseHelper.ToEntityList<DBTopicPositionTypeModel>(commandText);
        }

        public List<DBTopicPositionTypeModel> All(string searchKey)
        {
            string commandText = "select IdentityID, TypeName, TypeSort from T_TopicPositionType with(nolock) {0} order by TypeSort desc";
            StringBuilder stringBuilder = new StringBuilder();
            if(!string.IsNullOrEmpty(searchKey))
            {
                stringBuilder.Append(" where TypeName like '%");
                stringBuilder.Append(searchKey);
                stringBuilder.Append("%' ");
            }
            commandText = string.Format(commandText, stringBuilder.ToString());
            return DataBaseHelper.ToEntityList<DBTopicPositionTypeModel>(commandText);
        }
    }
}
