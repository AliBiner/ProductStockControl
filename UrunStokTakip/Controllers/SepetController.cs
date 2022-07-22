using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrunStokTakip.Models;

namespace UrunStokTakip.Controllers
{
    public class SepetController : Controller
    {
        // GET: Sepet

        MVC_StokTakipEntities1 db = new MVC_StokTakipEntities1();
        public ActionResult Index(decimal?tutar)
        {
            if (User.Identity.IsAuthenticated)
            {
                var kullaniciadi = User.Identity.Name;
                var kullanici = db.kullanici.FirstOrDefault(x => x.email == kullaniciadi);
                var model = db.sepet.Where(x => x.kullaniciID == kullanici.id).ToList();
                var kid = db.sepet.FirstOrDefault(x => x.kullaniciID == kullanici.id);

                if (model != null)
                {

                    if (kid == null)
                    {
                        ViewBag.tutar = "Sepette Ürün Bulunmamaktadır.";
                    }

                    else if (kid != null)
                    {
                        tutar = db.sepet.Where(x => x.kullaniciID == kid.kullaniciID).Sum(x => x.urunler.fiyat * x.adet);
                        ViewBag.tutar = "Toplam Tutar = " + tutar + " TL";

                    }
                    return View(model);
                }
                return View();
                

            }
            return HttpNotFound();

        }

        public ActionResult SepeteEkle(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var kullaniciadi = User.Identity.Name;
                var model = db.kullanici.FirstOrDefault(x => x.email == kullaniciadi);
                var u = db.urunler.Find(id);
                var sepet = db.sepet.FirstOrDefault(x => x.kullaniciID == model.id && x.urunID == id);

                if (model!=null)
                {
                    if (sepet != null)
                    {
                        sepet.adet++;
                        sepet.fiyat = u.fiyat * sepet.adet;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    var s = new sepet
                    {
                        kullaniciID = model.id,
                        urunID = u.id,
                        adet = 1,
                        fiyat = u.fiyat,
                        tarih = DateTime.Now

                    };
                    db.sepet.Add(s);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                return View();

 
            }
            return HttpNotFound();
        }

        public ActionResult SepetCount(int?count)
        {
            if (User.Identity.IsAuthenticated)
            {
                var model = db.kullanici.FirstOrDefault(x => x.email == User.Identity.Name);
                count = db.sepet.Where(x => x.kullaniciID == model.id).Count();
                ViewBag.count = count;
                if (count == 0)
                {
                    ViewBag.count = "";
                }
                return PartialView();
            }
            return HttpNotFound();
        }

        public ActionResult arttir(int id)
        {
            var model = db.sepet.Find(id);
            model.adet++;
            model.fiyat = model.fiyat * model.adet;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult azalt(int id)
        {
            var model = db.sepet.Find(id);
            if (model.adet == 1)
            {
                db.sepet.Remove(model);
                db.SaveChanges();
            }

            model.adet--;
            model.fiyat = model.fiyat * model.adet;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void AdetYaz(int id , int miktari) 
        {
            var model = db.sepet.Find(id);
            
            model.adet = miktari;
            model.fiyat = model.fiyat * model.adet;
            db.SaveChanges();

        }

        public ActionResult Sil(int id)
        {
            var sil = db.sepet.Find(id);
            db.sepet.Remove(sil);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult HepsiniSil() 
        {
            if (User.Identity.IsAuthenticated)
            {
                var kullaniciadi = User.Identity.Name;
                var model = db.kullanici.FirstOrDefault(x => x.email == kullaniciadi);
                var sil = db.sepet.Where(x => x.kullaniciID == model.id);
                db.sepet.RemoveRange(sil);
                db.SaveChanges();
                return RedirectToAction("Index");


            }
            return HttpNotFound();
        }
    }
}