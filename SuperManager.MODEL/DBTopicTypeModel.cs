using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.MODEL
{
    public class DBTopicTypeModel
    {
        public int IdentityID { get; set; }
        /// <summary>
        /// 父编号
        /// </summary>
        public int ParentID { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 类别排序
        /// </summary>
        public int TypeSort { get; set; }
    }
}
