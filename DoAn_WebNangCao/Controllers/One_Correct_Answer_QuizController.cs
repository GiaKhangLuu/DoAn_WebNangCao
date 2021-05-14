using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_WebNangCao.Models;

namespace DoAn_WebNangCao.Controllers
{
    public class One_Correct_Answer_QuizController : Controller
    {
        // GET: One_Correct_Answer_Quiz
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

        public void Save_answer_to_session(int quiz_idx)
        {
            if (Request["answer_id"] != null)
            {
                int id_dap_an_chon = Int32.Parse(Request["answer_id"]);
                (Session["Exam"] as Exam).Save_answer_of_one_correct_answer_quiz(quiz_idx, id_dap_an_chon);
            }
        }

        [HttpPost]
        public ActionResult Go(string type, int quiz_idx)
        {
            Save_answer_to_session(quiz_idx);
            return RedirectToAction("NewQuiz", "Test", new { type = type, quiz_idx = quiz_idx });
        }

        public PartialViewResult Check_answer(Quiz quiz)
        {
            return PartialView(quiz);
        }
    }
}