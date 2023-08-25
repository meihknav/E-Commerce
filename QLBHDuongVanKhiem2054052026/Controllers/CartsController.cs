using QLBHDuongVanKhiem2054052026.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Transactions;
using QLBHDuongVanKhiem2054052026;

namespace QLBHDuongVanKhiem2054052026.Controllers
{
    public class CartsController : Controller
    {
        NorthWindDataContext da = new NorthWindDataContext();
        private List<CartModel> GetListCarts()
        {
            List<CartModel> carts = Session["Cart"] as List<CartModel>;
            if (carts == null)
            {
                carts = new List<CartModel>();
                Session["Cart"] = carts;
            }
            return carts;
        }

        // GET: Cart
        public ActionResult ListCarts()//Hien thi dssp trong gio hang
        {
            List<CartModel> carts = GetListCarts();
            ViewBag.CountProduct = carts.Sum(s => s.Quantity);
            ViewBag.Total = carts.Sum(s => s.UnitPrice);
            return View(carts);
        }

        // GET: Cart
        public ActionResult AddCart(int id)//Them 1 SP vao trong gio hang
        {
            List<CartModel> carts = GetListCarts();
            //1.Tao moi 1 SP cart
            CartModel c = new CartModel(id);
            CartModel cget = carts.Find(s => s.ProductID == id);
            //Kiem tra c co trong gio hang chua
            if (cget == null)
            //Chua co them moi 
            {
                carts.Add(c);
            }
            //Co roi tang so luong
            else
            {
                cget.Quantity++;
            }
            //Hien thi lai dssp trong gio hang
            return RedirectToAction("ListCarts");

        }
        public ActionResult OrderProduct()
        {
            using (TransactionScope tranScope = new TransactionScope())
            {
                try
                {
                    //1. Tao moi 1 don hang
                    //1.1. Tao doi tuong order
                    Order order = new Order();
                    //1.2. Gan thuoc tinh cho order
                    order.OrderDate = DateTime.Now;
                    order.EmployeeID = 1;
                    //1.3. Add order vao bang Orders
                    da.Orders.InsertOnSubmit(order);
                    //1.4. Cap nhat database
                    da.SubmitChanges();

                    //2. Add cac sp vao OrderDetails
                    //2.1. Duyet tung Sp trong gio hang, add sp do vao trong OrderDetails
                    foreach (CartModel item in GetListCarts())
                    {
                        //Tao moi 1 orderdetails
                        Order_Detail orderDetail = new Order_Detail();
                        //gan thuoc tinh cho orderdetail
                        orderDetail.OrderID = order.OrderID;
                        orderDetail.ProductID = item.ProductID;
                        orderDetail.UnitPrice = decimal.Parse(item.UnitPrice.ToString());
                        orderDetail.Quantity = short.Parse(item.Quantity.ToString());
                        orderDetail.Discount = 0;
                        //Add order vao bang orderdetails
                        da.Order_Details.InsertOnSubmit(orderDetail);
                    }
                    //2.2. Cap nhat database
                    da.SubmitChanges();
                    tranScope.Complete();
                }
                catch (Exception)
                {
                    tranScope.Dispose();

                }
            }
            //hien thi DS cac don hang
            return RedirectToAction("ListOrders");
        }
        public ActionResult ListOrders()
        {
            var ds = da.Orders.OrderByDescending(s => s.OrderDate).ToList();
            return View(ds);
        }
    }
}