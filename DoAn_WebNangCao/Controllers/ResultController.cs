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
            Result rs = new Result();
            Exam exam = Session["Exam"] as Exam;
            rs.Num_of_correct_answers = exam.Count_correct_answers();
            rs.Num_of_quizs = exam.Quizs.Count;
            Add_user_choice_to_db(exam);
            return RedirectToAction("Index", rs);
        }
        
        public ActionResult Index(Result rs)
        {
            return View(rs);
        }

        public PartialViewResult Render_result(Result rs)
        {
            return PartialView(rs);
        }

        /*
        public void Process_user_choice(Exam exam)
        {
            string prefix = "answer-quiz-";
            foreach(var quiz in exam.Quizs)
            {
                int id_cau_hoi = quiz.Cau_hoi.IDCauHoi;
                string key = prefix + id_cau_hoi;
                string value = Request[key];
                if(value != null)
                {
                    quiz.Id_cau_tra_loi = Int32.Parse(value);
                    Add_user_choice_to_db(exam.Id_de_thi, quiz.Id_cau_tra_loi);
                }
            }
        }
        */

        public void Add_user_choice_to_db(Exam exam)
        {
            foreach (var quiz in exam.Quizs) 
            {
                if(quiz.Id_cau_tra_loi != -1)
                {
                    DANHSACHDAPANCHON dap_an_chon = new DANHSACHDAPANCHON();
                    dap_an_chon.IDDethi = exam.Id_de_thi;
                    dap_an_chon.IDDapAn = quiz.Id_cau_tra_loi;
                    dap_an_chon.ThuTu = 0;
                    db.DANHSACHDAPANCHONs.Add(dap_an_chon);
                    db.SaveChanges();
                }              
            }
        }
    }
}