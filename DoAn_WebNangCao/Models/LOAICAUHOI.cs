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
    
    public partial class LOAICAUHOI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LOAICAUHOI()
        {
            this.CAUHOIs = new HashSet<CAUHOI>();
        }
    
        public int IDLoaiCauHoi { get; set; }
        public string TenLoaiCauHoi { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAUHOI> CAUHOIs { get; set; }
    }
}
