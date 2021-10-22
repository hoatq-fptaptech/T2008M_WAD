using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T2008M_WAD.Models;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
namespace T2008M_WAD.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Admin/Auth
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Models.Admin user)
        {
            if (ModelState.IsValid)
            {
                var check = db.Users.FirstOrDefault(s => s.Email == user.UserName);// lay user co email nhap vao
                if (check == null)
                {
                    // tao password - SHA256
                    user.Password = GetMD5(user.Password);
                    db.Admins.Add(user);
                    db.SaveChanges();
                    FormsAuthentication.SetAuthCookie(user.UserName, true);
                    return Redirect("~/Admin/Categories");
                }
                else
                {
                    ViewBag.Error = "Username này đã tồn tại";
                }
            }
            return View();
        }

        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] frData = Encoding.UTF8.GetBytes(str);
            byte[] toData = md5.ComputeHash(frData);
            string hashString = "";
            for (int i = 0; i < toData.Length; i++)
            {
                hashString += toData[i].ToString("x2");
            }
            return hashString;
        }
    }
}