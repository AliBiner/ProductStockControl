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
        public ActionResult Index()
        {
            return View();
        }
    }
}