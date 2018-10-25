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
        /// <summary>
        /// 索引映射字典
        /// </summary>
        private static readonly Dictionary<int, List<DBKeyValueModel>> IndexMapperKeyValueDict = new Dictionary<int, List<DBKeyValueModel>>()
        {
            { IndexMapperTypeEnum.TOPIC, new List<DBKeyValueModel>(){
                new DBKeyValueModel(){ Key = "1", Value = "栏目一" },
                new DBKeyValueModel(){ Key = "2", Value = "栏目二" },
            }},
            { IndexMapperTypeEnum.LINKFRIEND, new List<DBKeyValueModel>(){
                new DBKeyValueModel(){ Key = "1", Value = "链接一" },
                new DBKeyValueModel(){ Key = "2", Value = "链接二" },
            }}
        };

        /// <summary>
        /// 操作字典
        /// </summary>
        private static readonly Dictionary<string, string> OperaterKeyValueDict = new Dictionary<string, string>()
        {
            { OperaterTypeEnum.DEFAULT, OperaterTypeEnum.DEFAULTNAME},
            { OperaterTypeEnum.DELETE, OperaterTypeEnum.DELETENAME },
            { OperaterTypeEnum.CHECKED, OperaterTypeEnum.CHECKEDNAME }
        };

        /// <summary>
        /// 获取索引映射数据
        /// </summary>
        /// <returns></returns>
        public static List<DBKeyValueModel> GetIndexMapperList()
        {
            List<DBKeyValueModel> modelList = new List<DBKeyValueModel>()
            {
                new DBKeyValueModel(){ Key = IndexMapperTypeEnum.TOPIC.ToString(), Value = GetIndexMapperName(IndexMapperTypeEnum.TOPIC) },
                new DBKeyValueModel(){ Key = IndexMapperTypeEnum.LINKFRIEND.ToString(), Value = GetIndexMapperName(IndexMapperTypeEnum.LINKFRIEND) },
            };
            return modelList;
        }
        public static Dictionary<int, List<DBKeyValueModel>> GetIndexMapperKeyValueDict()
        {
            return IndexMapperKeyValueDict;
        }
        /// <summary>
        /// 根据索引类别获取索引映射数据
        /// </summary>
        /// <param name="indexType"></param>
        /// <returns></returns>
        public static List<DBKeyValueModel> GetIndexMapperKeyValueList(int indexType)
        {
            if (IndexMapperKeyValueDict == null || !IndexMapperKeyValueDict.ContainsKey(indexType)) return null;
            return IndexMapperKeyValueDict[indexType];
        }
        /// <summary>
        /// 根据索引类别获取映射名称
        /// </summary>
        /// <param name="indexType"></param>
        /// <returns></returns>
        public static string GetIndexMapperName(int indexType)
        {
            if (indexType == IndexMapperTypeEnum.TOPIC) return IndexMapperTypeEnum.TOPICNAME;
            if (indexType == IndexMapperTypeEnum.LINKFRIEND) return IndexMapperTypeEnum.LINKFRIENDNAME;
            return "";
        }
        public static string GetIndexMapperName(int indexType, int indexID)
        {
            if (IndexMapperKeyValueDict == null || !IndexMapperKeyValueDict.ContainsKey(indexType)) return "";
            DBKeyValueModel model = IndexMapperKeyValueDict[indexType].Where(p => p.Key == indexID.ToString()).FirstOrDefault();
            if (model == null) return "";
            return model.Value;
        }
        /// <summary>
        /// 根据操作类别获取操作名称
        /// </summary>
        /// <param name="operaterType"></param>
        /// <returns></returns>
        public static string GetOperaterName(string operaterType)
        {
            if (OperaterKeyValueDict == null || OperaterKeyValueDict.Count == 0 || !OperaterKeyValueDict.ContainsKey(operaterType)) return "";
            return OperaterKeyValueDict[operaterType];
        }
    }
}