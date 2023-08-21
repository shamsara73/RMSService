using System;
using System.Collections.Generic;

namespace RMSModel.Models;

public partial class VwUserRoleMenu
{
    public long IDUSER { get; set; }

    public string USERNAME { get; set; } = null!;

    public long IDMENU { get; set; }

    public string MENUNAME { get; set; } = null!;

    public string MENULINK { get; set; } = null!;

    public int? MENULEVEL { get; set; }

    public long? MENUPARENT { get; set; }

    public int? P_VIEW { get; set; }

    public int? P_ADD { get; set; }

    public int? P_DELETE { get; set; }

    public int? P_UPDATE { get; set; }
}
