using System;
using System.Collections.Generic;

namespace RMSModel.Models;

public partial class PaymentDetail
{
    public long Id { get; set; }

    public string InvoiceNo { get; set; } = null!;

    public int InvoiceAmt { get; set; }

    public string SellerCode { get; set; } = null!;

    public string BuyerCode { get; set; } = null!;

    public string VABank { get; set; } = null!;

    public int VANumber { get; set; }

    public string VAName { get; set; } = null!;

    public bool IsPaid { get; set; }

    public long TransactionId { get; set; }

    public string UserId { get; set; } = null!;
}
