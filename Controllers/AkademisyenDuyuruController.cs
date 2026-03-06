using AkademikBilgiSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AkademikBilgiSistemi.Controllers
{
    public class AkademisyenDuyuruController : Controller
    {
        UNINOTSISTEMIEntities3 db = new UNINOTSISTEMIEntities3();
        // GET: AkademisyenDuyuru
        public ActionResult AdmsDuyuru()
        {
            List<Tbl_Duyurular> duyuru = db.Tbl_Duyurular.ToList();
            return View(duyuru);
        }
        public ActionResult AdmsDuyuruSil()
        {

            return View();

        }
        public ActionResult AdmsDuyuruGönder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdmsDuyuruGönder(FormCollection fc)
        {
            int duyuruGönderenId = Convert.ToInt32(Session["Ogretmen"].ToString());
            string duyuruBaslik = fc["DuyuruBaslik"];
            string duyuruIcerik = fc["Duyuruİçerik"];
            string duyuruTarih = DateTime.Now.ToString();

            Tbl_Duyurular yeniDuyuru = new Tbl_Duyurular
            {
                DuyuruGönderen = duyuruGönderenId,
                DuyuruBaslik =duyuruBaslik,
                DuyuruIcerik=duyuruIcerik,
                DuyuruTarih=Convert.ToDateTime(duyuruTarih),
            };

            db.Tbl_Duyurular.Add(yeniDuyuru);
            db.SaveChanges();
            return Redirect("~/AkademisyenDuyuru/AdmsDuyuru");
        }

    }
}