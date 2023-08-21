using System;
using System.Collections.Generic;

namespace RMSModel.Models;

public partial class TrInvoicePaymentDetail
{
    public long ID { get; set; }

    public long IDINVOICE { get; set; }

    public double? INVOICE_AMOUNT { get; set; }

    public double? PAYMENT_AMOUNT { get; set; }

    public double? OUTSTANDING_AMOUNT { get; set; }

    public DateTime? CREATED_DATE { get; set; }

    public long? CREATED_BY { get; set; }

    public bool? IsPaid { get; set; }

    public bool? IsOutstanding { get; set; }

    public int? OutstandingAmt { get; set; }

    public long? IDPaymentDetail { get; set; }
}
