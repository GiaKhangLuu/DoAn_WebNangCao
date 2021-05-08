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
        public ActionResult RegisterUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterUser(TAIKHOAN user)
        {
            user.AnhDaiDien = null;
            user.Quyen = false;
            if (ModelState.IsValid)
            {
                var check_UserName = db.TAIKHOANs.Where(x => x.UserName.ToString().Trim() == user.UserName.ToString().Trim()).FirstOrDefault();
                if(check_UserName==null)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.TAIKHOANs.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("LoginUser");
                }
                else
                {
                    ViewBag.ErrorRegister = "This User Name is not valid";
                    return View();
                }
            }
            return View();
        }
        public ActionResult RegisterUser1()
        {
            return View();
        }
    }
}