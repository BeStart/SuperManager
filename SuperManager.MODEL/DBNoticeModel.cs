using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.MODEL
{
    public class DBNoticeModel
    {
        public int IdentityID { get; set; }
        public int NoticeType { get; set; }
        public string NoticeTitle { get; set; }
        public string NoticeContent { get; set; }
        public DateTime NoticeDateTime { get; set; }
    }
}
