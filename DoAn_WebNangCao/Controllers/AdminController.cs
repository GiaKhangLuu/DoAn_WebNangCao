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
    }
}