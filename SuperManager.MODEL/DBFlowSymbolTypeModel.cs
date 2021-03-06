﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.MODEL
{
    public class DBFlowSymbolTypeModel
    {
        public int IdentityID { get; set; }
        /// <summary>
        /// 符号编号
        /// </summary>
        public string TypeCode { get; set; }
        /// <summary>
        /// 符号名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int TypeSort { get; set; }
    }
}
