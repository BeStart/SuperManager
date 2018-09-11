﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.MODEL
{
    public class DBUserFullModel : DBUserModel
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 角色菜单列表
        /// </summary>
        public string MenuList { get; set; }
        /// <summary>
        /// 角色动作列表
        /// </summary>
        public string ActionList { get; set; }
    }
}
