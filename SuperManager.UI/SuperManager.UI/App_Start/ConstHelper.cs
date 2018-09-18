using SuperManager.ENUM;
using SuperManager.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperManager.UI
{
    public class ConstHelper
    {
        private static readonly Dictionary<int, List<DBKeyValueModel>> IndexMapperKeyValueDict = new Dictionary<int, List<DBKeyValueModel>>()
        {
            { IndexMapperTypeEnum.Topic, new List<DBKeyValueModel>(){
                new DBKeyValueModel(){ Key = "1", Value = "栏目一" },
                new DBKeyValueModel(){ Key = "2", Value = "栏目二" },
            }},
            { IndexMapperTypeEnum.LinkFriend, new List<DBKeyValueModel>(){
                new DBKeyValueModel(){ Key = "1", Value = "栏目一" },
                new DBKeyValueModel(){ Key = "2", Value = "栏目二" },
            }}
        };

        public static Dictionary<int, List<DBKeyValueModel>> GetIndexMapperKeyValueDict()
        {
            return IndexMapperKeyValueDict;
        }
        public static List<DBKeyValueModel> GetIndexMapperKeyValueList(int indexType)
        {
            if (IndexMapperKeyValueDict == null || !IndexMapperKeyValueDict.ContainsKey(indexType)) return null;
            return IndexMapperKeyValueDict[indexType];
        }
        public static string GetIndexMapperName(int indexType)
        {
            if (indexType == IndexMapperTypeEnum.Topic) return "新闻";
            return "";
        }
        public static string GetIndexMapperName(int indexType, int indexID)
        {
            if (IndexMapperKeyValueDict == null || !IndexMapperKeyValueDict.ContainsKey(indexType)) return "";
            DBKeyValueModel model = IndexMapperKeyValueDict[indexType].Where(p => p.Key == indexID.ToString()).FirstOrDefault();
            if (model == null) return "";
            return model.Value;
        }
    }
}