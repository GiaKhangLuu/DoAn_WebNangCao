using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_WebNangCao.Models;

namespace DoAn_WebNangCao.Controllers
{
    public class TestController : Controller
    {
        THITRACNGHIEMEntities db = new THITRACNGHIEMEntities();

        public ActionResult CreateExam(int idLinhVuc)
        {
            string mucDo = "1";
            int id_tai_khoan = 1;
            //int id_linh_vuc = Int32.Parse(idLinhVuc);
            IEnumerable<CAUHOI> cau_hois = Get_cau_hoi_by_linh_vuc(idLinhVuc);
            // Create DETHI and danh sach cau hoi
            DETHI dethi = CreateDeThi(mucDo, id_tai_khoan);
            Create_danh_sach_cau_hoi(dethi, cau_hois);
            // Get session exam
            Create_new_session_exam(dethi.IDDeThi, cau_hois);
            int first_quiz_id = Find_quiz_id_by_idx(0);
            return RedirectToAction("Index", new { idCauHoi = first_quiz_id });
        }

        public ActionResult Index(int idCauHoi)
        {
            CAUHOI cau_hoi = db.CAUHOIs.FirstOrDefault(ch => ch.IDCauHoi == idCauHoi);
            return View(cau_hoi);
        }

        public int Find_quiz_idx_by_id(int id_quiz)
        {
            Exam exam = Session["Exam"] as Exam;
            int quiz_idx = exam.Quizs.FindIndex(ch => ch.Cau_hoi.IDCauHoi == id_quiz);
            return quiz_idx;
        }

        public int Find_quiz_id_by_idx(int idx_quiz)
        {
            Exam exam = Session["Exam"] as Exam;
            int quiz_id = exam.Quizs[idx_quiz].Cau_hoi.IDCauHoi;
            return quiz_id;
        }

        public ActionResult PreviousQuiz(int idCauHoi)
        {
            int current_quiz_idx = Find_quiz_idx_by_id(idCauHoi);
            int next_quiz_id = Find_quiz_id_by_idx(current_quiz_idx - 1);
            return RedirectToAction("Index", new { idCauHoi = next_quiz_id });
        }

        public ActionResult NextQuiz(int idCauHoi)
        {
            int current_quiz_idx = Find_quiz_idx_by_id(idCauHoi);
            int next_quiz_id = Find_quiz_id_by_idx(current_quiz_idx + 1);
            return RedirectToAction("Index", new { idCauHoi = next_quiz_id });
        }

        public void Create_new_session_exam(int id_de_thi, IEnumerable<CAUHOI> cau_hois)
        {
            Session["Exam"] = new Exam();
            ((Exam)Session["Exam"]).Id_de_thi = id_de_thi;
            ((Exam)Session["Exam"]).Add_cau_hoi(cau_hois);
        }

        public IEnumerable<CAUHOI> Get_cau_hoi_by_linh_vuc(int idLinhVuc)
        {
            var cau_hois = db.CAUHOIs.Where(ch => ch.IDLinhVuc == idLinhVuc);
            return cau_hois.AsEnumerable<CAUHOI>();
        }

        public PartialViewResult GetQuestion(Exam exam)
        {
            return PartialView(exam);
        }
       
        private DETHI CreateDeThi(string mucDo, int idTaiKhoan)
        {
            DETHI dethi = new DETHI();
            dethi.NgayThi = DateTime.Now;
            dethi.MucDo = mucDo;
            dethi.IDUser = idTaiKhoan;
            db.DETHIs.Add(dethi);
            db.SaveChanges();
            return dethi;
        }
    
        private void Create_danh_sach_cau_hoi(DETHI de_thi, IEnumerable<CAUHOI> cau_hois)
        {
            int so_luong_cau_hoi = cau_hois.Count();
            for (int i = 0; i < so_luong_cau_hoi; i++)
            {
                //DETHI dethi = db.DETHIs.FirstOrDefault(dt => dt.IDDeThi == id_de_thi);
                CAUHOI cau_hoi = cau_hois.ElementAt(i);
                de_thi.CAUHOIs.Add(cau_hoi);
                db.SaveChanges();
            }
        }
                
    }
}