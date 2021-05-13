using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn_WebNangCao.Models
{
    public class Quiz
    {
        private CAUHOI cau_hoi;
        private int id_cau_tra_loi;
        private List<int> id_dap_an_chons;
        private string id_dap_an_chons_raw;
        private int quiz_idx;

        public Quiz(CAUHOI cau_hoi, int quiz_idx)
        {
            this.Cau_hoi = cau_hoi;
            id_cau_tra_loi = -1;
            Id_dap_an_chons = new List<int>();
            Id_dap_an_chons_raw = "";
            this.quiz_idx = quiz_idx;
        }

        public Quiz(CAUHOI cau_hoi) {
            this.Cau_hoi = cau_hoi;
            id_cau_tra_loi = -1;
            Id_dap_an_chons = new List<int>();
            Id_dap_an_chons_raw = "";
        }
        
        public int Id_cau_tra_loi { get => id_cau_tra_loi; set => id_cau_tra_loi = value; }
        public CAUHOI Cau_hoi { get => cau_hoi; set => cau_hoi = value; }
        public string Id_dap_an_chons_raw { get => id_dap_an_chons_raw; set => id_dap_an_chons_raw = value; }
        public int Quiz_idx { get => quiz_idx; set => quiz_idx = value; }
        public List<int> Id_dap_an_chons { get => id_dap_an_chons; set => id_dap_an_chons = value; }

        public void Convert_raw_answer_to_list_answers(string raw_answer)
        {
            string[] answer_ids = raw_answer.Split(',');
            foreach(string answer_id in answer_ids)
            {
                Id_dap_an_chons.Add(Int32.Parse(answer_id));
            }
        }
    }
}