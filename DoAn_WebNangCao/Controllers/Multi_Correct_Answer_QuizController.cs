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

        public void Save_answer_for_quiz(int quiz_idx)
        {
            Exam exam = Session["Exam"] as Exam;
            Quiz quiz = exam.Quizs[quiz_idx];
            foreach(DAPAN dapan in quiz.Cau_hoi.DAPANs)
            {
                int id_dap_an = dapan.IDDapAn;
                string isChecked = Request[id_dap_an.ToString()];
                if (isChecked != null && isChecked.Length != 0)
                {
                    if (Boolean.Parse(isChecked))
                    {
                        quiz.Save_answer_of_multi_correct_answer_quiz(id_dap_an);
                    }
                }
            }
        }

        [HttpPost]
        public ActionResult Go(string type, int quiz_idx)
        {
            Save_answer_for_quiz(quiz_idx);
            return RedirectToAction("NewQuiz", "Test", new { type = type, quiz_idx = quiz_idx });
        }
    }
}