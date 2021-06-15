using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_WebNangCao.Models;

namespace DoAn_WebNangCao.Controllers
{
    public class AdminController : Controller
    {
        THITRACNGHIEMEntities db = new THITRACNGHIEMEntities();
        // GET: Admin
        public ActionResult Index()
        {
            if(Session["UserName"]==null)
            {
                return RedirectToAction("LoginUser", "Login");
            }
            ViewBag.Encrypt = Encryption.Encrypt("admin");
            return View();
        }
        public ActionResult Profiles()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("LoginUser", "Login");
            }
            return View();

        }
        [HttpPost]
        public ActionResult Profiles(TAIKHOAN x)
        {
            try
            {
                x.IDUser = Convert.ToInt32(Session["ID"]);
                x.UserName = Session["UserName"].ToString();
                x.MatKhau = Session["MatKhau"].ToString();
                if (Session["Quyen"].ToString() == "Admin")
                {
                    x.Quyen = true;
                }
                else
                {
                    x.Quyen = false;
                }
                if (x.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(x.UploadImage.FileName);
                    string extent = Path.GetExtension(x.UploadImage.FileName);
                    filename += extent;
                    x.AnhDaiDien = "~/Content/images/" + filename;
                    x.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }
                else
                {
                    x.AnhDaiDien = Session["AnhDaiDien"].ToString();
                }
                if (x.MatKhau != null)
                {
                    x.MatKhau = Encryption.Encrypt(x.MatKhau);
                }
                x.ConfirmPass = x.MatKhau;
                if (Session["Remember"].ToString() == "true")
                {
                    x.RememberMe = true;
                }
                else
                    x.RememberMe = false;
                db.Entry(x).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                Session["UserName"] = x.UserName;
                Session["HoTen"] = x.HoTen;
                Session["MatKhau"] = Encryption.Decrypt(x.MatKhau);
                Session["Email"] = x.Email;
                Session["AnhDaiDien"] = x.AnhDaiDien;
                if(x.RememberMe)
                {
                    HttpCookie cookie = Request.Cookies["Huflit Quiz"];
                    cookie["UserName"] = x.UserName;
                    cookie["MatKhau"] = x.MatKhau.ToString();
                    cookie.Expires = DateTime.Now.AddDays(365);
                    HttpContext.Response.Cookies.Add(cookie);
                }
                ViewBag.ThanhCong = "true";
                return View();
            }
            catch(Exception e)
            {
                ViewBag.ThanhCong = "false";
                Console.WriteLine(e.Message);
                return View();
            }
        }
    }
}