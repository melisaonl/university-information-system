using AkademikBilgiSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AkademikBilgiSistemi.Controllers
{
    public class MesajlarController : Controller
    {
        UNINOTSISTEMIEntities3 db = new UNINOTSISTEMIEntities3();
        // GET: Mesajlar
        public ActionResult GelenMesajlar()
        {
            int ogrBilgi = Convert.ToInt32(Session["Kullanıcı"].ToString());
            var mesaj = db.Tbl_Mesajlar.Where(x => x.Alıcı == ogrBilgi);
            List<Tbl_Mesajlar> mesajlar = mesaj.ToList();
            return View(mesajlar);
        }

        [HttpPost]
        public ActionResult GelenMesajlar(int id)
        {
            int ogrBilgi = Convert.ToInt32(Session["Kullanıcı"].ToString());
            var gelenMesaj = db.Tbl_Mesajlar.FirstOrDefault(x => x.MesajID == id && x.Tbl_Ogrenciler.Ogr_ID == ogrBilgi);

            return View(); 
        }

        public ActionResult MesajGönder()
        {
            var ogrenciListesi = db.Tbl_Ogrenciler
                  .Select(o => new SelectListItem
                  {
                      Text = o.OgrAd + " " + o.OgrSoyad,
                      Value = o.Ogr_ID.ToString()
                  }).ToList();
            SelectList aliciSelectList = new SelectList(ogrenciListesi, "Value", "Text");

            ViewBag.OgrenciListesi = aliciSelectList;

            return View();
        }

        [HttpPost]
        public ActionResult MesajGönder(FormCollection fc)
        {
            int mesajGonderen = Convert.ToInt32(Session["Kullanıcı"].ToString());
            int aliciId = Convert.ToInt32(fc["Alıcı"]);
            string mesajBaslik = fc["MesajBaslik"];
            string mesajIcerik = fc["MesajIcerik"];
            string duyuruTarih = DateTime.Now.ToString();

            Tbl_Mesajlar yeniMesaj = new Tbl_Mesajlar
            {
                Alıcı = aliciId,
                Gonderen = mesajGonderen,
                Baslik = mesajBaslik,
                Icerik = mesajIcerik,
                Tarih = Convert.ToDateTime(duyuruTarih),
            };

            db.Tbl_Mesajlar.Add(yeniMesaj);
            db.SaveChanges();
            return Redirect("~/Mesajlar/MesajGönder");
        }
    }
}