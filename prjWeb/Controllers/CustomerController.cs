using prjWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjWeb.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer

        [HttpPost] //模稜兩可使用
        public ActionResult Edit(CCustomer p)  //接收Edit的form表(Post)
        {
            (new CCustomerFactory()).update(p);
            return RedirectToAction("List");
        }

        public ActionResult Edit(int? id)  //只要在另一個Edit下面就好，也可以放在更下面
        {
            if (id == null)
                return RedirectToAction("List");
            CCustomer x = new CCustomerFactory().queryById((int)id);
            return View(x);
        }

        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                new CCustomerFactory().delete((int)id);
            }
            return RedirectToAction("List");
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Save()
        {
            CCustomer x = new CCustomer();
            x.fName = Request.Form["txtName"];
            x.fPhone = Request.Form["txtPhone"];
            x.fEmail = Request.Form["txtEmail"];
            x.fAddress = Request.Form["txtAddress"];
            x.fPassword = Request.Form["txtPassword"];
            new CCustomerFactory().create(x);
            return RedirectToAction("List");   //開啟List.cshtml
        }
        public ActionResult List()
        {
            List<CCustomer> datas = new CCustomerFactory().queryAll();
            return View(datas);   //強型別繫結
        }
        public ActionResult demoForm()
        {
            ViewBag.ANS = "?";
            if (!string.IsNullOrEmpty(Request.Form["txtA"]) && !string.IsNullOrEmpty(Request.Form["txtB"]))
            {
                double a = Convert.ToDouble(Request.Form["txtA"]);
                double b = Convert.ToDouble(Request.Form["txtB"]);
                double c = Convert.ToDouble(Request.Form["txtC"]);
                ViewBag.A = a;
                ViewBag.B = b;
                ViewBag.C = c;
                double r = (b * b) - (4 * a * c);
                r = System.Math.Sqrt(r);
                double a1 = ((-b + r) / (2 * a));
                double a2 = ((-b - r) / (2 * a));
                ViewBag.ANS = $"{a1}or{a2}";
            }
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}

