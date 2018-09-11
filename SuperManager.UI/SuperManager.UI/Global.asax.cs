﻿using Helper.Core.Library;
using log4net.Config;
using SuperManager.MODEL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml;

namespace SuperManager.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // 初始化数据库连接字符串
            DataBaseHelper.InitConnectionString(ConfigHelper.ConnectionString);
            // Log4 日志配置加载
            string logFileName = FilePath(ConfigurationManager.AppSettings["log4net"]);
            if (System.IO.File.Exists(logFileName))
            {
                XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(logFileName));
            }
        }
        private static string FilePath(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }
            return string.Format("{0}/{1}", AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\', '/'), name.TrimStart('\\', '/'));
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // 如果日志未开启
            if (!ConfigHelper.LogStatus) return;

            Exception exception = Server.GetLastError();
            // 开始一个线程来写日志
            Task.Run(() =>
            {
                // 写入日志
                LogHelper.Error(exception);
            });
        }
    }
}