using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicStore.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult MyIndex() {
            return View();
        }
        public ActionResult Login() {
            return View();
        }
        public ActionResult Signup() {
            return View();
        }
        public ActionResult Gallery() {
            return View();
        }
        public ActionResult Blog() {
            return View();
        }
        public ActionResult AboutUs() {
            return View();
        }
        public ActionResult Testmonial() {
            return View();
        }
        public ActionResult Shop() {
            return View();
        }
        public ActionResult ContactUs() {
            return View();
        }
        public ActionResult DashBoard() {
            return View();
        }
    }
}