using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UrunStokTakip.Models;

namespace UrunStokTakip.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        MVC_StokTakipEntities1 db = new MVC_StokTakipEntities1();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();

        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]

        public ActionResult Login(kullanici p)
        {
            var bilgiler = db.kullanici.FirstOrDefault(x => x.email == p.email && x.sifre == p.sifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.email, false);
                Session["Mail"] = bilgiler.email.ToString();
                Session["Ad"] = bilgiler.ad.ToString();
                Session["Soyadı"] = bilgiler.soyadı.ToString();
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ViewBag.hata = "Kullanıcı Adı veya Şifre Yanlış";

            }
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Register(kullanici data)
        {
            db.kullanici.Add(data);
            data.rol = "U";
            db.SaveChanges();



            return RedirectToAction("Login", "Account");
        }

        public ActionResult Guncelle()
        {
            var kullanicilar = (string)Session["mail"];
            var degerler = db.kullanici.FirstOrDefault(x => x.email == kullanicilar);
            return View(degerler);
        
        }

        [HttpPost]

        public ActionResult Guncelle(kullanici data)
        {
            var kullanicilar = (string)Session["mail"];
            var kullanici = db.kullanici.Where(x => x.email == kullanicilar).FirstOrDefault();
            kullanici.ad = data.ad;
            kullanici.soyadı = data.soyadı;
            kullanici.email = data.email;
            kullanici.kullaniciAd = data.kullaniciAd;
            kullanici.sifre = data.sifre;
            kullanici.sifreTekrar = data.sifreTekrar;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");

        }
    }
}