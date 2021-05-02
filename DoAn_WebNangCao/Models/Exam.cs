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

        public Exam() { }

        public int Id_de_thi { get => id_de_thi; set => id_de_thi = value; }
        public List<Quiz> Quizs { get => quizs; set => quizs = value; }

        public Quiz GetQuiz(int id_cau_hoi)
        {
            return quizs.FirstOrDefault(quiz => quiz.Cau_hoi.IDCauHoi == id_cau_hoi);
        }

        public void Add_id_cau_tra_loi(int id_cau_hoi, int id_cau_tra_loi)
        {
            quizs.Find(quiz => quiz.Cau_hoi.IDCauHoi == id_cau_hoi).Id_cau_tra_loi = id_cau_tra_loi;
        }

        public void Add_cau_hoi(IEnumerable<CAUHOI> cau_hois)
        {
            foreach(var cau_hoi in cau_hois)
            {
                quizs.Add(new Quiz(cau_hoi));
            }
        }

    }
}