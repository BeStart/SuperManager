using Helper.Core.Library;
using SuperManager.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.DAL
{
    public class VoteItemDAL
    {
        public List<DBVoteItemModel> List(int voteID)
        {
            string commandText = "select IdentityID, ItemID, VoteID, ItemTitle, ItemType, ItemContent, ItemMaxCount, ItemNum from T_VoteItem with(nolock) where VoteID=@VoteID";
            return DataBaseHelper.ToEntityList<DBVoteItemModel>(commandText, new { VoteID = voteID });
        }
    }
}
