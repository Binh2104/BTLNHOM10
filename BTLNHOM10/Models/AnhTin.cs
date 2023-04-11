using System;
using System.Collections.Generic;

namespace BTLNHOM10.Models;

public partial class AnhTin
{
    public string TenFileAnh { get; set; } = null!;

    public string? ViTri { get; set; }

    public string MaTin { get; set; } = null!;

    public virtual TinTuc MaTinNavigation { get; set; } = null!;
}
