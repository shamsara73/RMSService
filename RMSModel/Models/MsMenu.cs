using System;
using System.Collections.Generic;

namespace RMSModel.Models;

public partial class MsMenu
{
    public long ID { get; set; }

    public string NAME { get; set; } = null!;

    public string LINK { get; set; } = null!;

    public int? LEVEL { get; set; }

    public long? PARENT { get; set; }

    public virtual ICollection<MsRoleMenu> MsRoleMenu { get; set; } = new List<MsRoleMenu>();

    public virtual ICollection<MsUserMenu> MsUserMenu { get; set; } = new List<MsUserMenu>();
}
