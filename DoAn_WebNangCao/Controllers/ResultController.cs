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
        
        [HttpPost]
        public ActionResult Index()
        {
            Result rs = new Result();
            Exam exam = Session["Exam"] as Exam;
            Process_user_choice(exam);
            rs.Num_of_correct_answers = Count_correct_answers(exam);
            rs.Num_of_quizs = exam.Quizs.Count();        
            return View(rs);
        }

        public PartialViewResult Render_result(Result rs)
        {
            return PartialView(rs);
        }

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
                    Add_user_choice_to_db(exam.Id_de_thi, id_cau_hoi, quiz.Id_cau_tra_loi);
                }
            }
        }
        public void Add_user_choice_to_db(int id_de_thi, int id_cau_hoi, int id_cau_tra_loi)
        {
            CT_DETHI ctdt = 
                db.CT_DETHI.FirstOrDefault(m => m.IDCauHoi == id_cau_hoi && m.IDDeThi == id_de_thi);
            if(ctdt != null)
            {
                ctdt.DapAnChon = id_cau_tra_loi;
                db.SaveChanges();
            }
        }

        public int Get_correct_answer_id(CAUHOI cau_hoi)
        {
            DAPAN corr_answer = cau_hoi.DAPANs.FirstOrDefault(dapan => dapan.TinhChat == true);
            return corr_answer.IDDapAn;
        }

        public int Count_correct_answers(Exam exam)
        {
            int count = 0;
            foreach(var quiz in exam.Quizs)
            {
                if(quiz.Id_cau_tra_loi == Get_correct_answer_id(quiz.Cau_hoi))
                {
                    count++;
                }
            }
            return count;
        }
    }
}