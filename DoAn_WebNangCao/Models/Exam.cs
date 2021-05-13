﻿using System;
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

        public Quiz GetQuiz(int id_cau_hoi)
        {
            return quizs.FirstOrDefault(quiz => quiz.Cau_hoi.IDCauHoi == id_cau_hoi);
        }

        public int Get_quiz_idx_by_id(int id_cau_hoi)
        {
            return quizs.FindLastIndex(quiz => quiz.Cau_hoi.IDCauHoi == id_cau_hoi);
        }

        public void Add_id_cau_tra_loi(int quiz_idx, int id_cau_tra_loi)
        {
            //quizs.Find(quiz => quiz.Cau_hoi.IDCauHoi == id_cau_hoi).Id_cau_tra_loi = id_cau_tra_loi;
            //quizs[quiz_idx].Id_cau_tra_loi = id_cau_tra_loi;
        }

        private void Add_cau_hoi(IEnumerable<CAUHOI> cau_hois)
        {
            for(int idx = 0; idx < cau_hois.Count(); idx++)
            {
                Quiz quiz = new Quiz(cau_hois.ElementAt(idx), idx);
                quizs.Add(quiz);
            }
        }

        public int Get_correct_answer_id(CAUHOI cau_hoi)
        {
            DAPAN corr_answer = cau_hoi.DAPANs.FirstOrDefault(dapan => dapan.TinhChat == true);
            return corr_answer.IDDapAn;
        }

        public int Count_correct_answers()
        {
            /*
            int count = 0;
            foreach (var quiz in Quizs)
            {
                if (quiz.Id_cau_tra_loi == Get_correct_answer_id(quiz.Cau_hoi))
                {
                    count++;
                }
            }
            return count;
            */
            return -1;
        }

        public void Add_answer_by_id_quiz(int quiz_idx, string raw_answer)
        {
            Quiz quiz = quizs[quiz_idx];
            quiz.Convert_raw_answer_to_list_answers(raw_answer);
        }

        public void Save_answer_of_one_correct_answer_quiz(int quiz_idx, int answer_id)
        {
            quizs[quiz_idx].Save_answer_of_one_correct_answer_quiz(answer_id);
        }

        public void Mark_exam(THITRACNGHIEMEntities db)
        {
            foreach(var quiz in quizs)
            {
                if(quiz.Id_dap_an_chons.Count > 0
                    && quiz.Cau_hoi.IDLoaiCauHoi == Constant.ID_CAU_HOI_SAP_XEP_THEO_THU_TU)
                {
                    Mark_sorted_quiz(quiz, db);
                }
            }
            Add_user_answers_to_db(db);
        }

        public void Mark_sorted_quiz(Quiz quiz, THITRACNGHIEMEntities db)
        {
            for (int order = 0; order < quiz.Id_dap_an_chons.Count; order++)
            {
                int answer_id = quiz.Id_dap_an_chons[order];
                int answer_order = order + 1;
                quiz.Ket_quas.Add(Is_sorted_quiz_answer_correct(answer_id, answer_order, db));
            }
        }

        public void Add_user_answers_to_db(THITRACNGHIEMEntities db)
        {
            foreach(var quiz in quizs)
            {
                // Check whether quiz has answer or not
                if(quiz.Id_dap_an_chons.Count > 0 && 
                    quiz.Cau_hoi.IDLoaiCauHoi == Constant.ID_CAU_HOI_SAP_XEP_THEO_THU_TU)
                {
                    Add_user_answer(quiz, db);
                }
                /*
                else if (quiz.Id_cau_tra_loi != -1)
                {
                    DANHSACHDAPANCHON dap_an_chon = new DANHSACHDAPANCHON();
                    dap_an_chon.IDDethi = Id_de_thi;
                    dap_an_chon.IDDapAn = quiz.Id_cau_tra_loi;
                    dap_an_chon.ThuTu = 0;
                    db.DANHSACHDAPANCHONs.Add(dap_an_chon);
                    db.SaveChanges();
                }
                */
                else
                {
                    continue;
                }
            }
        }

        private void Add_user_answer(Quiz quiz, THITRACNGHIEMEntities db)
        {
            for (int order = 0; order < quiz.Id_dap_an_chons.Count; order++)
            {
                int answer_id = quiz.Id_dap_an_chons[order];
                bool answer_result = quiz.Ket_quas[order];
                int answer_order = order + 1;
                DANHSACHDAPANCHON dap_an_chon = new DANHSACHDAPANCHON();
                dap_an_chon.IDDethi = id_de_thi;
                dap_an_chon.IDDapAn = answer_id;
                dap_an_chon.ThuTu = answer_order;
                dap_an_chon.KetQua = answer_result;
                db.DANHSACHDAPANCHONs.Add(dap_an_chon);
                db.SaveChanges();
            }

        }

        private bool Is_sorted_quiz_answer_correct(int answer_id, int answer_order, THITRACNGHIEMEntities db)
        {
            DAPAN answer_ground_truth = db.DAPANs.FirstOrDefault(p => p.IDDapAn == answer_id);
            if(answer_ground_truth.TinhChat 
                && answer_ground_truth.ThuTu == answer_order)
            {
                return true;
            }
            return false;
        }
    }
}