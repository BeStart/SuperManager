using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.MODEL
{
    public class DBApplicationModel
    {
        public int IdentityID { get; set; }
        /// <summary>
        /// 应用图标
        /// </summary>
        public string ApplicationIcon { get; set; }
        /// <summary>
        /// 应用地址
        /// </summary>
        public string ApplicationUrl { get; set; }
        /// <summary>
        /// 应用名称
        /// </summary>
        public string ApplicationName { get; set; }
        /// <summary>
        /// 应用类别
        /// </summary>
        public string ApplicationType { get; set; }
        /// <summary>
        /// X 坐标
        /// </summary>
        public int ApplicationX { get; set; }
        /// <summary>
        /// Y 坐标
        /// </summary>
        public int ApplicationY { get; set; }
        /// <summary>
        /// 宽
        /// </summary>
        public int ApplicationWidth { get; set; }
        /// <summary>
        /// 高
        /// </summary>
        public int ApplicationHeight { get; set; }
    }
}
