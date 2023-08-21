using System;
using System.Collections.Generic;

namespace RMSModel.Models;

public partial class AuditLog
{
    public long Id { get; set; }

    public string? UserId { get; set; }

    public string? CreatedDate { get; set; }

    public string? Notes { get; set; }

    public string? IP { get; set; }
}
