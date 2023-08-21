using System;
using System.Collections.Generic;

namespace RMSModel.Models;

public partial class Payment
{
    public long Id { get; set; }

    public string InvoiceNo { get; set; } = null!;

    public int InvoiceTotalAmt { get; set; }

    public string InvoiceDueDate { get; set; } = null!;

    public string CreatedDate { get; set; } = null!;

    public long TransactionId { get; set; }
}
