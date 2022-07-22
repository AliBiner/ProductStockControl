using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrunStokTakip.Models;

namespace UrunStokTakip.Controllers
{
    public class IstatistikController : Controller
    {
        // GET: Istatistik
        MVC_StokTakipEntities1 db = new MVC_StokTakipEntities1();
        public ActionResult Index()
        {
            var satis = db.satislar.Count();
            ViewBag.satis = satis;

            var urun = db.urunler.Count();
            ViewBag.urun = urun;

            var kategori = db.kategori.Count();
            ViewBag.kategori = kategori;

            var sepet = db.sepet.Count();
            ViewBag.sepet = sepet;

            var kuklanici = db.kullanici.Count();
            ViewBag.kullanici = kuklanici;

            return View();
        }
    }
}