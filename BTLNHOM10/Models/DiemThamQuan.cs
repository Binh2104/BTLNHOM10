using System;
using System.Collections.Generic;

namespace BTLNHOM10.Models;

public partial class DiemThamQuan
{
    public string MaDd { get; set; } = null!;

    public string? Mien { get; set; }
    
    public string? MoTa { get; set; }

    public string? TenDiaDiem { get; set; }

    public virtual ICollection<DiadiemTour> DiadiemTours { get; } = new List<DiadiemTour>();
}
