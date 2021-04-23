using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.EntityContext;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class FavoriteController : Controller
    {
        // GET: Favorite
        public MusicStoreEntities db = new MusicStoreEntities();
        [Authorize]
        public ActionResult Index()
        {
            var lst = db.FavoriteAlbums.Where(f => f.UserName == User.Identity.Name).ToList();
            return View(lst);
        }

        [Authorize]
        public ActionResult Like(int? id, string returnUrl) {
            if (id == null) {
                return RedirectToAction("Index", "Home");
            }
            if (db.FavoriteAlbums.Where(f => f.AlbumId == id && f.UserName == User.Identity.Name).FirstOrDefault() != null) {
                return RedirectToAction("Details", "Store", new { id = id });
            }
            FavoriteAlbum favor = new FavoriteAlbum();
            favor.AlbumId = Convert.ToInt32(id);
            favor.UserName = User.Identity.Name;
            db.FavoriteAlbums.Add(favor);
            db.SaveChanges();
            if (string.IsNullOrEmpty(returnUrl)) {
                return RedirectToAction("Details", "Store", new { id = id});
            }
            return Redirect(returnUrl);
        }

        [Authorize]
        public ActionResult UnLike(int? id, string returnUrl)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var favor = db.FavoriteAlbums.Where(f => f.AlbumId == id && f.UserName == User.Identity.Name).FirstOrDefault();
            if (favor == null)
            {
                return RedirectToAction("Details", "Store", new { id = id });
            }
            db.FavoriteAlbums.Remove(favor);
            db.SaveChanges();
            if (string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction("Details", "Store", new { id = id });
            }
            return Redirect(returnUrl);
        }

    }
}