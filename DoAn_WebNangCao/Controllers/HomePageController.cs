using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_WebNangCao.Models;

namespace DoAn_WebNangCao.Controllers
{
    public class HomePageController : Controller
    {
        // GET: HomePage
        THITRACNGHIEMEntities db = new THITRACNGHIEMEntities();
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("LoginUser", "Login");
            }
            return View();
        }
        [HttpGet]
        public ActionResult TestLog()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("LoginUser", "Login");
            }
            int idUser = int.Parse(Session["ID"].ToString());
            List<DETHI> dethiLi = db.DETHIs.Where(x => x.IDUser == idUser).OrderBy(x=>x.NgayThi).ToList();
            foreach (var i in dethiLi)
            {
                if(i.TongDiem==null)
                {
                    i.TongDiem = 0;
                }
            }
            return View(dethiLi);
        }
    }
}