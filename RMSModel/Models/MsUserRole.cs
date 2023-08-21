using System;
using System.Collections.Generic;

namespace RMSModel.Models;

public partial class MsUserRole
{
    public long ID { get; set; }

    public string? NAME { get; set; }

    public string? CODE { get; set; }

    public string? DESCRIPT { get; set; }

    public string? CREATED_BY { get; set; }

    public DateTime? CREATED_DATE { get; set; }

    public virtual ICollection<MsRoleMenu> MsRoleMenu { get; set; } = new List<MsRoleMenu>();

    public virtual ICollection<MsUser> MsUser { get; set; } = new List<MsUser>();
}
