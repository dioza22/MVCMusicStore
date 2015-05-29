using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCMusicStore.Models;

namespace MVCMusicStore.Controllers
{
    [RequireHttps]
    public class StoreController : Controller
    {
        MusicStoreEntities db = new MusicStoreEntities();

        // GET: Store
        [AllowAnonymous]
        public ActionResult Index()
        {
            var genres = db.Genres.ToList();

            return View(genres);
        }

        //GET /Store/Browse
        [AllowAnonymous]
        public ActionResult Browse(string genre)
        {
            var genreModel = db.Genres.Include("Albums").Single(g => g.Name == genre);

            return View(genreModel);
        }

        //GET Store/Detail
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var album = db.Albums.Find(id);

            return View(album);
        }

        // GET: /Store/GenreMenu
        [ChildActionOnly]
        [AllowAnonymous]
        public ActionResult GenreMenu()
        {
            var genres = db.Genres.OrderBy(g => g.Name).ToList();
            return PartialView(genres);
        }
    }
}