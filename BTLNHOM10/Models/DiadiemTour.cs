using System;
using System.Collections.Generic;

namespace BTLNHOM10.Models;

public partial class DiadiemTour
{
    public string MaDd { get; set; } = null!;

    public string MaTour { get; set; } = null!;

    public string? TenFileAnh { get; set; }

    public string? ViTri { get; set; }

    public virtual DiemThamQuan MaDdNavigation { get; set; } = null!;

    public virtual Tour MaTourNavigation { get; set; } = null!;
}
