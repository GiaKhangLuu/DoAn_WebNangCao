using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using DoAn_WebNangCao.Models;

namespace DoAn_WebNangCao.Controllers
{
    public class Fill_In_Blank_QuizController : Controller
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

        private int Get_Num_Of_Blanks(Quiz quiz)
        {
            string quiz_title = quiz.Cau_hoi.NoiDung;
            string pattern = "{'blank'}";
            return Regex.Matches(quiz_title, pattern).Count;
        }

        private void Add_answer_to_session(int quiz_idx)
        {
            Exam exam = Session["Exam"] as Exam;
            Quiz quiz = exam.Quizs[quiz_idx];
            int num_Of_Blanks = Get_Num_Of_Blanks(quiz);
            int[] answer_ids = new int[num_Of_Blanks];
            for (int i = 0; i < num_Of_Blanks; i++)
            {
                string param_name = "answer-blank-" + (i + 1);
                answer_ids[i] = Int32.Parse(Request[param_name]);
            }
            quiz.Save_answer_of_fill_in_blank_quiz(answer_ids);
        }

        public ActionResult Go(int quiz_idx)
        {
            Add_answer_to_session(quiz_idx);
            int new_quiz_idx;
            // User click on next pre done btn
            if (Request["type"] != null)
            {
                new_quiz_idx = Get_new_quiz_idx_by_next_pre_done_btn(Request["type"], quiz_idx);
            }
            // User click on list quiz button
            else
            {
                // Because value in list quiz button start from 1, we must minus 1
                new_quiz_idx = Int32.Parse(Request["new_quiz_idx"]) - 1;
            }
            if (new_quiz_idx == -1)
            {
                return RedirectToAction("MarkExam", "Result");
            }
            return RedirectToAction("Index", "Test", new { quiz_idx = new_quiz_idx });
        }

        private int Get_new_quiz_idx_by_next_pre_done_btn(string type, int quiz_idx)
        {
            if (type == "Next")
            {
                return ++quiz_idx;
            }
            else if (type == "Pre")
            {
                return --quiz_idx;
            }
            return -1;
        }

        public PartialViewResult Check_answer(Quiz quiz)
        {
            return PartialView(quiz);
        }
    }
}