using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.MODEL
{
    public class DBIndexMapperModel
    {
        public int IdentityID { get; set; }
        /// <summary>
        /// 映射类别
        /// </summary>
        public int IndexType { get; set; }
        /// <summary>
        /// 索引编号
        /// </summary>
        public int IndexID { get; set; }
        /// <summary>
        /// 对应编号
        /// </summary>
        public int MapperID { get; set; }
    }
}
