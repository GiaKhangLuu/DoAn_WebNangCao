using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_WebNangCao.Models;

namespace DoAn_WebNangCao.Controllers
{
    public class Multi_Correct_Answer_QuizController : Controller
    {
        // GET: Multi_Correct_Answer_Quiz
        public ActionResult Index(int quiz_idx)
        {
            Exam exam = Session["Exam"] as Exam;
            Quiz quiz = exam.Quizs[quiz_idx];
            return View(quiz);
        }

        public PartialViewResult Render_quiz(Quiz quiz)
        {
            return PartialView(quiz);
        }

        [HttpPost]
        public ActionResult Go(string type, int quiz_idx)
        {
            //Add_answer_for_sorted_quiz(quiz_idx);
            return RedirectToAction("NewQuiz", "Test", new { type = type, quiz_idx = quiz_idx });
        }
    }
}