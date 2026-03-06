using AkademikBilgiSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AkademikBilgiSistemi.Controllers
{
    public class AkademisyenHomeController : Controller
    {
        UNINOTSISTEMIEntities3 db = new  UNINOTSISTEMIEntities3();  
        // GET: AkademisyenHome
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AdmsAyarlar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdmsAyarlar(Tbl_Ogretmenler ogretmenBilgi, FormCollection fc)
        {

            Tbl_Ogretmenler ogretmen = db.Tbl_Ogretmenler.FirstOrDefault(x => x.OgrtID == ogretmenBilgi.OgrtID && x.OgrtMail == ogretmenBilgi.OgrtMail && x.OgrtSifre == ogretmenBilgi.OgrtSifre);
            if (ogretmen != null)
            {
                Tbl_Ogretmenler ogrtSifre = db.Tbl_Ogretmenler.FirstOrDefault(x => x.OgrtID == ogretmenBilgi.OgrtID);
                ogretmen.OgrtSifre = Convert.ToInt32(fc["YeniSifre"]);

                db.SaveChanges();
                ViewBag.mesaj = "Şifre Başarıyla Güncellenmiştir.";
                
            }
            else
            {
                ViewBag.mesaj = "Girdiğiniz Bilgiler Hatalı.";
            }
            return View();

        }

        public ActionResult AdmsBilgiler()
        {

            int ogrtBilgi = Convert.ToInt32(Session["Ogretmen"].ToString());
            var ogrtBilgisi = db.Tbl_Ogretmenler.FirstOrDefault(x => x.OgrtID == ogrtBilgi);
            return View(ogrtBilgisi);

        }


    }
}