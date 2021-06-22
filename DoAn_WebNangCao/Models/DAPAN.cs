//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoAn_WebNangCao.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DAPAN
    {
        THITRACNGHIEMEntities db = new THITRACNGHIEMEntities();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DAPAN()
        {
            this.DANHSACHDAPANCHONs = new HashSet<DANHSACHDAPANCHON>();
        }
    
        public int IDDapAn { get; set; }
        public int IDCauHoi { get; set; }
        public string NoiDung { get; set; }
        public bool TinhChat { get; set; }
        public Nullable<int> ThuTu { get; set; }
    
        public virtual CAUHOI CAUHOI { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DANHSACHDAPANCHON> DANHSACHDAPANCHONs { get; set; }

        public DAPAN Add_dap_an(string noiDung, int idCauHoi, bool tinhChat, int? thuTu)
        {
            this.NoiDung = noiDung;
            this.IDCauHoi = idCauHoi;
            this.TinhChat = tinhChat;
            if (thuTu != null)
            {
                this.ThuTu = thuTu;
            }
            db.DAPANs.Add(this);
            db.SaveChanges();
            return this;
        }
    }
}
