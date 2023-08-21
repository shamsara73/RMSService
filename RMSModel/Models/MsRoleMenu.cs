using System;
using System.Collections.Generic;

namespace RMSModel.Models;

public partial class MsRoleMenu
{
    public long ID { get; set; }

    public long IDROLE { get; set; }

    public long IDMENU { get; set; }

    public int? P_VIEW { get; set; }

    public int? P_ADD { get; set; }

    public int? P_DELETE { get; set; }

    public int? P_UPDATE { get; set; }

    public virtual MsMenu IDMENUNavigation { get; set; } = null!;

    public virtual MsUserRole IDROLENavigation { get; set; } = null!;
}
