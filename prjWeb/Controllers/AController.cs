using prjWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjWeb.Controllers
{
    public class AController : Controller
    {
        // GET: A
        public ActionResult Index()
        {
            return View();
        }

        public String SayHello()
        {
            return "Hello ASP.NET!!";
        }

        //樂透
        public string lotto()
        {
            Class1 aa = new Class1();
            return aa.getnumber();
        }


        //傳回伺服器位置
        public string demoServer()
        {
            return "目前伺服器上的實體位置：" + Server.MapPath(".");
        }


        //response
        public string demoResponse()
        {
            Response.Clear();                              //連接到網頁可以下載這個jpg
            Response.WriteFile(@"C:\Users\iSpan\OneDrive\MVC\ASP\pjWebDemo\pjWebDemo\image\123DEMO.jpg");
            Response.Filter.Close();
            Response.WriteFile(@"C:\Users\iSpan\OneDrive\MVC\ASP\123DEMO.jpg");
            Response.End();
            return "";
        }

        //request
        public string demorequest()
        {
            string id = Request.QueryString["pid"];
            if (id == "0")
                return "xbox 加入購物車成功";
            else if (id == "1")
                return "ps5 加入購物車成功";
            else if (id == "2")
                return "switch 加入購物車成功";
            return "找不到產品";
        }


        //[NonAction]
        public string demoParameter(int? id)  //使用Parameter方式傳參數不能null，可以加?，就可以null
        {                                                             //取名id，demoParameter?id=2
                                                                      //string id = Request.QueryString["productId"];
            if (id == 0)
                return "XBox 加入購物車成功";
            else if (id == 1)
                return "PS5 加入購物車成功";
            else if (id == 2)
                return "Switch 加入購物車成功";
            else
                return "找不到該產品資料";
        }


        //會員資料連結實作
        public string queryById(int? id)
        {
            if (id == null)
                return "請指定要查詢的id";
            SqlConnection con = new SqlConnection();        //連接資料庫
            con.ConnectionString = @"Data Source=.;Initial Catalog=MVCdbDemo;Integrated Security=True";
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from tCustomer where fid = " + id.ToString(), con);
            SqlDataReader reader = cmd.ExecuteReader();
            string s = "查無該筆資料";
            if (reader.Read())
                s = reader["fName"].ToString() + "<br/>" + reader["fPhone"].ToString();
            reader.Close();
            con.Close();

            return s;
        }


        //ActionRsult-新增view
        public ActionResult showById(int? id)
        {
            ViewBag.kk = "查無該筆資料";
            if (id == null)
                ViewBag.kk = "請指定要查詢的id";
            else
            {
                SqlConnection con = new SqlConnection();         //連接資料庫
                con.ConnectionString = @"Data Source=.;Initial Catalog=MVCdbDemo;Integrated Security=True";
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from tCustomer where fid = " + id.ToString(), con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    ViewBag.kk = reader["fName"].ToString();
                    ViewBag.jj = reader["fPhone"].ToString();
                    ViewBag.aa = reader["fAddress"].ToString();
                    ViewBag.ee = reader["fEmail"].ToString();
                    ViewBag.img = reader["fimg"].ToString();
                }
                reader.Close();
                con.Close();
            }

            return View();
        }


        //繫結物件範例-取得實體  TEST
        public ActionResult BindingbyID(int? id)
        {
            //ViewBag.PP = "查無該筆資料";
            CCustomer x = null;
            if (id != null)
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from tCustomer where fid=" + id.ToString(), con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    x = new CCustomer()
                    {
                        fId = reader["fid"].ToString(),
                        fName = reader["fname"].ToString(),
                        fPhone = reader["fphone"].ToString(),
                        fEmail = reader["femail"].ToString(),
                        fAddress = reader["faddress"].ToString()
                    };

                }
                con.Close();
            }
            return View(x);
        }

        //CRUD
        //public string testingInsert()
        //{
        //    CCustomer x = new CCustomer();
        //    x.fname = "Tom";
        //    x.faddress = "Tainan";
        //    //x.fphone = "09456789123";
        //    x.femail = "tom@gmail.com";
        //    //x.fpassword = "9527";
        //    (new CCustomerFactory()).create(x);
        //    return "新增資料成功";
        //}
        //public string testingdelete(int? id)
        //{
        //    if (id == null)
        //        return "請指定id";
        //    (new CCustomerFactory()).delete((int)id);
        //    return "刪除資料成功";
        //}
        //public string testingUpdate(int? id)
        //{
        //    if (id == null)
        //        return "請指定id";
        //    CCustomer x = new CCustomer();
        //    x.fid = (int)id;
        //    //x.fname = "Tom";
        //    x.faddress = "Tainan";
        //    x.fphone = "09456789125";
        //    x.femail = "Tom@gmail.com";
        //    x.fpassword = "9527";
        //    (new CCustomerFactory()).update(x);
        //    return "修改資料成功";
        //}
        public string testingQuery()
        {
            return "目前客戶數:" + (new CCustomerFactory()).queryAll().Count.ToString();
        }
    }
}