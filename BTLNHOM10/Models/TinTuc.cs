using System;
using System.Collections.Generic;

namespace BTLNHOM10.Models;

public partial class TinTuc
{
    public string MaTin { get; set; } = null!;

    public string? Anh { get; set; }

    public DateOnly? NgayDang { get; set; }

    public string MaNv { get; set; } = null!;

    public string? MoTa { get; set; }

    public string? NoiDung { get; set; }

    public virtual ICollection<AnhTin> AnhTins { get; } = new List<AnhTin>();

    public virtual NhanVien MaNvNavigation { get; set; } = null!;
}
