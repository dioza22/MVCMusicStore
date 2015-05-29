using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCMusicStore.Models;

namespace MVCMusicStore.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        MusicStoreEntities db = new MusicStoreEntities();

        [AllowAnonymous]
        public ActionResult Index()
        {
            var albums = GetTopSellingAlbums(10);
            
            return View(albums);
        }
        
        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private List<Album> GetTopSellingAlbums(int count)
        {
            return db.Albums.OrderByDescending(album => album.OrderDetails.Count())
                .Take(count)
                .OrderBy(a => a.Title)
                .ToList();
        }
    }
}