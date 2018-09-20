using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperManager.MODEL
{
    public class ViewInitMenuModel
    {
        public DBMenuModel TrunkMenu { get; set; }
        public List<DBMenuModel> NodeMenuList { get; set; }
    }
}
