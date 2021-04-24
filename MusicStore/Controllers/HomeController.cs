using MusicStore.EntityContext;
using MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.ViewModels;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Text;
namespace MusicStore.Controllers {
    public class HomeController : Controller {
        private String AlbumSessionKey = "AlbumId";
        MusicStoreEntities storeDB = new MusicStoreEntities();
        //
        // GET: /Home/
        public ActionResult Index() {
            return View();
        }
        public ActionResult Shop() {
            ViewBag.IsAtHome = true;
            // Get most popular albums
            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.ViewedAlbums = GetViewedAlbums(5);
            homeViewModel.RandomAlbums = GetRandomAlbums(5);
            homeViewModel.TopSellingAlbums = GetTopSellingAlbums(5);
            return View(homeViewModel);
        }

        public List<Album> GetViewedAlbums(int cnt) {
            var id = Session[AlbumSessionKey];

            var viewedAlbums = new List<Album>();
            if (id != null) {
                var indexString = id.ToString().Split(',');
                for (int i = indexString.Count() - 1; i >= 0; i--) {
                    var index = indexString.ElementAt(i);
                    if (cnt == 0) break;
                    viewedAlbums.Add(storeDB.Albums.Find(Convert.ToInt32(index)));
                    cnt--;
                }
            }
            return viewedAlbums;
        }
        public List<Album> GetTopSellingAlbums(int count) {
            // Group the order details by album and return
            // the albums with the highest count
            return storeDB.Albums.OrderByDescending(a => a.OrderDetails.Count()).Take(count).ToList();
        }

        public List<Album> GetRandomAlbums(int count) {
            // Group the order details by album and return
            // the albums with the highest count
            return storeDB.Albums.Take(count).ToList();
        }

        public PartialViewResult RightContent() {
            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.ViewedAlbums = GetViewedAlbums(5);
            homeViewModel.RandomAlbums = GetRandomAlbums(5);
            homeViewModel.TopSellingAlbums = GetTopSellingAlbums(5);
            return PartialView(homeViewModel);
        }

        public ActionResult AboutUs() {
            return View();
        }
        public ActionResult Testmonial() {
            return View();
        }
        public ActionResult Gallery() {
            return View();
        }
        public ActionResult Blog() {
            return View();
        }

        public ActionResult ErrorPage() {
            return View();
        }
        public ActionResult ContactUs() {
            return View();
        }
        [HttpPost]
        public ActionResult ContactUs(string name, string email, string subject, string message) {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(message)) {
                return View("ErrorPage");
            }
            else if (name != null && email != null && subject != null && message != null) {
                if (name.Length < 2 || subject.Length < 2 || message.Length < 2 || email.Length < 10) {
                    return View("ErrorPage");
                }
                else {
                    try {
                        string subtile = "";
                        string receiver = "longdnk18@uef.edu.vn";
                        if (ModelState.IsValid) {
                            var senderEmail = new MailAddress("kimlong101020@gmail.com", "Guestt");
                            var receiverEmail = new MailAddress(receiver, "Receiver");
                            var password = "KimLong10102030";
                            subtile += (name + " " + email + " " + subject);
                            var sub = subtile;
                            var body = message;
                            var smtp = new SmtpClient
                            {
                                Host = "smtp.gmail.com",
                                Port = 587,
                                EnableSsl = true,
                                DeliveryMethod = SmtpDeliveryMethod.Network,
                                UseDefaultCredentials = false,
                                Credentials = new NetworkCredential(senderEmail.Address, password)
                            };
                            using (var mess = new MailMessage(senderEmail, receiverEmail)
                            {
                                Subject = sub,
                                Body = body
                            }) {
                                smtp.Send(mess);
                            }
                            return View("Index");
                        }
                    }
                    catch (Exception) {
                        ViewBag.Error = "Some Error";
                        return View("ErrorPage");
                    }
                }
            }
            return View("Index");
        }
        public ActionResult ContactByMail() {
            return View("Index");
        }
        [HttpPost]
        public ActionResult ContactByMail(string subscribe) {
            if (string.IsNullOrEmpty(subscribe)) {
                return View("ErrorPage");
            }
            else if (subscribe.Length <10) {
                return View("ErrorPage");
            }
            else {
                try {
                    string message = "";
                    string fi = "Hello i'm";
                    string se = "please add me";
                    string str = "to V.I.P membership";
                    string subtile = "";
                    string receiver = "longdnk18@uef.edu.vn";
                    if (ModelState.IsValid) {
                        message += (fi + " " + subscribe + " " + se + " " + str);
                        var senderEmail = new MailAddress("kimlong101020@gmail.com", "Guestt");
                        var receiverEmail = new MailAddress(receiver, "Receiver");
                        var password = "KimLong10102030";
                        subtile += str;
                        var sub = subtile;
                        var body = message;
                        var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(senderEmail.Address, password)
                        };
                        using (var mess = new MailMessage(senderEmail, receiverEmail)
                        {
                            Subject = sub,
                            Body = body
                        }) {
                            smtp.Send(mess);
                        }
                        return View("Index");
                    }
                }
                catch (Exception) {
                    ViewBag.Error = "Some Error";
                    return View("ErrorPage");
                }
            }
            return View("Index");
        }
    }
}