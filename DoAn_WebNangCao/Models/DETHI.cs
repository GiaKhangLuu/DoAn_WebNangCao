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
            this.DANHSACHCAUHOIs = new HashSet<DANHSACHCAUHOI>();
            this.DANHSACHDAPANCHONs = new HashSet<DANHSACHDAPANCHON>();
        }
    
        public int IDDeThi { get; set; }
        public System.DateTime NgayThi { get; set; }
        public string MucDo { get; set; }
        public System.TimeSpan ThoiGianThi { get; set; }
        public int IDUser { get; set; }
        public Nullable<decimal> TongDiem { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DANHSACHCAUHOI> DANHSACHCAUHOIs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DANHSACHDAPANCHON> DANHSACHDAPANCHONs { get; set; }
        public virtual TAIKHOAN TAIKHOAN { get; set; }
    }
}
