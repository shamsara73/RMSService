using System;
using System.Collections.Generic;

namespace RMSModel.Models;

public partial class TrInvoiceUpload
{
    public long ID { get; set; }

    public string? SELLER_CODE { get; set; }

    public string? BUYER_CODE { get; set; }

    public string? INVOICE_NO { get; set; }

    public string? AMOUNT_STRING { get; set; }

    public double? AMOUNT { get; set; }

    public string? DUE_DATE_STRING { get; set; }

    public DateTime? DUE_DATE { get; set; }

    public string? STATUS_RECEIVE { get; set; }

    public DateTime? UPLOAD_DATE { get; set; }

    public long? UPLOAD_BY { get; set; }

    public int? SEQUENCE { get; set; }

    public string? BATCH_NO { get; set; }
}
