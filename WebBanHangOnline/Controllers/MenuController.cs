﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Controllers
{
    public class MenuController : Controller
    {
        private WebBanHangOnlineDbContext db = new WebBanHangOnlineDbContext();
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MenuTop()
        {
            var items = db.Categories.OrderBy(x=>x.Position).ToList();
            return PartialView("_MenuTop", items);
        }

        public ActionResult MenuProductCategory()
        {
            var items = db.ProductCategories.ToList();
            return PartialView("_MenuProductCategory", items);
        }
        public ActionResult MenuLeft(int? id)
        {
            if (id != null)
            {
                ViewBag.CateId = id;
            }
            var items = db.ProductCategories.ToList();
            return PartialView("_MenuLeft", items);
        }
        public ActionResult MenuArrivals()
        {
            var items = db.ProductCategories.ToList();
            return PartialView("_MenuArrivals", items);
        }
        public ActionResult MenuSpecialOffer()
        {
            var items = db.SpecialOffers.ToList();
            return PartialView("_MenuSpecialOffer", items);
        }
        public ActionResult MenuSlides()
        {
            var items = db.Slides.ToList();
            return PartialView("_MenuSlides", items);
        }
        public ActionResult MenuBanners()
        {
            var items = db.Banners.ToList();
            return PartialView("_MenuBanners", items);
        }
    }
}