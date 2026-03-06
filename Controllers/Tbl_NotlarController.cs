using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AkademikBilgiSistemi.Models;

namespace AkademikBilgiSistemi.Controllers
{
    public class Tbl_NotlarController : Controller
    {
        private UNINOTSISTEMIEntities3 db = new UNINOTSISTEMIEntities3();

        // GET: Tbl_Notlar
        public ActionResult Index()
        {
            var tbl_Notlar = db.Tbl_Notlar.Include(t => t.Tbl_Dersler).Include(t => t.Tbl_Ogrenciler);
            return View(tbl_Notlar.ToList());
        }

        // GET: Tbl_Notlar/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Notlar tbl_Notlar = db.Tbl_Notlar.Find(id);
            if (tbl_Notlar == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Notlar);
        }

        // GET: Tbl_Notlar/Create
        public ActionResult Create()
        {
            ViewBag.DersID = new SelectList(db.Tbl_Dersler, "DersID", "DersAd");
            ViewBag.OgrenciID = new SelectList(db.Tbl_Ogrenciler, "Ogr_ID", "OgrAd");
            return View();
        }

        // POST: Tbl_Notlar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OgrenciID,DersID,Sinav1,Sinav2,Sinav3,Ortalama,Durum")] Tbl_Notlar tbl_Notlar)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Notlar.Add(tbl_Notlar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DersID = new SelectList(db.Tbl_Dersler, "DersID", "DersAd", tbl_Notlar.DersID);
            ViewBag.OgrenciID = new SelectList(db.Tbl_Ogrenciler, "Ogr_ID", "OgrAd", tbl_Notlar.OgrenciID);
            return View(tbl_Notlar);
        }

        // GET: Tbl_Notlar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Notlar tbl_Notlar = db.Tbl_Notlar.Find(id);
            if (tbl_Notlar == null)
            {
                return HttpNotFound();
            }
            ViewBag.DersID = new SelectList(db.Tbl_Dersler, "DersID", "DersAd", tbl_Notlar.DersID);
            ViewBag.OgrenciID = new SelectList(db.Tbl_Ogrenciler, "Ogr_ID", "OgrAd", tbl_Notlar.OgrenciID);
            return View(tbl_Notlar);
        }

        // POST: Tbl_Notlar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OgrenciID,DersID,Sinav1,Sinav2,Sinav3,Ortalama,Durum")] Tbl_Notlar tbl_Notlar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Notlar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DersID = new SelectList(db.Tbl_Dersler, "DersID", "DersAd", tbl_Notlar.DersID);
            ViewBag.OgrenciID = new SelectList(db.Tbl_Ogrenciler, "Ogr_ID", "OgrAd", tbl_Notlar.OgrenciID);
            return View(tbl_Notlar);
        }

        // GET: Tbl_Notlar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Notlar tbl_Notlar = db.Tbl_Notlar.Find(id);
            if (tbl_Notlar == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Notlar);
        }

        // POST: Tbl_Notlar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_Notlar tbl_Notlar = db.Tbl_Notlar.Find(id);
            db.Tbl_Notlar.Remove(tbl_Notlar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
