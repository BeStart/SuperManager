using Helper.Core.Library;
using SuperManager.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.DAL
{
    public class IndexMapperDAL
    {
        private const string TABLE_NAME = "T_IndexMapper";

        public bool Operater(DBIndexMapperModel model)
        {
            if (model.IdentityID == 0)
            {
                return DataBaseHelper.Insert<DBIndexMapperModel>(model, p => p.IdentityID, TABLE_NAME);
            }
            else
            {
                return DataBaseHelper.Update<DBIndexMapperModel>(model, p => p.IdentityID == p.IdentityID, p => p.IdentityID, TABLE_NAME);
            }
        }
        public bool Delete(int identityID)
        {
            return DataBaseHelper.Delete<DBIndexMapperModel>(new { IdentityID = identityID }, p => p.IdentityID == p.IdentityID, TABLE_NAME);
        }
        public bool DeleteMore(string identityIDList)
        {
            List<int> dataList = StringHelper.ToList<int>(identityIDList, ",");
            return DataBaseHelper.Delete<DBIndexMapperModel>(null, p => dataList.Contains(p.IdentityID), TABLE_NAME);
        }
        public DBIndexMapperModel Select(int identityID)
        {
            return DataBaseHelper.Single<DBIndexMapperModel>(new { IdentityID = identityID }, p => new { p.IdentityID, p.IndexType, p.IndexID }, p => p.IdentityID == p.IdentityID, TABLE_NAME);
        }
        public List<DBIndexMapperModel> List()
        {
            return DataBaseHelper.More<DBIndexMapperModel>(null, p => new { p.IdentityID, p.IndexType, p.IndexID }, null, null, true, TABLE_NAME);
        }
        public List<DBIndexMapperModel> All()
        {
            return DataBaseHelper.More<DBIndexMapperModel>(null, p => new { p.IdentityID, p.IndexType, p.IndexID }, null, null, true, TABLE_NAME);
        }
    }
}
