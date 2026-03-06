using AkademikBilgiSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AkademikBilgiSistemi.Controllers
{
    public class LoginController : Controller
    {
        UNINOTSISTEMIEntities3 db = new UNINOTSISTEMIEntities3();
        // GET: Login

        [AllowAnonymous]
        public ActionResult KullanıcıSecme()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult OgrLogin()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult OgrLogin(Tbl_Ogrenciler ogrenci)
        {
            var girenOgrenci = db.Tbl_Ogrenciler.FirstOrDefault( x => x.Ogr_ID == ogrenci.Ogr_ID && x.OgrSifre == ogrenci.OgrSifre);
            if (girenOgrenci != null)
            {
                FormsAuthentication.SetAuthCookie(girenOgrenci.OgrAd, false);
                Session["Kullanıcı"] = girenOgrenci.Ogr_ID;
                Session["OgrenciAdSoyad"] = girenOgrenci.OgrAd + ' ' + girenOgrenci.OgrSoyad;
                //Session["OgrenciBolum"] = girenOgrenci.OgrB
                return Redirect("/Home/Index");
            }
            else
            {
                ViewBag.Mesaj = "Geçersiz Öğrenci Numarası veya Şifre";
                return View();
            }
                
         }

        public ActionResult OgrLogout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return Redirect("~/Login/OgrLogin");
        }

        [AllowAnonymous]
        public ActionResult AdmsLogin()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult AdmsLogin(Tbl_Ogretmenler ogretmen)
        {
            var girenOgretmen = db.Tbl_Ogretmenler.FirstOrDefault(x => x.OgrtID == ogretmen.OgrtID && x.OgrtSifre == ogretmen.OgrtSifre);
            if (girenOgretmen != null)
            {
                FormsAuthentication.SetAuthCookie(girenOgretmen.OgrtAd, false);
                Session["Ogretmen"] = girenOgretmen.OgrtID;
                Session["OgretmenAdi"] = girenOgretmen.OgrtAd;
                Session["OgretmenSoyadi"] = girenOgretmen.OgrtSoyad;
                Session["OgretmenBransID"] = girenOgretmen.OgrtBrans;
                return Redirect("/AkademisyenHome/Index");
            }
            else
            {
                ViewBag.Mesaj = "Geçersiz Akademisyen Numarası veya Şifre";
                return View();
            }
        }

        public ActionResult AdmsLogout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("/AdmsLogin");
        }
        
    }
}