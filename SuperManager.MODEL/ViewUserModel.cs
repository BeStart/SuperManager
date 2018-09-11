using Helper.Core.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.MODEL
{
    public class ViewUserModel
    {
        public string UserCode { get; set; }
        public string NickName { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }
}
