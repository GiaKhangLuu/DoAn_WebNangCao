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
        Exam exam;

        // GET: Test
        public ActionResult Index(int idLinhVuc)
        {
            string mucDo = "1";
            int id_tai_khoan = 1;
            //int id_linh_vuc = Int32.Parse(idLinhVuc);
            IEnumerable<CAUHOI> cau_hois = Get_cau_hoi_by_linh_vuc(idLinhVuc);
            // Create DETHI and CT_DETHI in db
            DETHI dethi = CreateDeThi(mucDo, id_tai_khoan);
            Create_CT_DeThi(dethi.IDDeThi, cau_hois);
            // Get session exam
            Create_new_session_exam(dethi.IDDeThi, cau_hois);
            Exam exam = Session["Exam"] as Exam;
            return View(exam);
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

        private void Create_CT_DeThi(int id_de_thi, IEnumerable<CAUHOI> cau_hois)
        {
            int so_luong_cau_hoi = cau_hois.Count();
            for (int i = 0; i < so_luong_cau_hoi; i++)
            {
                CT_DETHI ctdt = new CT_DETHI();
                ctdt.IDDeThi = id_de_thi;
                ctdt.IDCauHoi = cau_hois.ElementAt(i).IDCauHoi;
                db.CT_DETHI.Add(ctdt);
                db.SaveChanges();
            }
        }

        
    }
}