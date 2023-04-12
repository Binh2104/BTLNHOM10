using BTLNHOM10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;
using X.PagedList;

namespace BTLNHOM10.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    [Route("Admin/HomeAdmin")]
    public class HomeAdminController : Controller
    {
        QlTourdlN5Context db = new QlTourdlN5Context();
        //test
        string username = "";
        [Route("")]
        [Route("Index")]
        
        public IActionResult Index()
        {
            /*  if (Session["U"])*/
            return View();
        }
        //Dia Diem-Tour
        [Route("themdiadiemtours")]
        [HttpGet]
        public IActionResult themdiadiemtours()
        {
            var t = db.Tours.ToList();
            ViewBag.MaTour = new SelectList(db.Tours.ToList(), "MaTour", "TenTour");
            ViewBag.MaDd = new SelectList(db.DiemThamQuans.ToList(), "MaDd","TenDiaDiem");
            return View();
        }
        [Route("themdiadiemtours")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult themdiadiemtours(DiadiemTour ddt)
        {
            if (ModelState.IsValid)
            {
                db.DiadiemTours.Add(ddt);
                db.SaveChanges();
                TempData["Message"] = "Thanh cong";
                return RedirectToAction("DanhsachTour");
            }
            else
            {
                TempData["Message"] = "Loi";
            }
            return View(ddt);
        }
        //TinTuc
        [Route("DanhsachTT")]
        public IActionResult DanhsachTT(int page = 1)
        {
            int pageNumber = page;
            int pageSize = 8;
            var lstTT = db.TinTucs.OrderBy(x => x.MaTin).ToList();
            PagedList<TinTuc> lst = new PagedList<TinTuc>(lstTT, pageNumber, pageSize);
            return View(lst);
        }
        [Route("ThemMotTTMoi")]
        [HttpGet]
        public IActionResult ThemMotTTMoi()
        {
            return View();
        }
        [Route("ThemMotTTMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemMotTTMoi(TinTuc tt)
        {
            if (ModelState.IsValid)
            {
                db.TinTucs.Add(tt);
                db.SaveChanges();
                return RedirectToAction("DanhSachTT");
            }
            return View(tt);
        }
        //DIEMTHAMQUAN
        [Route("DanhsachDD")]
        public IActionResult DanhsachDD(int page = 1)
        {
            int pageNumber = page;
            int pageSize = 8;
            var lstDD = db.DiemThamQuans.OrderBy(x => x.TenDiaDiem).ToList();
            PagedList<DiemThamQuan> lst = new PagedList<DiemThamQuan>(lstDD, pageNumber, pageSize);
            return View(lst);
        }
       
        //chỉnh sửa thông tin điểm thăm quan
        [Route("ChinhSuaDD")]
        [HttpGet]

        public IActionResult ChinhSuaDD(String maDD)
        {

            DiemThamQuan madd = db.DiemThamQuans.Find(maDD);
            return View(madd);
        }
        [Route("ChinhSuaDD")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChinhSuaDD(DiemThamQuan diadiem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diadiem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(diadiem);
        }
		// them mot diem tham quan
		[Route("ThemMotDDMoi")]
		[HttpGet]
		public IActionResult ThemMotDDMoi()
		{
			return View();
		}
		[Route("ThemMotDDMoi")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult ThemMotDDMoi(DiemThamQuan dd)
		{
			if (ModelState.IsValid)
			{
				db.DiemThamQuans.Add(dd);
				db.SaveChanges();
				return RedirectToAction("DanhsachDD");
			}
			return View(dd);
		}
        //xóa điểm thăm quan
        [Route("XoaDD")]
        [HttpGet]
        public IActionResult XoaDD(string madd)
        {
            TempData["Message"] = "";
            var listDD = db.DiemThamQuans.Where(x => x.MaDd == madd);
            foreach (var item in listDD)
            {
                if (db.DiadiemTours.Where(x => x.MaDd == item.MaDd) != null)
                {
                    return RedirectToAction("DanhsachDD");
                }
            }
            var listAnh = db.DiadiemTours.Where(x => x.MaDd == madd);
            if (listAnh != null) db.RemoveRange(listAnh);
            if (listDD != null) db.RemoveRange(listDD);
            db.Remove(db.DiemThamQuans.Find(madd));
            db.SaveChanges();
            return RedirectToAction("DanhsachDD");
        }
        //TOUR
        [Route("DanhsachTour")]
        public IActionResult DanhsachTour(int page = 1)
        {
            int pageNumber = page;
            int pageSize = 8;
            var lstTour = db.Tours.OrderBy(x => x.TenTour).ToList();
            PagedList<Tour> lst = new PagedList<Tour>(lstTour, pageNumber, pageSize);
            return View(lst);
        }
        //Chỉnh sửa thông tin tour

        [Route("ChinhSua")]
        [HttpGet]

        public IActionResult ChinhSua(String matour)
        {

            Tour sp = db.Tours.Find(matour);
            return View(sp);
        }
        [Route("ChinhSua")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChinhSua(Tour matour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(matour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhSachTour");
            }
            return View(matour);
        }
        //Thêm mới Tour
        [Route("ThemMotTourMoi")]
        [HttpGet]
        public IActionResult ThemMotTourMoi()
        {
            return View();
        }
        [Route("ThemMotTourMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemMotTourMoi(Tour tour)
        {
            if (string.IsNullOrEmpty(tour.MaTour) == true)
            {
                ModelState.AddModelError("", "Mã Tour Không Được Bỏ Trống!");
                return View(tour);
            }
            if (string.IsNullOrEmpty(tour.TenTour)== true)
            {
                ModelState.AddModelError("", "Tên Tour Không Được Bỏ Trống!");
                return View(tour);
            }
            if (string.IsNullOrEmpty(tour.DiemXuatPhat) == true)
            {
                ModelState.AddModelError("", "Điểm Xuất Phát Không Được Bỏ Trống!");
                return View(tour);
            }
             if (string.IsNullOrEmpty(tour.GiaCho.ToString()) == true)
            {
                ModelState.AddModelError("", "Giá Chỗ Không Được Bỏ Trống!");
                return View(tour);
            }
            db.Tours.Add(tour);
            db.SaveChanges();
            if (tour.MaTour.Length > 0)
            {
                return RedirectToAction("DanhSachTour");
            }
            else
            {
                ModelState.AddModelError("", "Lỗi Thêm!");
                return View(tour);  
            }

            /*  if (ModelState.IsValid)
              {
                  db.Tours.Add(tour);
                  db.SaveChanges();
                  return RedirectToAction("Index");
              }
              return View(tour);*/
        }
        //Xóa Tour
        [Route("XoaTour")]
        [HttpGet]
        public IActionResult XoaTour(string matour)
        {
            TempData["Message"] = "";
            var listBooking = db.Bookings.Where(x => x.MaTour == matour).ToList();
            if (listBooking.Count() > 0)
            {
               TempData["Message"] = "Không xóa được";
               return RedirectToAction("DanhsachTour");
            }
            var listAnh = db.AnhTours.Where(x => x.MaTour == matour);
            if (listAnh != null) db.RemoveRange(listAnh);
            db.Remove(db.Tours.Find(matour));
            db.SaveChanges();
            return RedirectToAction("DanhsachTour");
        }
    }
}
