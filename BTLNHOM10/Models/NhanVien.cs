using System;
using System.Collections.Generic;

namespace BTLNHOM10.Models;

public partial class NhanVien
{
    public string GioiTinh { get; set; } = null!;

    public string MaNv { get; set; } = null!;

    public string TenNv { get; set; } = null!;

    public string SoDienThoai { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; } = new List<Booking>();

    public virtual ICollection<TinTuc> TinTucs { get; } = new List<TinTuc>();

    public virtual TaiKhoan UserNameNavigation { get; set; } = null!;
}
