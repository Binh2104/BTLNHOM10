using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BTLNHOM10.Models;

public partial class TaiKhoan
{
    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;
	[Compare("Password")]
	public string ConfirmPassword { get; set; } = null!;


    public int Loai { get; set; }

    public virtual ICollection<KhachHang> KhachHangs { get; } = new List<KhachHang>();

    public virtual ICollection<NhanVien> NhanViens { get; } = new List<NhanVien>();
}
