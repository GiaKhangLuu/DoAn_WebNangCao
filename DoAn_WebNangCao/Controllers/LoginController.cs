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
            return View();
        }
        public ActionResult LoginUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginUser(TAIKHOAN user)
        {
            var check = db.TAIKHOANs.Where(s => s.UserName == user.UserName && s.MatKhau == user.MatKhau).FirstOrDefault();
            if(check==null)
            {
                ViewBag.ErrorInfo = "Tên đăng nhập hoặc mật khẩu không phù hợp!";
                return View("LoginUser");
            }
            else
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["UserName"] = user.UserName;
                Session["Password"] = user.MatKhau;
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult RegisterUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterUser(TAIKHOAN user)
        {
            user.AnhDaiDien = "";
            user.Quyen = false;
            if (ModelState.IsValid)
            {
                var check_UserName = db.TAIKHOANs.Where(x => x.UserName.ToString().Trim().ToLower() == user.UserName.ToString().Trim().ToLower()).FirstOrDefault();
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
    }
}