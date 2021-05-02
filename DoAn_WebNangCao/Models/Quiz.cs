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

        public Quiz(CAUHOI cau_hoi, int id_cau_tra_loi)
        {
            this.Cau_hoi = cau_hoi;
            this.Id_cau_tra_loi = id_cau_tra_loi;
        }

        public Quiz(CAUHOI cau_hoi) {
            this.Cau_hoi = cau_hoi;
            id_cau_tra_loi = -1;
        }

        
        public int Id_cau_tra_loi { get => id_cau_tra_loi; set => id_cau_tra_loi = value; }
        public CAUHOI Cau_hoi { get => cau_hoi; set => cau_hoi = value; }
    }
}