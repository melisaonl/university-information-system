using AkademikBilgiSistemi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using System;

namespace AkademikBilgiSistemi.Controllers
{
    public class HomeController : Controller
    {
        public UNINOTSISTEMIEntities3 db = new UNINOTSISTEMIEntities3();
        // GET: Home
        [Authorize]
        public ActionResult Index()
        {
            List<Tbl_Duyurular> ogrDuyurular = db.Tbl_Duyurular.ToList();
            return View(ogrDuyurular);
        }
        public ActionResult Bilgiler()
        {
            int ogrBilgi = Convert.ToInt32(Session["Kullanıcı"].ToString());
            var ogrBilgiler = db.Tbl_Ogrenciler.FirstOrDefault(x => x.Ogr_ID == ogrBilgi);
            return View(ogrBilgiler);
        }
        public ActionResult Ayarlar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Ayarlar(Tbl_Ogrenciler ogrenciBilgi, FormCollection fc)
        {
            Tbl_Ogrenciler ogrenci = db.Tbl_Ogrenciler.FirstOrDefault(x => x.Ogr_ID == ogrenciBilgi.Ogr_ID && x.OgrMail == ogrenciBilgi.OgrMail && x.OgrSifre == ogrenciBilgi.OgrSifre);
            if (ogrenci != null)
            {
                Tbl_Ogrenciler ogrtSifre = db.Tbl_Ogrenciler.FirstOrDefault(x => x.Ogr_ID == ogrenciBilgi.Ogr_ID);
                ogrenci.OgrSifre = fc["YeniSifre"];

                db.SaveChanges();
                ViewBag.mesaj = "Şifre Başarıyla Güncellenmiştir.";
            }
            else
            {
                ViewBag.mesaj = "Girdiğiniz Bilgiler Hatalı.";
            }
            return View();
        }
    }
}