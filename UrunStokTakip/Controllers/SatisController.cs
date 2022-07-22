using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrunStokTakip.Models;
using PagedList;
using PagedList.Mvc;

namespace UrunStokTakip.Controllers
{
    public class SatisController : Controller
    {
        // GET: Satis
        MVC_StokTakipEntities1 db = new MVC_StokTakipEntities1();
        public ActionResult Index(int sayfa=1)
        {
            if (User.Identity.IsAuthenticated)
            {
                var kullaniciadi = User.Identity.Name;
                var kullanici = db.kullanici.FirstOrDefault(x => x.email == kullaniciadi);
                var model = db.satislar.Where(x => x.kullaniciID == kullanici.id).ToList().ToPagedList(sayfa, 5);
                return View(model);

            }
            return HttpNotFound();
        }

        public ActionResult SatinAl(int id) 
        {
            var model = db.sepet.FirstOrDefault(x => x.id == id);
            return View(model);
        }

        [HttpPost]

        public ActionResult SatinAl2(int id) 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = db.sepet.FirstOrDefault(x => x.id == id);

                    var satis = new satislar
                    {
                        kullaniciID = model.kullaniciID,
                        urunID = model.urunID,
                        adet = model.adet,
                        resim = model.resim,
                        fiyat = model.fiyat,
                        tarih = model.tarih,
                    };

                    db.sepet.Remove(model);
                    db.satislar.Add(satis);
                    db.SaveChanges();
                    ViewBag.islem = "Satın Alma İşleminiz Başarılı Bir Şekilde Gerçekleştirilmiştir.";


                }
            }

            catch (Exception)
            {
                ViewBag.islem = "Satın Alma Başarısız.";
            }

            return View("islem");
        }

        public ActionResult HepsiniSatinAl(decimal? tutar)
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

        public ActionResult HepsiniSatinAl2()
        {
            var kullaniciadi = User.Identity.Name;
            var kullanici = db.kullanici.FirstOrDefault(x => x.email == kullaniciadi);
            var model = db.sepet.Where(x => x.kullaniciID == kullanici.id).ToList();

            int satir = 0;
            foreach (var item in model)
            {
                var satis = new satislar
                {
                    kullaniciID = model[satir].kullaniciID,
                    urunID = model[satir].urunID,
                    adet = model[satir].adet,
                    fiyat = model[satir].fiyat,
                    resim = model[satir].urunler.resim,
                    tarih = DateTime.Now,
                };

                db.satislar.Add(satis);
                db.SaveChanges();
                satir++;

            }
            db.sepet.RemoveRange(model);
            db.SaveChanges();
            return RedirectToAction("Index", "Sepet");
        }

    }
}