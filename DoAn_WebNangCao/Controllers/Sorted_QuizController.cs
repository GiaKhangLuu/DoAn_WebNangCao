using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_WebNangCao.Models;

namespace DoAn_WebNangCao.Controllers
{
    public class Sorted_QuizController : Controller
    {
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

        public void Add_answer_for_sorted_quiz(int quiz_idx)
        {
            string raw_answer = Request["raw_answer"];
            if (raw_answer != null && raw_answer.Length != 0)
            {
                (Session["Exam"] as Exam).Add_answer_by_id_quiz(quiz_idx, raw_answer);
            }
        }

        [HttpPost]
        public ActionResult Go(string type, int quiz_idx)
        {
            Add_answer_for_sorted_quiz(quiz_idx);
            return RedirectToAction("NewQuiz", "Test", new { type = type, quiz_idx = quiz_idx });
        }

        public PartialViewResult Check_answer(Quiz quiz)
        {
            return PartialView(quiz);
        }

    }
}