using Microsoft.AspNetCore.Mvc;
using BTLNHOM10.Models;


namespace BTLNHOM10.Controllers
{
    public class AccessController : Controller
    {
        QlTourdlN5Context db = new QlTourdlN5Context();

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public IActionResult Login(TaiKhoan taiKhoan, int admin = 1, int user = 2)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var tk1 = db.TaiKhoans.Where(x => x.UserName.Equals(taiKhoan.UserName) && x.Password.Equals(taiKhoan.Password) && x.Loai.Equals(1)).ToList().FirstOrDefault();
                var tk2 = db.TaiKhoans.Where(x => x.UserName.Equals(taiKhoan.UserName) && x.Password.Equals(taiKhoan.Password) && x.Loai.Equals(2)).ToList().FirstOrDefault();
                //var l = db.TaiKhoans.Count(m => m.Loai == loai);

                if (tk1 != null)
                {

                    HttpContext.Session.SetString("UserName", tk1.UserName.ToString());
                    return Redirect("~/Home/Index");
                }
                if (tk2 != null)
                {

                    HttpContext.Session.SetString("UserName", tk2.UserName.ToString());
                    return Redirect("~/Admin/Index");
                }
            }

            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Index", "Home");
        }
    }
}
