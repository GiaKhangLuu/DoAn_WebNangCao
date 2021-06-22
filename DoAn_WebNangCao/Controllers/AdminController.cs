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
            if(Session["UserName"]==null)
            {
                return RedirectToAction("LoginUser", "Login");
            }
            ViewBag.Encrypt = Encryption.Encrypt("admin");
            return View();
        }
        public ActionResult Permission(string _name)
        {
            if (_name == null)
                return View(db.TAIKHOANs.ToList());
            else
                return View(db.TAIKHOANs.Where(s => s.UserName.Contains(_name)).ToList());
        }
        [HttpGet]
        public ActionResult CreateField()//Lĩnh vực
        {
            List<LINHVUC> linhvucLi = db.LINHVUCs.OrderBy(x => x.IDLinhVuc).ToList();
            ViewData["linhvucLi"] = linhvucLi;
            return View();
        }

        [HttpGet]
        public ActionResult ManageQuiz(int idLinhVuc)
        {
            List<CAUHOI> cAUHOIs = db.CAUHOIs.Where(p => p.IDLinhVuc == idLinhVuc).ToList();
            ViewData["list_cau_hoi"] = cAUHOIs;
            return View();
        }

        [HttpPost]
        public ActionResult AddQuiz(int idLinhVuc, int idLoaiCauHoi)
        {
            if(idLoaiCauHoi == Constant.ID_CAU_HOI_1_DAP_AN)
            {
                string noiDungCauHoi = Request["one-correct-quiz-content"];
                int num_of_dap_an = Int32.Parse(Request["one-correct-quiz-num-of-dap-an"]);
                int true_answer_position = Int32.Parse(Request["one-correct-quiz-radio"]);
                List<string> noiDungDapAns = new List<string>();
                List<bool> tinhChats = new List<bool>();
                for(int idx = 0; idx < num_of_dap_an; idx++)
                {
                    int order = idx + 1;
                    string dapan_content_name = "one-correct-quiz-noi-dung-" + order;
                    noiDungDapAns.Add(Request[dapan_content_name]);
                    tinhChats.Add(order == true_answer_position ? true : false);
                }
                return Add_one_correct_answer_quiz_to_db(idLinhVuc, idLoaiCauHoi, noiDungCauHoi, noiDungDapAns, tinhChats);
            }
            else if(idLoaiCauHoi == Constant.ID_CAU_HOI_NHIEU_DAP_AN)
            {
                string noiDungCauHoi = Request["multi-correct-quiz-content"];
                int num_of_dap_an = Int32.Parse(Request["multi-correct-quiz-num-of-dap-an"]);
                List<string> noiDungDapAns = new List<string>();
                List<bool> tinhChats = new List<bool>();
                for (int idx = 0; idx < num_of_dap_an; idx++)
                {
                    int order = idx + 1;
                    string checkbox_name = "multi-correct-quiz-checkbox-" + order;
                    string dapan_content_name = "multi-correct-quiz-noi-dung-" + order;
                    noiDungDapAns.Add(Request[dapan_content_name]);
                    tinhChats.Add(Request[checkbox_name] != null ? true : false);
                }
                return Add_multi_correct_answer_quiz_to_db(idLinhVuc, idLoaiCauHoi, noiDungCauHoi, noiDungDapAns, tinhChats);
            }
            return null;
        }

        private ActionResult Add_one_correct_answer_quiz_to_db(int idLinhVuc,
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
            return RedirectToAction("ManageQuiz", "Admin", new { idLinhVuc = idLinhVuc });
        }

        private ActionResult Add_multi_correct_answer_quiz_to_db(int idLinhVuc, 
                                                                 int idLoaiCauHoi,
                                                                 string noiDungCauHoi,
                                                                 List<string> noiDungDapAns,
                                                                 List<bool> tinhChats)
        {
            CAUHOI cauhoi = new CAUHOI();
            cauhoi = cauhoi.Add_cau_hoi(noiDungCauHoi, idLinhVuc, idLoaiCauHoi);
            for (int idx = 0; idx < noiDungDapAns.Count; idx++)
            {
                string noiDungDapAn = noiDungDapAns[idx];
                bool tinhChat = tinhChats[idx];
                int idCauHoi = cauhoi.IDCauHoi;
                DAPAN dapan = new DAPAN();
                dapan.Add_dap_an(noiDungDapAn, idCauHoi, tinhChat, null);
            }
            return RedirectToAction("ManageQuiz", "Admin", new { idLinhVuc = idLinhVuc });
        }
    }
}