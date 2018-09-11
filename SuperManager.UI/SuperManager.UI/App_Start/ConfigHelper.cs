using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SuperManager.UI
{
    public class ConfigHelper
    {
        public static string ConnectionString
        {
            get { return GetValue("connectionString"); }
        }

        public static string Version
        {
            get { return GetValue("version"); }
        }

        public static string ManagerTitle
        {
            get { return GetValue("managerTitle"); }
        }

        public static int ManagerPageSize
        {
            get { return int.Parse(GetValue("managerPageSize")); }
        }

        public static string ConfuseKey
        {
            get { return GetValue("confuseKey"); }
        }

        public static string TokenType
        {
            get { return GetValue("tokenType"); }
        }

        public static string TokenName
        {
            get { return GetValue("tokenName"); }
        }

        public static string TokenUserCode
        {
            get { return GetValue("tokenUserCode"); }
        }

        public static bool LogStatus
        {
            get { return int.Parse(GetValue("logStatus")) > 0; }
        }

        public static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}