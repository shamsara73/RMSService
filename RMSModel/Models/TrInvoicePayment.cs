using System;
using System.Collections.Generic;

namespace RMSModel.Models;

public partial class TrInvoicePayment
{
    public long ID { get; set; }

    public string? PAYMENT_METHOD { get; set; }

    public double? TOTAL_AMOUNT { get; set; }

    public DateTime? CREATED_DATE { get; set; }

    public long CREATED_BY { get; set; }
}
