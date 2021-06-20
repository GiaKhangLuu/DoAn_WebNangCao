using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoAn_WebNangCao.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu!")]
        [DataType(DataType.Password)]
        [DisplayName("Mật Khẩu cũ")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Độ dài mật khẩu không phù hợp!(5 - 30 kí tự)")]
        public string OldPass { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu mới!")]
        [DataType(DataType.Password)]
        [DisplayName("Mật Khẩu mới")]
        public string NewPass { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu!")]
        [Compare(otherProperty:"NewPass")]
        [DataType(DataType.Password)]
        [DisplayName("Nhập lại mật khẩu mới")]
        public string ConfirmPass { get; set; }
    }
}