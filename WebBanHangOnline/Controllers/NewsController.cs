using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Controllers
{
    public class NewsController : Controller
    {
        private WebBanHangOnlineDbContext db = new WebBanHangOnlineDbContext();
        // GET: News
        public ActionResult Index(int? page)
        {
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<News> items = db.News.OrderByDescending(x=>x.CreatedDate);
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }
        public ActionResult Detail(int id)
        {
            var item = db.News.Find(id);
            ViewBag.RelatedNews = db.News.Where(x=>x.CategoryId == id).Take(6).ToList();
            return View(item);
        }
        public ActionResult Partial_News_Home()
        {
            var items = db.News.Where(x=>x.IsActive).Take(4).ToList();
            return PartialView(items);
        }
    }
}