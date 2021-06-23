using DoAn_WebNangCao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn_WebNangCao.Controllers
{
    public class LoginController : Controller
    {
        THITRACNGHIEMEntities db = new THITRACNGHIEMEntities();
        
        // GET: Login
        public ActionResult Index()
        {
            return RedirectToAction("LoginUser","Admin");
        }

        public ActionResult LoginUser()
        {
            if(Session["UserName"]!=null)
            {
                if(Session["Quyen"]=="Admin")
                {
                    return Direct_To_Admin_Page();
                }
                else
                {
                    return Direct_To_Client_Home_Page();
                }
            }
            HttpCookie cookie = Request.Cookies["Huflit Quiz"];// yêu cầu cookie.
            if(cookie!=null)
            {
                ViewBag.UserName = cookie["UserName"].ToString();// gán cookie
                ViewBag.MatKhau = Encryption.Decrypt(cookie["MatKhau"].ToString());
            }    
            return View();
            
        }
        [HttpPost]
        public ActionResult LoginUser(TAIKHOAN user)
        {
            if (Session["UserName"] != null)
            {
                if (Session["Quyen"] == "Admin")
                {
                    return Direct_To_Admin_Page();
                }
                else
                {
                    return Direct_To_Client_Home_Page();
                }
            }
            var matKhau = Encryption.Encrypt(user.MatKhau);
            var check = db.TAIKHOANs.Where(s => s.UserName == user.UserName && s.MatKhau == matKhau).FirstOrDefault();
            if(check==null)
            {
                ViewBag.ErrorInfo = "Tên đăng nhập hoặc mật khẩu không phù hợp!";
                return View("LoginUser");
            }
            else
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["ID"] = check.IDUser;
                Session["UserName"] = user.UserName;
                Session["MatKhau"] = user.MatKhau;
                Session["AnhDaiDien"] = check.AnhDaiDien;
                Session["HoTen"] = check.HoTen;
                Session["Email"] = check.Email;
                if (check.Quyen)
                {
                    Session["Quyen"] = "Admin";
                }
                else
                {
                    Session["Quyen"] = "Khách";
                }
                HttpCookie cookie = new HttpCookie("Huflit Quiz"); //tạo cookie
                Session["Remember"] = "false";
                if (user.RememberMe)
                {

                    cookie["UserName"] = user.UserName;
                    cookie["MatKhau"] = matKhau;
                    cookie.Expires = DateTime.Now.AddDays(365);
                    HttpContext.Response.Cookies.Add(cookie);// thông tin cookie và lưu.
                    Session["Remember"] = "true";
                }
                if(check.Quyen)
                {
                    return Direct_To_Admin_Page();
                }
                else
                {
                    return Direct_To_Client_Home_Page();
                }
            }
        }
        public ActionResult RegisterUser()
        {
            if (Session["UserName"] != null)
            {
                if (Session["Quyen"] == "Admin")
                {
                    return Direct_To_Admin_Page();
                }
                else
                {
                    return Direct_To_Client_Home_Page();
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult RegisterUser(TAIKHOAN user)
        {
            if (Session["UserName"] != null)
            {
                if (Session["Quyen"] == "Admin")
                {
                    return Direct_To_Admin_Page();
                }
                else
                {
                    return Direct_To_Client_Home_Page();
                }
            }
            if (ModelState.IsValid)
            {
                user.AnhDaiDien = "~/Content/images/avatardefault.png";
                user.Quyen = false;
                user.MatKhau = Encryption.Encrypt(user.MatKhau);
                var check_UserName = db.TAIKHOANs.Where(x => x.UserName== user.UserName).FirstOrDefault();
                if(check_UserName==null)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.TAIKHOANs.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("LoginUser");
                }
                else
                {
                    ViewBag.ErrorRegister = "Tên đăng nhập đã tồn tại!";
                    return View();
                }
            }
            return View();
        }
        public ActionResult LogOutUser()
        {
            Session.Abandon();
            if(Session["Remember"].ToString()=="true")
            {
                HttpCookie cookie = Request.Cookies["Huflit Quiz"];
                cookie.Expires = DateTime.Now.AddDays(-1);//Xóa cookie
                HttpContext.Response.Cookies.Add(cookie);
                Session["Remember"] = "false";
            }
            return RedirectToAction("LoginUser","Login");
        }

        public ActionResult Direct_To_Client_Home_Page()
        {
            return RedirectToAction("Index", "HomePage");
        }

        public ActionResult Direct_To_Admin_Page()
        {
            return RedirectToAction("Index", "Admin");
        }
    }
}