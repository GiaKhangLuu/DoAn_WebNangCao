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
            return RedirectToAction("Index", new { quiz_idx = 0 });
        }

        public ActionResult Index(int quiz_idx)
        {
            Exam exam = Session["Exam"] as Exam;
            var quiz_type_id = exam.Quizs[quiz_idx].Cau_hoi.IDLoaiCauHoi;
            if (quiz_type_id == Constant.ID_CAU_HOI_SAP_XEP_THEO_THU_TU)
            {
                return RedirectToAction("Index", "Sorted_Quiz", new { quiz_idx = quiz_idx });
            }
            else if(quiz_type_id == Constant.ID_CAU_HOI_1_DAP_AN)
            {
                return RedirectToAction("Index", "One_Correct_Answer_Quiz", new { quiz_idx = quiz_idx });
            }
            else if(quiz_type_id == Constant.ID_CAU_HOI_NHIEU_DAP_AN)
            {
                return RedirectToAction("Index", "Multi_Correct_Answer_Quiz", new { quiz_idx = quiz_idx });
            }
            return null;
        }

        public PartialViewResult Render_list_button_quiz()
        {
            Exam exam = Session["Exam"] as Exam;
            return PartialView(exam);
        }

        public void Create_new_session_exam(int id_de_thi, IEnumerable<CAUHOI> cau_hois)
        {
            Session["Exam"] = new Exam(id_de_thi, cau_hois);
        }

        public IEnumerable<CAUHOI> Get_cau_hoi_by_linh_vuc(int idLinhVuc)
        {
            var cau_hois = db.CAUHOIs.Where(ch => ch.IDLinhVuc == idLinhVuc);
            return cau_hois.AsEnumerable<CAUHOI>();
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
            for(int i = 0; i < cau_hois.Count(); i++)
            {
                var cau_hoi_id = cau_hois.ElementAt(i).IDCauHoi;
                DANHSACHCAUHOI danh_sach_cau_hoi = new DANHSACHCAUHOI();
                danh_sach_cau_hoi.IDDeThi = de_thi.IDDeThi;
                danh_sach_cau_hoi.IDCauHoi = cau_hoi_id;
                db.DANHSACHCAUHOIs.Add(danh_sach_cau_hoi);
                db.SaveChanges();
            }
        }
    }
}