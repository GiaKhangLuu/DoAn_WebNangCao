using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_WebNangCao.Models;
using System.Collections;

namespace DoAn_WebNangCao.Controllers
{
    public class CheckAnswerController : Controller
    {
        // GET: CheckAnswer
        public ActionResult Index()
        {
            Exam exam = Session["Exam"] as Exam;
            List<Quiz> quizs = exam.Quizs;
            return View(quizs);
        }

        public PartialViewResult Render_quizs(List<Quiz> quiz)
        {
            return PartialView(quiz);
        }

        public PartialViewResult Render_sorted_quiz(Quiz quiz)
        {
            return PartialView(quiz);
        }
    }
}