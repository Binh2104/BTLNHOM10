using BTLNHOM10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Thang.Models.Authentication;
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
        [Route("")]
        [Route("Index")]
        [Authentication]
        public IActionResult Index()
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");

            /*  if (Session["U"])*/
            return View();
        }

        //Dia Diem-Tour
        [Route("themdiadiemtourss")]
        [HttpGet]
        [Authentication]
        public IActionResult themdiadiemtourss()
        {
            ViewBag.MaTour = new SelectList(db.Tours.ToList(), "MaTour","TenTour");
            ViewBag.MaDd = new SelectList(db.DiemThamQuans.ToList(), "MaDd", "TenDiaDiem");
            /* ViewBag.MaDd= new SelectList(db.Tours).ToLis*/
            return View();
        }
        [Route("themdiadiemtourss")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authentication]
        public IActionResult themdiadiemtourss(DiadiemTour ddt)
        {         
                db.DiadiemTours.Add(ddt);
                db.SaveChanges();
                return RedirectToAction("DanhSachTour");
        }
        //xoa dia diem cho tour
        //DIEMTHAMQUAN
        [Route("DanhsachDD")]
        [Authentication]
        public IActionResult DanhsachDD()
        {
                      var lstDD = db.DiemThamQuans.OrderBy(x => x.TenDiaDiem).ToList();
      
            return View(lstDD);
        }
       
        //chỉnh sửa thông tin điểm thăm quan
        [Route("ChinhSuaDD")]
        [HttpGet]
        [Authentication]

        public IActionResult ChinhSuaDD(String maDD)
        {
            DiemThamQuan madd = db.DiemThamQuans.Find(maDD);
            return View(madd);
        }
        [Route("ChinhSuaDD")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authentication]
        public IActionResult ChinhSuaDD(DiemThamQuan diadiem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diadiem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhsachDD");
            }
            return View(diadiem);
        }
		// them mot diem tham quan
		[Route("ThemMotDDMoi")]
		[HttpGet]
        [Authentication]
        public IActionResult ThemMotDDMoi()
		{
			return View();
		}
		[Route("ThemMotDDMoi")]
		[HttpPost]
		[ValidateAntiForgeryToken]
        [Authentication]
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
        [Authentication]
        public IActionResult XoaDD(string madd)
        {
            TempData["Message"] = "";
            var listDDT = db.DiadiemTours.Where(x => x.MaDd == madd).ToList();
            if (listDDT.Count() > 0)
            {
                TempData["Message"] = "Không xóa được";
                return RedirectToAction("DanhsachDD");
            }
            db.Remove(db.DiemThamQuans.Find(madd));
            db.SaveChanges();
            return RedirectToAction("DanhsachDD");
        }
        //chi tiet diem tham quan 
        [Route("ChitietDD")]
        [HttpGet]
        [Authentication]
        public IActionResult ChitietDD(string madd)
        {

            var ct = db.DiemThamQuans.SingleOrDefault(m => m.MaDd == madd);
            return View(ct);
        }
        //TOUR
        [Route("DanhsachTour")]
        [Authentication]
        public IActionResult DanhsachTour(int page = 1)
        {
            int pageNumber = page;
            int pageSize = 15;
            var lstTour = db.Tours.OrderBy(x => x.TenTour).ToList();
            PagedList<Tour> lst = new PagedList<Tour>(lstTour, pageNumber, pageSize);
            return View(lst);
        }
        //Chỉnh sửa thông tin tour

        [Route("ChinhSua")]
        [HttpGet]
        [Authentication]
        public IActionResult ChinhSua(String mt)
        {

            Tour sp = db.Tours.Find(mt);
            return View(sp);
        }
        [Route("ChinhSua")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authentication]
        public IActionResult ChinhSua(Tour matours)
        {
            if (ModelState.IsValid)
            {
                db.Entry(matours).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhSachTour");
            }
            return View(matours);
        }
        //Thêm mới Tour
        [Route("ThemMotTourMoi")]
        [HttpGet]
        [Authentication]
        public IActionResult ThemMotTourMoi()
        {
            return View();
        }
        [Route("ThemMotTourMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authentication]
        public IActionResult ThemMotTourMoi(Tour tour)
        {
            if (ModelState.IsValid)
            {
                db.Tours.Add(tour);
                db.SaveChanges();
                return RedirectToAction("DanhSachTour");
            }
            return View(tour);
        }
        //Xóa Tour
        [Route("XoaTour")]
        [HttpGet]
        [Authentication]
        public IActionResult XoaTour(string matour)
        {
            TempData["Message"] = "";
            var listBooking = db.Bookings.Where(x => x.MaTour == matour).ToList();
            if (listBooking.Count() > 0)
            {
               TempData["Message"] = "Không xóa được";
               return RedirectToAction("DanhsachTour");
            }
           
            var list = db.DiadiemTours.Where(x => x.MaTour == matour);
            if (list != null) db.RemoveRange(list);
            db.Remove(db.Tours.Find(matour));
            db.SaveChanges();
            return RedirectToAction("DanhsachTour");
        }
        //khachhang
        [Route("danhsachkh")]
        [Authentication]
        public IActionResult danhsachkh()
        {
           
            var lstkh = db.KhachHangs.OrderBy(x => x.TenKh).ToList();
          
            return View(lstkh);
        }
        //chitietkh
        [Route("chitietkh")]
        [HttpGet]
        [Authentication]
        public IActionResult chitietkh(string makh)
        {

            var ct = db.KhachHangs.SingleOrDefault(m => m.MaKh == makh);
            return View(ct);
        }
        //Booking
        //Danhsachbooking
        [Route("DanhsachBooking")]
        [Authentication]
        public IActionResult DanhsachBooking(int page = 1)
        {
            int pageNumber = page;
            int pageSize = 8;
            var lstt = db.Bookings.OrderBy(x => x.MaBk).ToList();
            PagedList<Booking> lst = new PagedList<Booking>(lstt, pageNumber, pageSize);
            return View(lst);
        }
        // chi tiet booking
        [Route("Chitietbooking")]
        [Authentication]
        public IActionResult Chitietbooking(string mabk)
        {
            var tenkhach = (from i in db.Bookings 
                            join x in db.KhachHangs on i.MaKh equals x.MaKh 
                            join y in db.Tours on i.MaTour equals y.MaTour  
                            join z in db.NhanViens on i.MaNv equals z.MaNv
                            where i.MaBk==mabk select new
                            {
                                tenkhach=x.TenKh,
                                tentour=y.TenTour,
                                tennv= z.TenNv,
                                matour=i.MaTour,
                                maKh=x.MaKh
                            }).ToList();
            ViewBag.tenkhach = tenkhach;
            var ct = db.Bookings.SingleOrDefault(m => m.MaBk == mabk);
            return View(ct);
        }
        //xoa booking 
        [Route("xoabooking")]
        [HttpGet]
        [Authentication]
        public IActionResult xoabooking(string mabk)
        {
            
            db.Remove(db.Bookings.Find(mabk));
            db.SaveChanges();TempData["Message"] = "Thành công";
            return RedirectToAction("danhsachBooking");
        }
        //chinh sua booking

        [Route("chinhsuabooking")]
        [HttpGet]
        [Authentication]
        public IActionResult chinhsuabooking(String mabk)
        {
            ViewBag.MaTour = new SelectList(db.Tours, "MaTour", "TenTour");
            Booking bk = db.Bookings.Find(mabk);
            return View(bk);
        }
        [Route("chinhsuabooking")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authentication]
        public IActionResult chinhsuabooking(Booking mabk)
        {
            if (ModelState.IsValid)
            {               
                db.Entry(mabk).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Sửa Thành Công";
                return RedirectToAction("DanhSachBoogking");
            }
            else
            {
                return View(mabk);
            }
            
        }
        //nhan su
        [Route("danhsachnhansu")]
        [Authentication]
        public IActionResult danhsachnhansu(int page = 1)
        {
            int pageNumber = page;
            int pageSize = 8;
            var lstnv = db.NhanViens.OrderBy(x => x.TenNv).ToList();
            PagedList<NhanVien> lst = new PagedList<NhanVien>(lstnv, pageNumber, pageSize);
            return View(lst);
        }
        //themnhanvien
        [Route("themnhanvien")]
        [HttpGet]
        [Authentication]
        public IActionResult themnhanvien()
        {           
            ViewBag.UserName = new SelectList(db.TaiKhoans, "UserName", "UserName");
            return View();
        }
        [Route("themnhanvien")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authentication]
        public IActionResult themnhanvien(NhanVien nv)
        {
            if (ModelState.IsValid)
            {
                db.NhanViens.Add(nv);
                db.SaveChanges();
                return RedirectToAction("danhsachnhansu");
            }
            return View(nv);
        }
        //Xóa nhân viên 
        [Route("xoaNV")]
        [HttpGet]
        [Authentication]
        public IActionResult xoaNV(string manv)
        {
            var listBooking = db.Bookings.Where(x => x.MaNv == manv).ToList();
            if (listBooking.Count() > 0)
            {
                TempData["Message"] = "Không xóa được";
                return RedirectToAction("danhsachnhansu");
            }
            db.Remove(db.NhanViens.Find(manv));
            db.SaveChanges();
            return RedirectToAction("danhsachnhansu");
        }
        //chinh sua
        [Route("chinhsuaa")]
        [HttpGet]
        [Authentication]
        public IActionResult chinhsuaa(String id)
        {
            ViewBag.UserName = new SelectList(db.TaiKhoans, "UserName", "UserName");
            NhanVien sp = db.NhanViens.Find(id);
            return View(sp);
        }
        [Route("chinhsuaa")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authentication]
        public IActionResult chinhsuaa(NhanVien id)
        {           
 
                db.Entry(id).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("danhsachnhansu");
        }
        //Tài Khoản 
        [Route("danhsachtknv")]
        [Authentication]
        public IActionResult danhsachtknv()
        {
            var lsttknv = (from a in db.TaiKhoans where a.Loai ==1 select a).ToList();
            return View(lsttknv);
        }
        //Xoa tai khoan
        [Route("xoatknv")]
        [HttpGet]
        [Authentication]
        public IActionResult xoatknv(string username)
        {
            var listtknv = db.NhanViens.Where(x => x.UserName == username).ToList();
            if (listtknv.Count() > 0)
            {
                TempData["Message"] = "Không xóa được";
                return RedirectToAction("danhsachtknv");
            }
            db.Remove(db.TaiKhoans.Find(username));
            db.SaveChanges();
            return RedirectToAction("danhsachtknv");
        }
        [Route("themtknv")]
        [HttpGet]
        [Authentication]
        public IActionResult themtknv()
        {
            return View();
        }
        [Route("themtknv")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authentication]
        public IActionResult themtknv(TaiKhoan tk)
        {
            if (ModelState.IsValid)
            {
                db.TaiKhoans.Add(tk);
                db.SaveChanges();
                return RedirectToAction("danhsachtknv");
            }
            return View(tk);
        }
        //TinTuc
        [Route("DanhsachTT")]
        [Authentication]
        public IActionResult DanhsachTT(int page = 1)
        {

            var lstTT = db.TinTucs.OrderBy(x => x.MaTin).ToList();

            return View(lstTT);
        }
        // chi tiet tin tuc
        [Route("ChiTietTinTuc")]
        [HttpGet]
        [Authentication]
        public IActionResult ChiTietTinTuc(string id)
        {
            
            var ct= db.TinTucs.SingleOrDefault(m=>m.MaTin== id);
            return View(ct);
        }
       
        //xoa tin 
        [Route("xoattin")]
        [HttpGet]
        [Authentication]
        public IActionResult xoatin(string matin)
        {
            var listanh = db.AnhTins.Where(x => x.MaTin == matin);
            db.RemoveRange(listanh);
            db.Remove(db.TinTucs.Find(matin));
            db.SaveChanges();
            return RedirectToAction("danhsachTT");
        }
        //them tin 
        [Route("themtin")]
        [HttpGet]
        [Authentication]
        public IActionResult themtin()
        {
            ViewBag.MaNv = new SelectList(db.NhanViens, "MaNv", "TenNv");
            return View();
        }
        [Route("themtin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authentication]
        public IActionResult themtin(TinTuc tk)
        {
            if (ModelState.IsValid)
            {
                db.TinTucs.Add(tk);
                db.SaveChanges();
                return RedirectToAction("danhsachTT");
            }
            return View(tk);
        }


    }
}
