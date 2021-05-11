﻿//------------------------------------------------------------------------------
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
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class TAIKHOAN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TAIKHOAN()
        {
            this.DETHIs = new HashSet<DETHI>();
        }
    
        public int IDUser { get; set; }
        [Required(ErrorMessage ="Tên đăng nhập không được để trống ...")]
        [StringLength(25,MinimumLength =8)]
        
        public string UserName { get; set; }
        [Required(ErrorMessage ="Mật khẩu không được để trống...")]
        [DataType(DataType.Password)]
        [StringLength(16,MinimumLength =8)]
        public string MatKhau { get; set; }
        [NotMapped]
        [Required(ErrorMessage ="Vui lòng nhập lại mật khẩu ....")]
        [Compare("MatKhau")]
        [DataType(DataType.Password)]
        public string ConfirmPass { get; set; }
        [Required(ErrorMessage = "Họ tên không được trống ....")]
        public string HoTen { get; set; }
        [Required(ErrorMessage = "Địa chỉ Email không được trống ....")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool Quyen { get; set; }
        public string AnhDaiDien { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETHI> DETHIs { get; set; }
    }
}
