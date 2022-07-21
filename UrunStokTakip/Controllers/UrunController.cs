using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrunStokTakip.Models;
using System.Data.Entity.Validation;
using System.Runtime.Remoting.Contexts;
using System.Collections;
using System.Web.Helpers;

namespace UrunStokTakip.Controllers
{

public class UrunController : Controller
    {
        // GET: Urun

        MVC_StokTakipEntities1 db = new MVC_StokTakipEntities1();

        [Authorize]
        public ActionResult Index(string ara)
        {
            var list = db.urunler.ToList();
            if (!string.IsNullOrEmpty(ara))
            {
                list = list.Where(x => x.ad.Contains(ara) || x.aciklama.Contains(ara)).ToList();
            }
            return View(list);
        }

        [Authorize(Roles ="A")]
        public ActionResult Ekle() {
            List<SelectListItem> deger1 = (from x in db.kategori.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.ad,
                                               Value = x.id.ToString()
                                           }).ToList();

            ViewBag.ktgr = deger1;
            return View();

        }
        [Authorize(Roles = "A")]
        [HttpPost]

        public ActionResult Ekle(urunler Data, HttpPostedFileBase File) {

            string path = Path.Combine("~/Content/Icon" + File.FileName);
            File.SaveAs(Server.MapPath(path));
            Data.resim = File.FileName.ToString();
            db.urunler.Add(Data);
            db.SaveChanges();
            return RedirectToAction("Index");
        
        }
        [Authorize(Roles = "A")]
        public ActionResult Sil(int id)
        {
            var urun = db.urunler.Where(x => x.id == id).FirstOrDefault();
            db.urunler.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        [Authorize(Roles = "A")]
        public ActionResult Guncelle(int id , HttpPostedFileBase File )
        {
            var guncelle = db.urunler.Where(x => x.id == id).FirstOrDefault();
            List<SelectListItem> deger1 = (from x in db.kategori.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.ad,
                                               Value = x.id.ToString()
                                           }).ToList();

            ViewBag.ktgr = deger1;
            return View(guncelle);

        }
        [Authorize(Roles = "A")]
        [HttpPost]
        
        public ActionResult Guncelle(urunler model , HttpPostedFileBase File)
        {
            var urun = db.urunler.Find(model.id);


            if (File == null)
            {
                urun.aciklama = model.aciklama;
                urun.ad = model.ad;
                urun.fiyat = model.fiyat;
                urun.stok = model.stok;
                urun.populer = model.populer;
                urun.kategoriID = model.kategoriID;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                urun.resim = File.FileName.ToString();
                urun.aciklama = model.aciklama;
                urun.ad = model.ad;
                urun.fiyat = model.fiyat;
                urun.stok = model.stok;
                urun.populer = model.populer;
                urun.kategoriID = model.kategoriID;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            

           

            
        }

        [Authorize(Roles = "A")]
        public ActionResult KritikStok()
        {
            var kritik = db.urunler.Where(x => x.stok <= 5).ToList();
            return View(kritik);
        }

        public PartialViewResult StokCount()
        {
            if (User.Identity.IsAuthenticated)
            {
                var count = db.urunler.Where(x => x.stok < 5).Count();
                ViewBag.count = count;
                var azalan = db.urunler.Where(x => x.stok == 5).Count();
                ViewBag.azalan = azalan;

            }
            return PartialView();

        }

        public ActionResult StokGrafik()
        {
            ArrayList deger1 = new ArrayList();
            ArrayList deger2 = new ArrayList();
            var veriler = db.urunler.ToList();
            veriler.ToList().ForEach(x => deger1.Add(x.ad));
            veriler.ToList().ForEach(x => deger2.Add(x.stok));
            var grafik = new Chart(width: 500, height: 500).AddTitle("Ürün Stok Grafiği").AddSeries(chartType:"Column" , name:"ad" , xValue:deger1 , yValues:deger2);

            return File(grafik.ToWebImage().GetBytes(), "image/jpeg");

           

        }

    }


}

