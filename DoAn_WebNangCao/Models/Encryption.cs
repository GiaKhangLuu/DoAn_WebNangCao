using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace DoAn_WebNangCao.Models
{
    public class Encryption
    {
        public static string Encrypt(string text)
        {
            string hash = "Password@2021$";
            byte[] data = UTF8Encoding.UTF8.GetBytes(text);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB };
            ICryptoTransform transform = tripDes.CreateEncryptor();
            byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
            return Convert.ToBase64String(results);
        }
        public static string Decrypt(string text)
        {
            string hash = "Password@2021$";
            byte[] data = Convert.FromBase64String(text);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB};
            ICryptoTransform transform = tripDes.CreateDecryptor();
            byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
            return UTF8Encoding.UTF8.GetString(results);
        }

    }
}