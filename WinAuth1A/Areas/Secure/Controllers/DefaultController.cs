using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WinAuth1A.Areas.Secure.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Secure/Default
		[Authorize]
        public ActionResult Index()
        {
            return View();
        }

	}
}