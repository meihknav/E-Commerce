using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBHDuongVanKhiem2054052026.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }

        //datacontext
        public NorthWindDataContext da = new NorthWindDataContext();

        public ActionResult ListProducts()
        {
            List<Product> ds = da.Products.Select(s => s).ToList();
            return View(ds);
        }

        // GET: Products/Details/5

        public ActionResult Details(int id)
        {
            Product p = da.Products.Where(s => s.ProductID == id).FirstOrDefault();
            //var product = from p in da.Products
            //        where p.ProductID == id
            //        select p;
            return View(p);
        }

        // GET: Products/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewData["NCC"] = new SelectList(da.Suppliers, "SupplierID", "CompanyName");
            ViewData["LoaiSP"] = new SelectList(da.Categories, "CategoryID", "CategoryName");
            return View();
        }

        // POST: Products/Create
       [HttpPost]
        public ActionResult Create(Product product,FormCollection collection)
        {
            try
            {
                Product p = product;
                p.SupplierID = int.Parse(collection["NCC"]);
                p.CategoryID = int.Parse(collection["LoaiSP"]);
                da.Products.InsertOnSubmit(p);
                da.SubmitChanges();
                // TODO: Add insert logic here

                return RedirectToAction("ListProducts");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            ViewData["NCC"] = new SelectList(da.Suppliers, "SupplierID", "CompanyName");
            ViewData["LoaiSP"] = new SelectList(da.Categories, "CategoryID", "CategoryName");
            //Lay san pham muon sua
            Product p = da.Products.FirstOrDefault(s => s.ProductID == id);
            //truyen model xuong view
            return View(p);
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(int id,Product product)
        {
            try
            {
                // TODO: Add update logic here
                //Tao mot san pham can sua
                Product p = da.Products.FirstOrDefault(s => s.ProductID == id);
                if (!string.IsNullOrEmpty(product.ProductName))
                {
                p.ProductName = product.ProductName;
                p.UnitPrice = product.UnitPrice;
                p.UnitsInStock = product.UnitsInStock;
                p.UnitsOnOrder = product.UnitsOnOrder;

                //luu vao database
                da.SubmitChanges();
                return RedirectToAction("ListProducts");
                }
                else
                {
                    return View();
                }
               
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            Product p = da.Products.FirstOrDefault(s => s.ProductID == id);

            return View(p);
        }

        // POST: Products/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Product p = da.Products.FirstOrDefault(s => s.ProductID == id);
                da.Products.DeleteOnSubmit(p);
                da.SubmitChanges();
                return RedirectToAction("ListProducts");
            }
            catch
            {
                return View();
            }
        }
    }
}
