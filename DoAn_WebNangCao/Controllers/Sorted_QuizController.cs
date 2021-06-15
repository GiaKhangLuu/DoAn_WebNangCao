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

        private void Add_answer_for_sorted_quiz(int quiz_idx)
        {
            string raw_answer = Request["raw_answer"];
            if (raw_answer != null && raw_answer.Length != 0)
            {
                Exam exam = Session["Exam"] as Exam;
                Quiz quiz = exam.Quizs[quiz_idx];
                //(Session["Exam"] as Exam).Add_answer_by_id_quiz(quiz_idx, raw_answer);
                int[] answer_ids = Convert_raw_answer_to_list_answer(raw_answer);
                quiz.Save_answer_of_sorted_answer_quiz(answer_ids);
            }
        }

        private int[] Convert_raw_answer_to_list_answer(string raw_answer)
        {
            string[] string_answer_ids = raw_answer.Split(',');
            int[] answer_ids = Array.ConvertAll(string_answer_ids, id => Int32.Parse(id));
            return answer_ids;
        }

        public ActionResult Go(int quiz_idx)
        {
            Add_answer_for_sorted_quiz(quiz_idx);
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