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
    public class TumSatislarController : Controller
    {
        // GET: TumSatislar
        MVC_StokTakipEntities1 db = new MVC_StokTakipEntities1();
        [Authorize (Roles ="A")]
        public ActionResult Index(int sayfa =1)
        {

            return View(db.satislar.ToList().ToPagedList(sayfa , 5));
        }
    }
}