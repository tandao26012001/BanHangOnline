using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;
using System.Data.Entity;


namespace WebBanHangOnline.Controllers
{
    public class ProductsController : Controller
    {
        private WebBanHangOnlineDbContext db = new WebBanHangOnlineDbContext();
        // GET: Products
        public ActionResult Index(string Searchtext, int? page, string sortOrder)
        {
            var pageSize = 12;
            int pageNumber = (page ?? 1);
            //if (page == null)
            //{
            //    page = 1;
            //}
            //IEnumerable<Product> items = db.Products.OrderByDescending(x => x.Id);
            //if (!string.IsNullOrEmpty(Searchtext))
            //{
            //items = items.Where(x => x.Alias.Contains(Searchtext) || x.Title.Contains(Searchtext));
            var _query = db.Products
                                .Where(p => string.IsNullOrEmpty(Searchtext) || p.Title.Contains(Searchtext))
                                .OrderBy(p => p.Id);
            //}
            // Xử lý sắp xếp
            switch (sortOrder)
            {
                case "price_desc":
                    _query = _query.OrderByDescending(p => p.PriceSale);
                    break;
                default:
                    _query = _query.OrderBy(p => p.PriceSale);
                    break;
            }
            // Tổng số sản phẩm tìm thấy (không phân trang)
            int totalProducts = _query.Count();
            //var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            // Phân trang sản phẩm
            var items = _query.ToPagedList(pageNumber, pageSize);
            //ViewBag.PageSize = pageSize;
            //ViewBag.Page = page;
            ViewBag.TotalProducts = totalProducts; // Tổng số sản phẩm tìm thấy
            ViewBag.CurrentCount = items.Count; // Số sản phẩm trên trang hiện tại
            ViewBag.CurrentSort = sortOrder; // Giữ lại thứ tự sắp xếp hiện tại
            return View(items);
        }

        public ActionResult Detail(/*string alias*/ int id)
        {
            var product = db.Products.Find(id);
            //var product = db.Products
            //    .Include(p=>p.Image)
            //    .FirstOrDefault(p => p.Id == id);
            product = db.Products.Include(p => p.ProductImages)
                                 .FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                db.Products.Attach(product);
                product.ViewCount = product.ViewCount + 1;
                db.Entry(product).Property(x => x.ViewCount).IsModified = true;
                db.SaveChanges();
            }
            ViewBag.RelatedProducts = db.Products.Where(x => x.Id != id && x.ProductCategoryId == product.ProductCategoryId).ToList();
            return View(product);
        }
        public ActionResult ProductCategory(string alias, int id)
        {
            var items = db.Products.ToList();
            if (id > 0)
            {
                items = items.Where(x => x.ProductCategoryId == id).ToList();
            }
            var cate = db.ProductCategories.Find(id);
            if (cate != null)
            {
                ViewBag.CateName = cate.Title;
            }

            ViewBag.CateId = id;
            return View(items);
        }

        public ActionResult Partial_ProductHots()
        {
            var items = db.Products.Where(x => x.IsActive && x.IsHot).Take(8).ToList();
            return PartialView(items);
        }
        public ActionResult Partial_ProductFeature()
        {
            var items = db.Products.Where(x => x.IsActive && x.IsFeature).Take(8).ToList();
            return PartialView(items);
        }
        public ActionResult Partial_ProductSales()
        {
            var items = db.Products.Where(x => x.IsSale && x.IsActive).Take(8).ToList();
            return PartialView(items);
        }
    }
}