using Helper.Core.Library;
using SuperManager.ENUM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperManager.UI.Areas.Manager.Controllers
{
    public class CommonController : Controller
    {
        public ActionResult Error(string note = null, string url = null, bool status = true, bool parent = false)
        {
            ViewBag.Note = note;
            ViewBag.Url = url;
            ViewBag.Status = status;
            ViewBag.Parent = parent;
            return View();
        }

        public ActionResult Confirm(string note = null, string okUrl = null, string cancelUrl = null, bool status = true, bool parent = false)
        {
            ViewBag.Note = note;
            ViewBag.OkUrl = okUrl;
            ViewBag.CancelUrl = cancelUrl;
            ViewBag.Parent = parent;
            return View();
        }
    }
}