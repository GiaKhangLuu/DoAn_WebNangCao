using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn_WebNangCao.Models
{
    public class Exam
    {
        private int id_de_thi;
        private List<Quiz> quizs = new List<Quiz>();

        public Exam(int id_de_thi, List<Quiz> quizs)
        {
            this.Id_de_thi = id_de_thi;
            this.Quizs = quizs;
        }

        public Exam(int exam_id, IEnumerable<CAUHOI> cau_hois)
        {
            this.id_de_thi = exam_id;
            Add_cau_hoi(cau_hois);
        }

        public Exam() { }

        public int Id_de_thi { get => id_de_thi; set => id_de_thi = value; }
        public List<Quiz> Quizs { get => quizs; set => quizs = value; }

        private void Add_cau_hoi(IEnumerable<CAUHOI> cau_hois)
        {
            for(int idx = 0; idx < cau_hois.Count(); idx++)
            {
                Quiz quiz = new Quiz(cau_hois.ElementAt(idx), idx);
                quizs.Add(quiz);
            }
        }

        public int Count_correct_answers(THITRACNGHIEMEntities db)
        {
            var ds_ch = db.DANHSACHCAUHOIs.Where(p => p.IDDeThi == id_de_thi);
            int num_of_correct_answers = ds_ch.Where(p => p.KetQua == true).Count();
            return num_of_correct_answers;
        }  

        public void Mark_exam(THITRACNGHIEMEntities db)
        {
            Mark_quiz();
            Add_user_answers_to_db(db);
            for(int i = 0; i < quizs.Count; i++)
            {
                Quiz quiz = quizs[i];
                int quiz_id = quiz.Cau_hoi.IDCauHoi;
                DANHSACHCAUHOI ds_ch = db.DANHSACHCAUHOIs.FirstOrDefault(
                    p => p.IDCauHoi == quiz_id &&
                    p.IDDeThi == id_de_thi);
                bool ket_qua = quiz.Is_correct_quiz();
                ds_ch.KetQua = ket_qua;
                db.SaveChanges();
            }
        }

        private void Mark_quiz()
        {
            foreach(var quiz in quizs)
            {
                if(quiz.Id_dap_an_chons.Count > 0)
                {
                    var quiz_type_id = quiz.Cau_hoi.IDLoaiCauHoi;
                    if(quiz_type_id == Constant.ID_CAU_HOI_SAP_XEP_THEO_THU_TU)
                    {
                        Mark_sorted_quiz(quiz);
                    }
                    else if(quiz_type_id == Constant.ID_CAU_HOI_1_DAP_AN)
                    {
                        Mark_one_correct_answer_quiz(quiz);
                    }
                    else if(quiz_type_id == Constant.ID_CAU_HOI_NHIEU_DAP_AN)
                    {
                        Mark_multi_correct_answer_quiz(quiz);
                    }
                    else if(quiz_type_id == Constant.ID_CAU_HOI_DIEN_VAO_CHO_TRONG)
                    {
                        Mark_fill_in_blank_quiz(quiz);
                    }
                }
            }
        }

        private void Mark_fill_in_blank_quiz(Quiz quiz)
        {
            var selected_answer_ids = quiz.Id_dap_an_chons;
            for(int order = 0; order < selected_answer_ids.Count; order++)
            {
                int selected_answer_id = quiz.Id_dap_an_chons[order];
                quiz.Ket_quas.Add(Is_fill_in_blank_answer_correct(order + 1, selected_answer_id, quiz));
            }
        }

        private bool Is_fill_in_blank_answer_correct(int order, int selected_answer_id, Quiz quiz)
        {
            if(selected_answer_id == -1)
            {
                return false;
            }
            // One fill in blank answer is correct when TinhChat = 1 and same order
            var answer = quiz.Cau_hoi.DAPANs.First(p => p.IDDapAn == selected_answer_id);
            if(answer.TinhChat 
                && order == answer.ThuTu)
            {
                return true;
            }
            return false;
        }

        private void Mark_one_correct_answer_quiz(Quiz quiz)
        {
            int correct_answer_id = quiz.Cau_hoi.DAPANs.FirstOrDefault(p => p.TinhChat).IDDapAn;
            int selected_answer_id = quiz.Id_dap_an_chons[0];
            if(correct_answer_id == selected_answer_id)
            {
                quiz.Ket_quas.Add(true);
            }
            else
            {
                quiz.Ket_quas.Add(false);
            }
        }

        private void Mark_sorted_quiz(Quiz quiz)
        {
            for (int order = 0; order < quiz.Id_dap_an_chons.Count; order++)
            {
                int answer_id = quiz.Id_dap_an_chons[order];
                int answer_order = order + 1;
                quiz.Ket_quas.Add(Is_sorted_quiz_answer_correct(answer_id, answer_order, quiz));
            }
        }

        private void Mark_multi_correct_answer_quiz(Quiz quiz)
        {
            for(int i = 0; i < quiz.Id_dap_an_chons.Count; i++)
            {
                int selected_answer_id = quiz.Id_dap_an_chons[i];
                DAPAN dap_an = quiz.Cau_hoi.DAPANs.FirstOrDefault(p => p.IDDapAn == selected_answer_id);
                if(dap_an.TinhChat)
                {
                    quiz.Ket_quas.Add(true);
                }
                else
                {
                    quiz.Ket_quas.Add(false);
                }
            }
        }

        private void Add_user_answers_to_db(THITRACNGHIEMEntities db)
        {
            foreach(var quiz in quizs)
            {
                if(quiz.Id_dap_an_chons.Count > 0)
                {
                    Add_user_answer(quiz, db);
                }
            }
        }

        private void Add_user_answer(Quiz quiz, THITRACNGHIEMEntities db)
        {
            for (int idx = 0; idx < quiz.Id_dap_an_chons.Count; idx++)
            {
                DANHSACHDAPANCHON dap_an_chon = new DANHSACHDAPANCHON();
                int answer_id = quiz.Id_dap_an_chons[idx];
                if(answer_id == -1)
                {
                    continue;
                }
                bool answer_result = quiz.Ket_quas[idx];
                int answer_order;
                if(quiz.Cau_hoi.IDLoaiCauHoi == Constant.ID_CAU_HOI_SAP_XEP_THEO_THU_TU ||
                    quiz.Cau_hoi.IDLoaiCauHoi == Constant.ID_CAU_HOI_DIEN_VAO_CHO_TRONG)
                {
                    answer_order = idx + 1;
                    dap_an_chon.ThuTu = answer_order;
                }
                dap_an_chon.IDDethi = id_de_thi;
                dap_an_chon.IDDapAn = answer_id;
                dap_an_chon.KetQua = answer_result;
                db.DANHSACHDAPANCHONs.Add(dap_an_chon);
                db.SaveChanges();
            }

        }

        private bool Is_sorted_quiz_answer_correct(int answer_id, int answer_order, Quiz quiz)
        {
            DAPAN answer_ground_truth = quiz.Cau_hoi.DAPANs.FirstOrDefault(p => p.IDDapAn == answer_id);
            if(answer_ground_truth.TinhChat 
                && answer_ground_truth.ThuTu == answer_order)
            {
                return true;
            }
            return false;
        }

        
    }
}