using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_WebNangCao.Models;

namespace DoAn_WebNangCao.Controllers
{
    public class AccountController : Controller
    {
        THITRACNGHIEMEntities db = new THITRACNGHIEMEntities();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Profiles()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("LoginUser", "Login");
            }
            if (Session["SDT"] == null)
            {
                Session["SDT"] = "";
            }
            if (Session["DiaChi"] == null)
            {
                Session["DiaChi"] = "";
            }
            return View();

        }
        [HttpPost]
        public ActionResult Profiles(TAIKHOAN x)
        {
            try
            {
                x.IDUser = Convert.ToInt32(Session["ID"]);
                TAIKHOAN _user = db.TAIKHOANs.Where(s => s.IDUser == x.IDUser).FirstOrDefault();
                if (x.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(x.UploadImage.FileName);
                    string extent = Path.GetExtension(x.UploadImage.FileName);
                    filename += extent;
                    _user.AnhDaiDien = "~/Content/images/" + filename;
                    x.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                    _user.UploadImage = x.UploadImage;

                }
                else
                {
                    _user.AnhDaiDien = Session["AnhDaiDien"].ToString();
                    _user.UploadImage = x.UploadImage;
                }
                _user.HoTen = x.HoTen;
                _user.SDT = x.SDT;
                _user.DiaChi = x.DiaChi;
                _user.Email = x.Email;
                _user.ConfirmPass = Encryption.Encrypt(x.MatKhau);
                _user.RememberMe = false;
                db.Entry(_user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                Session["HoTen"] = _user.HoTen;
                Session["SDT"] = _user.SDT;
                Session["Email"] = _user.Email;
                Session["DiaChi"] = _user.DiaChi;
                Session["AnhDaiDien"] = _user.AnhDaiDien;
                ViewBag.ThanhCong = "true";
                return View();
            }
            catch (Exception e)
            {
                ViewBag.ThanhCong = "false";
                Console.WriteLine(e.Message);
                return View();
            }
        }
        public ActionResult ChangePassword()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("LoginUser", "Login");
            }
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel changePass)
        {
            var id = int.Parse(Session["ID"].ToString());
            TAIKHOAN _user = db.TAIKHOANs.Where(s => s.IDUser == id).FirstOrDefault();
            if (Encryption.Decrypt(_user.MatKhau) == changePass.OldPass)
            {
                if (changePass.OldPass != changePass.NewPass)
                {
                    Session["MatKhau"] = changePass.NewPass;
                    _user.MatKhau = Encryption.Encrypt(changePass.NewPass);
                    _user.ConfirmPass = _user.MatKhau;
                    db.Entry(_user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    Session["MatKhau"] = changePass.NewPass;
                    if (Session["Remember"].ToString() == "true")
                    {
                        HttpCookie cookie = Request.Cookies["Huflit Quiz"];
                        cookie["MatKhau"] = _user.MatKhau;
                        HttpContext.Response.Cookies.Add(cookie);
                    }
                    ViewBag.ThanhCong = "true";
                    return View();
                }
                else
                {
                    ViewBag.ErrorMessage = "Mật khẩu mới phải khác mật khẩu cũ!";
                    ViewBag.ThanhCong = "false";
                    return View();
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Mật khẩu cũ không trùng khớp!";
                ViewBag.ThanhCong = "false";
                return View();
            }
            
        }
    }
}