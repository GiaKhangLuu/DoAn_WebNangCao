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
    
    public partial class DETHI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DETHI()
        {
            this.CT_DETHI = new HashSet<CT_DETHI>();
        }
    
        public int IDDeThi { get; set; }
        public System.DateTime NgayThi { get; set; }
        public int IDLinhVuc { get; set; }
        public string MucDo { get; set; }
        public System.TimeSpan ThoiGianThi { get; set; }
        public int IDUser { get; set; }
        public Nullable<decimal> TongDiem { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_DETHI> CT_DETHI { get; set; }
        public virtual LINHVUC LINHVUC { get; set; }
        public virtual TAIKHOAN TAIKHOAN { get; set; }
    }
}
