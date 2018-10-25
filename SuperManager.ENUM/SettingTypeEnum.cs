using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.ENUM
{
    public class SettingTypeEnum
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public const string VERSION = "version";
        /// <summary>
        /// 管理后台标题
        /// </summary>
        public const string MANAGERTITLE = "managerTitle";
        /// <summary>
        /// 管理后台分页大小
        /// </summary>
        public const string MANAGERPAGESIZE = "managerPageSize";
        /// <summary>
        /// Log 日志开启状态
        /// </summary>
        public const string LOGOPENSTATUS = "logOpenStatus";
        /// <summary>
        /// 权限开启状态
        /// </summary>
        public const string AUTHOPENSTATUS = "authOpenStatus";
        /// <summary>
        /// 开发者工具开启状态
        /// </summary>
        public const string ATTACHOPENSTATUS = "attachOpenStatus";
        /// <summary>
        /// 上传图片大小
        /// </summary>
        public const string UPLOADIMAGEMAXSIZE = "uploadImageMaxSize";
        /// <summary>
        /// 上传图片后缀
        /// </summary>
        public const string UPLOADIMAGEEXT = "uploadImageExt";
        /// <summary>
        /// 上传视频大小
        /// </summary>
        public const string UPLOADVIDEOMAXSIZE = "uploadVideoMaxSize";
        /// <summary>
        /// 上传视频后缀
        /// </summary>
        public const string UPLOADVIDEOEXT = "uploadVideoExt";
        /// <summary>
        /// 上传文件大小
        /// </summary>
        public const string UPLOADFILEMAXSIZE = "uploadFileMaxSize";
        /// <summary>
        /// 上传文件后缀
        /// </summary>
        public const string UPLOADFILEEXT = "uploadFileExt";
        /// <summary>
        /// 数据库备份时间配置
        /// </summary>
        public const string BAKCRON = "bakCron";
        /// <summary>
        /// 数据库备份地址
        /// </summary>
        public const string BAKPATH = "bakPath";
    }
}
