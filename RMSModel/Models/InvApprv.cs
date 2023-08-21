using System;
using System.Collections.Generic;

namespace RMSModel.Models;

public partial class InvApprv
{
    public long id { get; set; }

    public long InvoiceId { get; set; }

    public bool? IsApproved { get; set; }

    public string? ApprovedBy { get; set; }

    public string? ApprovedDate { get; set; }

    public string? ApprovedRole { get; set; }
}
