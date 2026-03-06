using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AkademikBilgiSistemi.Controllers
{
    public class AkademisyenLoginController : Controller
    {
        // GET: AkademisyenLogin
        public ActionResult AdmsLogin()
        {
            return View();
        }
        public ActionResult AdmsLogout()
        {
            return View();
        }
    }
}