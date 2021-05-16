using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn_WebNangCao.Models
{
    public class Quiz
    {
        private CAUHOI cau_hoi;
        private List<int> id_dap_an_chons;
        private List<bool> ket_quas;
        private int quiz_idx;

        public Quiz(CAUHOI cau_hoi, int quiz_idx)
        {
            this.Cau_hoi = cau_hoi;
            Id_dap_an_chons = new List<int>();
            this.quiz_idx = quiz_idx;
            ket_quas = new List<bool>();
        }

        public Quiz(CAUHOI cau_hoi) {
            this.Cau_hoi = cau_hoi;
            Id_dap_an_chons = new List<int>();
            ket_quas = new List<bool>();
        }
        
        public CAUHOI Cau_hoi { get => cau_hoi; set => cau_hoi = value; }
        public int Quiz_idx { get => quiz_idx; set => quiz_idx = value; }
        public List<int> Id_dap_an_chons { get => id_dap_an_chons; set => id_dap_an_chons = value; }
        public List<bool> Ket_quas { get => ket_quas; set => ket_quas = value; }

        public void Save_answer_of_sorted_answer_quiz(int[] answer_ids)
        {
            id_dap_an_chons.Clear();
            foreach(int answer_id in answer_ids)
            {
                Id_dap_an_chons.Add(answer_id);
            }
        }

        public void Save_answer_of_one_correct_answer_quiz(int answer_id)
        {
            if(id_dap_an_chons.Count == 0)
            {
                id_dap_an_chons.Add(answer_id);
            }
            else
            {
                id_dap_an_chons[0] = answer_id;
            }
        }

        public void Save_answer_of_multi_correct_answer_quiz(List<int> selected_answer_ids)
        {
            id_dap_an_chons.Clear();
            id_dap_an_chons.AddRange(selected_answer_ids);
        }

        public bool Is_correct_quiz()
        {
            // One quiz is correct when num of correct answer is equal and doesnt contain any wrong answer
            if (!Did_quiz_contain_wrong_answer() && Did_user_select_enough_correct_answer())
                return true;
            return false;
        }

        private bool Did_user_select_enough_correct_answer()
        {
            int num_of_selected_correct_answer = ket_quas.Where(p => p == true).Count();
            int num_of_correct_answer = cau_hoi.DAPANs.Where(p => p.TinhChat == true).Count();
            return num_of_correct_answer == num_of_selected_correct_answer;
        }

        private bool Did_quiz_contain_wrong_answer()
        {
            if (ket_quas.Contains(false))
            {
                return true;
            }
            return false;
        }

    }
}