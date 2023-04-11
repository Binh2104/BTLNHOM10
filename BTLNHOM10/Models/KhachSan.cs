using System;
using System.Collections.Generic;

namespace BTLNHOM10.Models;

public partial class KhachSan
{
    public string MaKs { get; set; } = null!;

    public string TenKs { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public string Sdt { get; set; } = null!;

    public int XepHangKs { get; set; }
	public string? Anh { get; set; }

	public virtual ICollection<Tour> MaTours { get; } = new List<Tour>();
}
