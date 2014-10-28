using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alluminati.Controllers
{
    public class VisionController : Controller
    {
        // GET: Vision
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}