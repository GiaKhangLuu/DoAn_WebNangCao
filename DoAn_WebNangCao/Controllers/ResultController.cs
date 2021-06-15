using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_WebNangCao.Models;

namespace DoAn_WebNangCao.Controllers
{
    public class ResultController : Controller
    {
        THITRACNGHIEMEntities db = new THITRACNGHIEMEntities();

        public ActionResult MarkExam()
        {
            Result rs = new Result();
            Exam exam = Session["Exam"] as Exam;
            rs.Num_of_quizs = exam.Quizs.Count;
            exam.Mark_exam(db);
            rs.Num_of_correct_answers = exam.Count_correct_answers(db);
            return RedirectToAction("Index", rs);
        }
        
        public ActionResult Index(Result rs)
        {
            return View(rs);
        }

        public PartialViewResult Render_result(Result rs)
        {
            return PartialView(rs);
        }
    }
}