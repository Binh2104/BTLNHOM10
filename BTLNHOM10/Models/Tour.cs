using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace BTLNHOM10.Models;

public partial class Tour
{
    public string MaTour { get; set; } = null!;

    public string TenTour { get; set; } = null!;

    public int GiaCho { get; set; }

    public string? Anh { get; set; }

    public string DiemXuatPhat { get; set; } = null!;

    public int XepHangTour { get; set; }

    public int SoNgayDl { get; set; }

    public virtual ICollection<AnhTour> AnhTours { get; } = new List<AnhTour>();

    public virtual ICollection<Booking> Bookings { get; } = new List<Booking>();

    public virtual ICollection<DiadiemTour> DiadiemTours { get; } = new List<DiadiemTour>();

    public virtual ICollection<KhachSan> MaKs { get; } = new List<KhachSan>();
}
