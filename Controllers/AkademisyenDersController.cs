using AkademikBilgiSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AkademikBilgiSistemi.Controllers
{
    public class AkademisyenDersController : Controller
    {
        UNINOTSISTEMIEntities3 db = new UNINOTSISTEMIEntities3();
        // GET: AkademisyenDers
        public ActionResult AdmsNotlar()
        {
            int dersID = Convert.ToInt32(Session["OgretmenBransID"].ToString());
            var dersnot = db.Tbl_Notlar.Where(x => x.DersID == dersID);
            List<Tbl_Notlar> notlar = dersnot.ToList();
            return View(notlar);
        }
        [HttpGet]
        public ActionResult OgrenciNotEkle()
        {
            int bransID = Convert.ToInt32(Session["OgretmenBransID"].ToString());
            var dersiAlmayanOgrenciler = db.Tbl_Ogrenciler
                .Where(o => !db.Tbl_Notlar.Any(n => n.OgrenciID == o.Ogr_ID && n.DersID == bransID))
                .Select(o => new SelectListItem
                {
                    Text = o.OgrAd + " " + o.OgrSoyad,
                    Value = o.Ogr_ID.ToString()
                }).ToList();

            ViewBag.OgrenciID = new SelectList(dersiAlmayanOgrenciler, "Value", "Text");
            return View();
            //List<SelectListItem> ogrencilist = (from ogrenci in db.Tbl_Ogrenciler.ToList() select new SelectListItem()
            //{
            //    Text = ogrenci.OgrAd + " " + ogrenci.OgrSoyad,
            //    Value = ogrenci.Ogr_ID.ToString()
            //} ).ToList();

            //return View();
            //ViewBag.OgrenciID = new SelectList(db.Tbl_Ogrenciler, "Ogr_ID", "Ogr_ID");
            //return View();
        }
        [HttpPost]
        public ActionResult OgrenciNotEkle(Tbl_Notlar ogrenciNot)
        {

            Tbl_Notlar yeniOgrDers = new Tbl_Notlar();
            yeniOgrDers.DersID = Convert.ToInt32(Session["OgretmenBransID"].ToString());



            if (yeniOgrDers.OgrenciID == ogrenciNot.OgrenciID && yeniOgrDers.DersID == ogrenciNot.DersID)
            {

            }
            else
            {
                yeniOgrDers.OgrenciID = ogrenciNot.OgrenciID;
                yeniOgrDers.Sinav1 = ogrenciNot.Sinav1;
                yeniOgrDers.Sinav2 = ogrenciNot.Sinav2;
                yeniOgrDers.Sinav3 = ogrenciNot.Sinav3;
                var ogrenciOrt = yeniOgrDers.Sinav1 * 20 / 100 + yeniOgrDers.Sinav2 * 20 / 100 + yeniOgrDers.Sinav3 * 60 / 100;
                yeniOgrDers.Ortalama = Convert.ToDecimal(ogrenciOrt);

                ViewBag.DersID = new SelectList(db.Tbl_Dersler, "DersID", "DersAd", ogrenciNot.DersID);
                ViewBag.OgrenciID = new SelectList(db.Tbl_Ogrenciler, "Ogr_ID", "OgrAd", ogrenciNot.OgrenciID);
                db.Tbl_Notlar.Add(yeniOgrDers);
                db.SaveChanges();
                return RedirectToAction("AdmsNotlar");
            }
            return View();
        }
        [HttpGet]
        public ActionResult NotKaydet(int id)
        {
            int bransID = Convert.ToInt32(Session["OgretmenBransID"].ToString());
            var not = db.Tbl_Notlar.FirstOrDefault(x => x.OgrenciID == id && x.DersID == bransID);
            return View(not);
        }
        [HttpPost]
        public ActionResult NotKaydet(Tbl_Notlar ogrNot, int id)
        {
            int bransID = Convert.ToInt32(Session["OgretmenBransID"].ToString());
            var not = db.Tbl_Notlar.FirstOrDefault(x => x.OgrenciID == id && x.DersID == bransID);
            not.Sinav1 = ogrNot.Sinav1;
            not.Sinav2 = ogrNot.Sinav2;
            not.Sinav3 = ogrNot.Sinav3;

            var ogrenciOrt = not.Sinav1 * 20 / 100 + not.Sinav2 * 20 / 100 + not.Sinav3 * 60 / 100;
            not.Ortalama = Convert.ToDecimal(ogrenciOrt);
            db.SaveChanges();
            return RedirectToAction("AdmsNotlar");
        }
    }
}