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

        public void Save_answer_for_quiz(int quiz_idx)
        {
            Exam exam = Session["Exam"] as Exam;
            Quiz quiz = exam.Quizs[quiz_idx];
            List<int> selected_answer_ids = new List<int>();
            foreach(DAPAN dapan in quiz.Cau_hoi.DAPANs)
            {
                int id_dap_an = dapan.IDDapAn;
                string isChecked = Request[id_dap_an.ToString()];
                if (isChecked != null && isChecked.Length != 0)
                {
                    //if (Boolean.Parse(isChecked))
                    //{
                        selected_answer_ids.Add(id_dap_an);                       
                    //}
                }
            }
            quiz.Save_answer_of_multi_correct_answer_quiz(selected_answer_ids);
        }

        public ActionResult Go(int quiz_idx)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("LoginUser", "Login");
            }
            Save_answer_for_quiz(quiz_idx);
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
            else if (type == "Pre")
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