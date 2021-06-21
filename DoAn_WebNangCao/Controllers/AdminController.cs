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
        THITRACNGHIEMEntities db = new THITRACNGHIEMEntities();
        public ActionResult Index()
        {
            if(Session["UserName"]==null)
            {
                return RedirectToAction("LoginUser", "Login");
            }
            ViewBag.Encrypt = Encryption.Encrypt("admin");
            return View();
        }
        public ActionResult Permission(string _name)
        {
            if (_name == null)
                return View(db.TAIKHOANs.ToList());
            else
                return View(db.TAIKHOANs.Where(s => s.UserName.Contains(_name)).ToList());
        }
        [HttpGet]
        public ActionResult CreateField()//Lĩnh vực
        {
            List<LINHVUC> linhvucLi = db.LINHVUCs.OrderBy(x => x.IDLinhVuc).ToList();
            ViewData["linhvucLi"] = linhvucLi;
            return View();
        }
        public ActionResult GetListLV()
        {
            var lvLi = db.LINHVUCs.ToList<LINHVUC>();
            return Json(new { data = lvLi }, JsonRequestBehavior.AllowGet);
        }
    }
}