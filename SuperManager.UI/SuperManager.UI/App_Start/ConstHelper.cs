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

        public static List<DBKeyValueModel> GetIndexMapperList()
        {
            List<DBKeyValueModel> modelList = new List<DBKeyValueModel>()
            {
                new DBKeyValueModel(){ Key = IndexMapperTypeEnum.Topic.ToString(), Value = GetIndexMapperName(IndexMapperTypeEnum.Topic) },
                new DBKeyValueModel(){ Key = IndexMapperTypeEnum.LinkFriend.ToString(), Value = GetIndexMapperName(IndexMapperTypeEnum.LinkFriend) },
            };
            return modelList;
        }
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
            if (indexType == IndexMapperTypeEnum.LinkFriend) return "链接";
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