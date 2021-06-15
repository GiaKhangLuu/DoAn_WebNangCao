using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn_WebNangCao.Models
{
    public class Result
    {
        private int num_of_correct_answers, num_of_quizs;
        private double score;

        public Result() { }

        public int Num_of_correct_answers { get => num_of_correct_answers; set => num_of_correct_answers = value; }
        public int Num_of_quizs { get => num_of_quizs; set => num_of_quizs = value; }
        public double Score { get => score; set => score = value; }
    }
}