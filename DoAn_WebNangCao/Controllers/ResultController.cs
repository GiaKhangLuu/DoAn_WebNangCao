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
            if (Session["UserName"] == null)
            {
                return RedirectToAction("LoginUser", "Login");
            }
            Result rs = new Result();
            Exam exam = Session["Exam"] as Exam;
            if(!exam.Status)
            {
                return RedirectToAction("Index", "ReSubmit_Error");
            }
            exam.Status = false;
            rs.Num_of_quizs = exam.Quizs.Count;
            exam.Mark_exam(db);
            rs.Num_of_correct_answers = exam.Count_correct_answers(db);
            rs.Score = Get_total_score(rs);
            Save_total_score_to_db(exam.Id_de_thi, rs.Score);
            return RedirectToAction("Index", rs);
        }

        private double Get_total_score(Result rs)
        {
            double one_correct_quiz_score =  10 / (rs.Num_of_quizs * 1.0);
            double total_score = one_correct_quiz_score * rs.Num_of_correct_answers;
            return Math.Round(total_score, 2);
        }
        
        public ActionResult Index(Result rs)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("LoginUser", "Login");
            }
            return View(rs);
        }

        public PartialViewResult Render_result(Result rs)
        {
            return PartialView(rs);
        }

        private DETHI Save_total_score_to_db(int idDeThi, double score)
        {
            DETHI dETHI = db.DETHIs.Find(idDeThi);
            dETHI.TongDiem = (decimal?)score;
            db.SaveChanges();
            return dETHI;
        }
    }
}