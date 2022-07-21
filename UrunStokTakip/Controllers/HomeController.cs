using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UrunStokTakip.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        //otomatik çıkış yapma url eklentisi
        [Authorize]


        public ActionResult Index()
        {
            return View();
        }
    }
}