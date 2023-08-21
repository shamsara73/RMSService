using System;
using System.Collections.Generic;

namespace RMSModel.Models;

public partial class MsUserToken
{
    public long ID { get; set; }

    public long USERID { get; set; }

    public string? ACCESS_TOKEN { get; set; }

    public DateTime? ACCESS_TOKEN_EXPIRY { get; set; }

    public string? REFRESH_TOKEN { get; set; }

    public DateTime? REFRESH_TOKEN_EXPIRY { get; set; }

    public virtual MsUser USER { get; set; } = null!;
}
