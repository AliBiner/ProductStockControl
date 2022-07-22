using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using UrunStokTakip.Models;

namespace UrunStokTakip.Controllers
{
    public class KullaniciController : Controller
    {
        // GET: Kullanici
        MVC_StokTakipEntities1 db = new MVC_StokTakipEntities1();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SifreReset() {
            return View();
        }

        [HttpPost]

        public ActionResult SifreReset(string eposta)
        {
            var mail = db.kullanici.Where(x => x.email == eposta).SingleOrDefault();
            if (mail != null)
            {
                Random rnd = new Random();
                int yenisifre = rnd.Next();
                kullanici sifre = new kullanici();
                mail.sifre = Crypto.Hash(Convert.ToString(yenisifre), "MD5");
                db.SaveChanges();
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "ali.bnr.63@gmail.com";
                WebMail.Password = "341exp341";
                WebMail.SmtpPort = 587;
                WebMail.Send(eposta, "Giriş Şifreniz", "Şifreniz" + yenisifre);
                ViewBag.uyari = "Şifreniz Başarıyla Gönderilmiştir.";
            }

            else
            {
                ViewBag.uyari = "Hata Oluştu Tekrar Deneyiniz.";
            }

            return View();
        }
    }
}