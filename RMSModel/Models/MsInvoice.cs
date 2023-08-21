using System;
using System.Collections.Generic;

namespace RMSModel.Models;

public partial class MsInvoice
{
    public long InvId { get; set; }

    public string InvRef { get; set; } = null!;

    public string? InvUplDate { get; set; }

    public int InvAmount { get; set; }

    public int InvOutstanding { get; set; }

    public int? InvPayment { get; set; }

    public bool? InvStatus { get; set; }

    public string? InvRemarks { get; set; }

    public bool? IsDelete { get; set; }
}
