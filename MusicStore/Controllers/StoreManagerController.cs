using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MusicStore.Models;
using MusicStore.EntityContext;
using System.IO;

namespace MusicStore.Controllers {
    [Authorize(Roles = "Administrator")]
    public class StoreManagerController : Controller {
        private MusicStoreEntities db = new MusicStoreEntities();

        public ActionResult Index(string searchString, string page)
        {
            
            var albums = db.Albums.Include(a => a.Artist).Include(a => a.Genre);
            if (!string.IsNullOrEmpty(searchString))
            {
                albums = albums.Where(a => a.Title.Contains(searchString));
                ViewBag.SearchString = searchString;
            }
            if (string.IsNullOrEmpty(page))
            {
                page = "1";
            }
            int curPage = Convert.ToInt32(page);
            int amountForEach = 15;
            int cntPage = albums.Count() / amountForEach;
            if (albums.Count() % amountForEach != 0) {
                cntPage += 1;
            }
            ViewBag.CountPage = cntPage;
            ViewBag.CurrentPage = page;
            //System.Diagnostics.Debug.WriteLine(page);
            //System.Diagnostics.Debug.WriteLine(curPage);
            //System.Diagnostics.Debug.WriteLine(Convert.ToInt32(curPage - 1));
            var albumPage = albums.ToList().ToArray().Skip((curPage - 1) * amountForEach).Take(amountForEach);

            return View(albumPage.ToList());
        }

        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null) {
                return HttpNotFound();
            }
            return View(album);
        }

        public ActionResult Create() {
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name");
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlbumId,GenreId,ArtistId,Title,Price,AlbumArtUrl,mp3FileName")] Album album, HttpPostedFileBase file, HttpPostedFileBase mp3) {
            if (ModelState.IsValid) {
                try
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        System.Diagnostics.Debug.Write("file length: " + file.ContentLength);
                        string _FileName = Path.GetFileName(file.FileName);
                        string _path = Path.Combine(Server.MapPath("~/ImagesAlbum"), _FileName);
                        file.SaveAs(_path);
                        album.AlbumArtUrl = _FileName;
                    }

                    if (mp3 != null && mp3.ContentLength > 0)
                    {
                        System.Diagnostics.Debug.Write("file length: " + mp3.ContentLength);
                        string _FileName = Path.GetFileName(mp3.FileName);
                        string _path = Path.Combine(Server.MapPath("~/MP3"), _FileName);
                        mp3.SaveAs(_path);
                        album.mp3FileName = _FileName;
                    }
                }
                catch
                {
                }
                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
            return View(album);
        }
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null) {
                return HttpNotFound();
            }
            Session["OldAlbumId"] = album.AlbumId;
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
            return View(album);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlbumId,GenreId,ArtistId,Title,Price,AlbumArtUrl,mp3FileName")] Album album, HttpPostedFileBase file, HttpPostedFileBase mp3)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        System.Diagnostics.Debug.Write("file length: " + file.ContentLength);
                        string _FileName = Path.GetFileName(file.FileName);
                        string _path = Path.Combine(Server.MapPath("~/ImagesAlbum"), _FileName);
                        file.SaveAs(_path);
                        album.AlbumArtUrl = _FileName;
                    }

                    if (mp3 != null && mp3.ContentLength > 0)
                    {
                        System.Diagnostics.Debug.Write("file length: " + mp3.ContentLength);
                        string _FileName = Path.GetFileName(mp3.FileName);
                        string _path = Path.Combine(Server.MapPath("~/MP3"), _FileName);
                        mp3.SaveAs(_path);
                        album.mp3FileName = _FileName;
                    }
                }
                catch
                {
                }
                System.Diagnostics.Debug.WriteLine(album.mp3FileName);
                album.AlbumId = Convert.ToInt32(Session["OldAlbumId"]);
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
            return View(album);
        }
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null) {
                //404方法
                return HttpNotFound();
            }
            return View(album);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
