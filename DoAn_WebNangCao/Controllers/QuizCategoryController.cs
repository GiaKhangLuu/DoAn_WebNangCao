using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_WebNangCao.Models;

namespace DoAn_WebNangCao.Controllers
{
    public class QuizCategoryController : Controller
    {
        THITRACNGHIEMEntities db = new THITRACNGHIEMEntities();
        
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("LoginUser", "Login");
            }
            var linh_vucs = Get_all_linh_vuc();
            return View(linh_vucs);
        }

        public PartialViewResult Render_quiz_category(IEnumerable<LINHVUC> linh_vucs)
        {
            return PartialView(linh_vucs);
        }

        private IEnumerable<LINHVUC> Get_all_linh_vuc()
        {
            return db.LINHVUCs.AsEnumerable<LINHVUC>();
        }
    }
}