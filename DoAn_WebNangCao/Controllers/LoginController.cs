﻿using DoAn_WebNangCao.Models;
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
                Session["UserName"] = user.UserName;
                Session["AnhDaiDien"] = check.AnhDaiDien;
                Session["HoTen"] = check.HoTen;
                Session["Quyen"] = check.Quyen;
                HttpCookie cookie = new HttpCookie("Huflit Quiz"); //tạo cookie
                if (user.RememberMe)
                {

                    cookie["UserName"] = user.UserName;
                    cookie["MatKhau"] = matKhau;
                    cookie.Expires = DateTime.Now.AddDays(365);
                    HttpContext.Response.Cookies.Add(cookie);// thông tin cookie và lưu.
                }
                return RedirectToAction("Index", "Admin");
            }
        }
        public ActionResult RegisterUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterUser(TAIKHOAN user)
        {
            user.AnhDaiDien = "~/Content/images/avatardefault.png";
            user.Quyen = false;
            user.MatKhau = Encryption.Encrypt(user.MatKhau);
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
        public ActionResult LogOutUser()
        {
            Session.Abandon();
            HttpCookie cookie = Request.Cookies["Huflit Quiz"];
            cookie.Expires = DateTime.Now.AddDays(-1);//Xóa cookie
            HttpContext.Response.Cookies.Add(cookie);
            return RedirectToAction("LoginUser","Login");
        }

    }
}