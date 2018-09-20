using Helper.Core.Library;
using SuperManager.ENUM;
using SuperManager.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperManager.UI
{
    public class SettingHelper
    {
        private static string XmlPath = null;
        private static Dictionary<string, string> SettingDict = null;
        
        public static void Init(string xmlPath)
        {
            XmlPath = xmlPath;
            SettingDict = XmlHelper.ToDict<string, string>(xmlPath, "//item", "name", "value");
        }
        public static bool Set(Dictionary<string, string> dict)
        {
            try
            {
                if(!dict.ContainsKey(SettingTypeEnum.LogOpenStatus))
                {
                    dict[SettingTypeEnum.LogOpenStatus] = "0";
                }
                if (!dict.ContainsKey(SettingTypeEnum.AuthOpenStatus))
                {
                    dict[SettingTypeEnum.AuthOpenStatus] = "0";
                }
                if (!dict.ContainsKey(SettingTypeEnum.AttachOpenStatus))
                {
                    dict[SettingTypeEnum.AttachOpenStatus] = "0";
                }

                List<ViewSettingModel> modelList = new List<ViewSettingModel>();
                foreach(KeyValuePair<string, string> keyValueItem in dict)
                {
                    modelList.Add(new ViewSettingModel() { Name = keyValueItem.Key, Value = keyValueItem.Value });
                }
                XmlHelper.SetValue<ViewSettingModel>(XmlPath, "//settings", "item[@name=\"{name}\"]", modelList, null);
                SettingDict = dict;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string Version
        {
            get { return GetValue(SettingTypeEnum.Version); }
        }
        public static string ManagerTitle
        {
            get { return GetValue(SettingTypeEnum.ManagerTitle); }
        }
        public static int ManagerPageSize
        {
            get
            {
                string value = GetValue(SettingTypeEnum.ManagerPageSize);
                if (string.IsNullOrEmpty(value)) return 1;

                return int.Parse(value);
            }
        }
        public static bool LogOpenStatus
        {
            get
            {
                string value = GetValue(SettingTypeEnum.LogOpenStatus);
                if (string.IsNullOrEmpty(value)) return false;

                return int.Parse(value) > 0;
            }
        }
        public static bool AuthOpenStatus
        {
            get
            {
                string value = GetValue(SettingTypeEnum.AuthOpenStatus);
                if (string.IsNullOrEmpty(value)) return true;

                return int.Parse(value) > 0;
            }
        }
        public static bool AttachOpenStatus
        {
            get
            {
                string value = GetValue(SettingTypeEnum.AttachOpenStatus);
                if (string.IsNullOrEmpty(value)) return false;

                return int.Parse(value) > 0;
            }
        }
        public static string BakCron
        {
            get { return GetValue(SettingTypeEnum.BakCron); }
        }

        private static string GetValue(string key)
        {
            if (SettingDict == null || !SettingDict.ContainsKey(key)) return "";
            return SettingDict[key];
        }
    }
}