using System;
using System.Collections.Generic;

namespace BTLNHOM10.Models;

public partial class AnhTour
{
    public string TenFileAnh { get; set; } = null!;

    public string? ViTri { get; set; }

    public string MaTour { get; set; } = null!;

    public virtual Tour MaTourNavigation { get; set; } = null!;
}
