using AkademikBilgiSistemi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AkademikBilgiSistemi.Controllers
{
    public class DerslerController : Controller
    {
        public UNINOTSISTEMIEntities3 db = new UNINOTSISTEMIEntities3();
        public int ogrBilgi;

        // GET: Dersler
        public ActionResult Notlar()
        {
            ogrBilgi = Convert.ToInt32(Session["Kullanıcı"].ToString());
            var ogrnot = db.Tbl_Notlar.Where(x => x.OgrenciID == ogrBilgi );
            List<Tbl_Notlar> notlar = ogrnot.ToList();
            return View(notlar);

        }
        public ActionResult DersProgrami()
        {
            return View();
        }
    }
}