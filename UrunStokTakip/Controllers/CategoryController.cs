using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrunStokTakip.Models;

namespace UrunStokTakip.Controllers
{
    [Authorize(Roles = "A")]
    public class CategoryController : Controller
    {
        // GET: Category
        MVC_StokTakipEntities1 db = new MVC_StokTakipEntities1();

        
        public ActionResult Index()
        {
            return View(db.kategori.Where(x => x.durum == true).ToList());
        }

        
        public ActionResult Ekle()
        {
            return View();

        }

        [HttpPost]
        

        public ActionResult Ekle(kategori data)
        {
            db.kategori.Add(data);
            data.durum = true;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        
        public ActionResult Sil(int id)
        {
            var kategori = db.kategori.Where(x => x.id == id).FirstOrDefault();
            kategori.durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        
        public ActionResult Guncelle()
        {
            
            return View();

        }
        
        [HttpPost]

        public ActionResult Guncelle(kategori data)
        {
            var guncelle = db.kategori.Where(x => x.id == data.id).FirstOrDefault();
            guncelle.ad = data.ad;
            guncelle.aciklama = data.aciklama;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}