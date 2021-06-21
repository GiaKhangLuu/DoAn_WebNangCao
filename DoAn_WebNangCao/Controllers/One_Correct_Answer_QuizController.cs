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
            if (Session["UserName"] == null)
            {
                return RedirectToAction("LoginUser", "Login");
            }
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
                Exam exam = Session["Exam"] as Exam;
                Quiz quiz = exam.Quizs[quiz_idx];
                int id_dap_an_chon = Int32.Parse(Request["answer_id"]);
                quiz.Save_answer_of_one_correct_answer_quiz(id_dap_an_chon);
            }
        }

        public ActionResult Go(int quiz_idx)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("LoginUser", "Login");
            }
            Save_answer_to_session(quiz_idx);
            int new_quiz_idx;
            // User click on next pre done btn
            if(Request["type"] != null)
            {
                new_quiz_idx = Get_new_quiz_idx_by_next_pre_done_btn(Request["type"], quiz_idx);
            }
            // User click on list quiz button
            else
            {
                // Because value in list quiz button start from 1, we must minus 1
                new_quiz_idx = Int32.Parse(Request["new_quiz_idx"]) - 1;
            }
            if(new_quiz_idx == -1)
            {
                return RedirectToAction("MarkExam", "Result");
            }
            return RedirectToAction("Index", "Test", new { quiz_idx = new_quiz_idx });
        }

        public int Get_new_quiz_idx_by_next_pre_done_btn(string type, int quiz_idx)
        {
            int num_of_quizs = (Session["Exam"] as Exam).Quizs.Count;
            if (type == "Next")
            {
                // Case user is at the last quiz and press next button
                if (quiz_idx == num_of_quizs - 1)
                {
                    return 0;
                }
                return ++quiz_idx;
            }
            else if(type == "Pre")
            {
                // Case user is at the first quiz and press pre button
                if (quiz_idx == 0)
                {
                    return num_of_quizs - 1;
                }
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