using AkademikBilgiSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AkademikBilgiSistemi.Controllers
{
    public class AkademisyenOgrencilerController : Controller
    {
        UNINOTSISTEMIEntities3 db = new UNINOTSISTEMIEntities3();
        // GET: AkademisyenOgrenciler
        public ActionResult AdmsOgrListe()
        {
            List<Tbl_Ogrenciler> ogrenciler = db.Tbl_Ogrenciler.ToList();
            return View(ogrenciler);
        }

        public ActionResult AdmsOgrDetay(int id)
        {
            var ogrBilgiler = db.Tbl_Ogrenciler.FirstOrDefault(x => x.Ogr_ID == id);
            return View(ogrBilgiler);
        }

        public ActionResult OgrGuncelle(int id)
        {
            var ogrGuncelle = db.Tbl_Ogrenciler.FirstOrDefault(x => x.Ogr_ID == id);
            return View(ogrGuncelle);
        }
        [HttpPost]
        public ActionResult OgrGuncelle(FormCollection fc, int id)
        {
            string OgrenciAdi = fc["OgrenciAdi"];
            string OgrenciSoyadi = fc["OgrenciSoyadi"];
            string OgrenciUniversite = fc["OgrenciUniversite"];
            string OgrenciFakulte = fc["OgrenciFakulte"];
            string OgrenciTelefon = fc["OgrenciTelefon"];
            string OgrenciMail = fc["OgrenciMail"];

            
            Tbl_Ogrenciler ogrenciGuncelle = db.Tbl_Ogrenciler.FirstOrDefault(x => x.Ogr_ID == id);
            ogrenciGuncelle.OgrAd = OgrenciAdi;
            ogrenciGuncelle.OgrSoyad = OgrenciSoyadi;
            ogrenciGuncelle.OgrUniAdi = OgrenciUniversite;
            ogrenciGuncelle.OgrFakulteAdi = OgrenciFakulte;
            ogrenciGuncelle.OgrTelefon = OgrenciTelefon;
            ogrenciGuncelle.OgrMail = OgrenciMail;

            db.SaveChanges();

            return Redirect("~/AkademisyenOgrenciler/AdmsOgrListe");
        }
        public ActionResult OgrEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult OgrEkle(FormCollection fc)
        {
            if (Session["Ogretmen"] != null)
            {
                string OgrenciAdi = fc["OgrenciAdi"];
                string OgrenciSoyadi = fc["OgrenciSoyadi"];
                string OgrenciUniversite = fc["OgrenciUniversite"];
                string OgrenciFakulte = fc["OgrenciFakulte"];
                string OgrenciTelefon = fc["OgrenciTelefon"];
                string OgrenciMail = fc["OgrenciMail"];

                Tbl_Ogrenciler yeniOgrenci = new Tbl_Ogrenciler
                {
                    OgrAd = OgrenciAdi,
                    OgrSoyad = OgrenciSoyadi,
                    OgrUniAdi = OgrenciUniversite,
                    OgrFakulteAdi = OgrenciFakulte,
                    OgrTelefon = OgrenciTelefon,
                    OgrMail = OgrenciMail
                };

                db.Tbl_Ogrenciler.Add(yeniOgrenci);
                db.SaveChanges();

                return Redirect("~/AkademisyenOgrenciler/AdmsOgrListe");

            }
            else
            {
                return Redirect("~/Login/AdmsLogin");
            }

        }
        public ActionResult OgrSil(int id)
        {
            Tbl_Ogrenciler ogrenciyiSil = db.Tbl_Ogrenciler.FirstOrDefault(x => x.Ogr_ID == id);
            db.Tbl_Ogrenciler.Remove(ogrenciyiSil);
            db.SaveChanges();

            return Redirect("~/AkademisyenOgrenciler/AdmsOgrListe");
        }
    }
}