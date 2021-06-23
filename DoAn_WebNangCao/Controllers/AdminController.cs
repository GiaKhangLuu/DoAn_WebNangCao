using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_WebNangCao.Models;

namespace DoAn_WebNangCao.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        THITRACNGHIEMEntities db = new THITRACNGHIEMEntities();
        public ActionResult Index()
        {

            return RedirectToAction("CreateField", "Admin");
        }
        public ActionResult Permission(string _name)
        {
            if(Session["Quyen"]!="Admin")
            {
                return RedirectToAction("Index", "HomePage");
            }
            if (_name == null)
                return View(db.TAIKHOANs.ToList());
            else
                return View(db.TAIKHOANs.Where(s => s.UserName.Contains(_name)).ToList());
        }
        [HttpGet]
        public ActionResult CreateField()//Lĩnh vực
        {
            if (Session["Quyen"] != "Admin")
            {
                return RedirectToAction("Index", "HomePage");
            }
            List<LINHVUC> linhvucLi = db.LINHVUCs.OrderBy(x => x.IDLinhVuc).ToList();
            ViewData["linhvucLi"] = linhvucLi;
            return View();
        }
        [HttpPost]
        public ActionResult CreateField(LINHVUC lv)//Lĩnh vực
        {
            if (Session["Quyen"] != "Admin")
            {
                return RedirectToAction("Index", "HomePage");
            }
            db.LINHVUCs.Add(lv);
            db.SaveChanges();
            List<LINHVUC> linhvucLi = db.LINHVUCs.OrderBy(x => x.IDLinhVuc).ToList();
            ViewData["linhvucLi"] = linhvucLi;
            return View();
        }

        [HttpGet]
        public ActionResult ManageQuiz(int idLinhVuc)
        {
            if (Session["Quyen"] != "Admin")
            {
                return RedirectToAction("Index", "HomePage");
            }
            List<CAUHOI> cAUHOIs = db.CAUHOIs.Where(p => p.IDLinhVuc == idLinhVuc).ToList();
            ViewData["list_cau_hoi"] = cAUHOIs;
            return View();
        }

        [HttpPost]
        public ActionResult AddQuiz(int idLinhVuc, int idLoaiCauHoi)
        {
            if (Session["Quyen"] != "Admin")
            {
                return RedirectToAction("Index", "HomePage");
            }
            List<string> noiDungDapAns = new List<string>();
            List<bool> tinhChats = new List<bool>();
            if (idLoaiCauHoi == Constant.ID_CAU_HOI_1_DAP_AN)
            {
                string noiDungCauHoi = Request["one-correct-quiz-content"];
                int num_of_dap_an = Int32.Parse(Request["one-correct-quiz-num-of-dap-an"]);
                int true_answer_position = Int32.Parse(Request["one-correct-quiz-radio"]);
                
                for(int idx = 0; idx < num_of_dap_an; idx++)
                {
                    int order = idx + 1;
                    string dapan_content_name = "one-correct-quiz-noi-dung-" + order;
                    noiDungDapAns.Add(Request[dapan_content_name]);
                    tinhChats.Add(order == true_answer_position ? true : false);
                }
                Add_quiz_to_db(idLinhVuc, idLoaiCauHoi, noiDungCauHoi, noiDungDapAns, tinhChats);
            }
            else if(idLoaiCauHoi == Constant.ID_CAU_HOI_NHIEU_DAP_AN)
            {
                string noiDungCauHoi = Request["multi-correct-quiz-content"];
                int num_of_dap_an = Int32.Parse(Request["multi-correct-quiz-num-of-dap-an"]);
                for (int idx = 0; idx < num_of_dap_an; idx++)
                {
                    int order = idx + 1;
                    string checkbox_name = "multi-correct-quiz-checkbox-" + order;
                    string dapan_content_name = "multi-correct-quiz-noi-dung-" + order;
                    noiDungDapAns.Add(Request[dapan_content_name]);
                    tinhChats.Add(Request[checkbox_name] != null ? true : false);
                }
                Add_quiz_to_db(idLinhVuc, idLoaiCauHoi, noiDungCauHoi, noiDungDapAns, tinhChats);
            }
            else if(idLoaiCauHoi == Constant.ID_CAU_HOI_SAP_XEP_THEO_THU_TU)
            {
                string noiDungCauHoi = Request["sorted-quiz-content"];
                int num_of_dap_an = Int32.Parse(Request["sorted-quiz-num-of-dap-an"]);
                List<int> thuTus = new List<int>();
                for(int idx = 0; idx < num_of_dap_an; idx++)
                {
                    int order = idx + 1;
                    string dapan_content_name = "sorted-quiz-noidung-" + order;
                    noiDungDapAns.Add(Request[dapan_content_name]);
                    tinhChats.Add(true);
                    thuTus.Add(order);
                }
                Add_quiz_to_db(idLinhVuc, idLoaiCauHoi, noiDungCauHoi, noiDungDapAns, tinhChats, thuTus);
            }
            else if(idLoaiCauHoi == Constant.ID_CAU_HOI_DIEN_VAO_CHO_TRONG)
            {
                string noiDungCauHoi = Request["fill-in-blank-quiz-content"];
                int num_of_blanks = Int32.Parse(Request["fill-in-blank-quiz-num-of-blank"]);
                int num_of_wrong_answers = Int32.Parse(Request["fill-in-blank-quiz-num-of-wrong-answer"]);
                List<int> thuTus = new List<int>();
                // Add blank answer
                for(int idx = 0; idx < num_of_blanks; idx++)
                {
                    int order = idx + 1;
                    string blank_answer_name = "blank-answer-content-" + order;
                    noiDungDapAns.Add(Request[blank_answer_name]);
                    tinhChats.Add(true);
                    thuTus.Add(order);
                }
                CAUHOI cauhoi = Add_quiz_to_db(idLinhVuc, idLoaiCauHoi, noiDungCauHoi, noiDungDapAns, tinhChats, thuTus);
                // Add wrong answer
                noiDungDapAns.Clear();
                for(int idx = 0; idx < num_of_wrong_answers; idx++)
                {
                    int order = idx + 1;
                    string wrong_answer_name = "wrong-answer-content-" + order;
                    noiDungDapAns.Add(Request[wrong_answer_name]);
                }
                Add_wrong_answer_of_fill_in_blank_quiz_to_db(cauhoi.IDCauHoi, noiDungDapAns);
            }
            return RedirectToAction("ManageQuiz", "Admin", new { idLinhVuc = idLinhVuc });
        }

        private void Add_wrong_answer_of_fill_in_blank_quiz_to_db(int idCauHoi,
                                                                  List<string> noiDungDapAns)
                                                                  
        {
            foreach(string noiDungDapAn in noiDungDapAns)
            {
                DAPAN dAPAN = new DAPAN();
                dAPAN.Add_dap_an(noiDungDapAn, idCauHoi, false, null);
            }
        }

        private CAUHOI Add_quiz_to_db(int idLinhVuc,
                                           int idLoaiCauHoi,
                                           string noiDungCauHoi,
                                           List<string> noiDungDapAns,
                                           List<bool> tinhChats)
        {
            CAUHOI cauhoi = new CAUHOI();
            cauhoi = cauhoi.Add_cau_hoi(noiDungCauHoi, idLinhVuc, idLoaiCauHoi);        
            for(int idx = 0; idx < noiDungDapAns.Count; idx++)
            {
                string noiDungDapAn = noiDungDapAns[idx];
                bool tinhChat = tinhChats[idx];
                int idCauHoi = cauhoi.IDCauHoi;
                DAPAN dapan = new DAPAN();
                dapan.Add_dap_an(noiDungDapAn, idCauHoi, tinhChat, null);
            }
            return cauhoi;
        }

        private CAUHOI Add_quiz_to_db(int idLinhVuc,
                                           int idLoaiCauHoi,
                                           string noiDungCauHoi,
                                           List<string> noiDungDapAns,
                                           List<bool> tinhChats,
                                           List<int> thuTus)
        {
            CAUHOI cauhoi = new CAUHOI();
            cauhoi = cauhoi.Add_cau_hoi(noiDungCauHoi, idLinhVuc, idLoaiCauHoi);
            for (int idx = 0; idx < noiDungDapAns.Count; idx++)
            {
                string noiDungDapAn = noiDungDapAns[idx];
                bool tinhChat = tinhChats[idx];
                int thuTu = thuTus[idx];
                int idCauHoi = cauhoi.IDCauHoi;
                DAPAN dapan = new DAPAN();
                dapan.Add_dap_an(noiDungDapAn, idCauHoi, tinhChat, thuTu);
            }
            return cauhoi;
        }

    }
}