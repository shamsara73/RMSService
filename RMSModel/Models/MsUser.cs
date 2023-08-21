using System;
using System.Collections.Generic;

namespace RMSModel.Models;

public partial class MsUser
{
    public long ID { get; set; }

    public string USERNAME { get; set; } = null!;

    public string PASSWORD { get; set; } = null!;

    public string? UserRole { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public bool? IsApproved { get; set; }

    public string? CreatedDate { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsVerified { get; set; }

    public bool? IsDeleted { get; set; }

    public long? IDROLE { get; set; }

    public virtual MsUserRole? IDROLENavigation { get; set; }

    public virtual ICollection<MsUserMenu> MsUserMenu { get; set; } = new List<MsUserMenu>();

    public virtual ICollection<MsUserToken> MsUserToken { get; set; } = new List<MsUserToken>();
}
