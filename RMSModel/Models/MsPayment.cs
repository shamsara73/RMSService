using System;
using System.Collections.Generic;

namespace RMSModel.Models;

public partial class MsPayment
{
    public long PaymentId { get; set; }

    public string PaymentCode { get; set; } = null!;

    public long BuyerId { get; set; }

    public long SellerId { get; set; }

    public long PaymentDate { get; set; }

    public bool IsPaid { get; set; }
}
