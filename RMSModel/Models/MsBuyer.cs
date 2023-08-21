using System;
using System.Collections.Generic;

namespace RMSModel.Models;

public partial class MsBuyer
{
    public long BuyerId { get; set; }

    public string BuyerCode { get; set; } = null!;

    public string BuyerName { get; set; } = null!;

    public string? BuyerAddrs { get; set; }

    public string? BuyerProv { get; set; }

    public string? BuyerCity { get; set; }

    public string? BuyerDist { get; set; }

    public string? BuyerZipCode { get; set; }

    public string? BuyerPhone { get; set; }

    public int? BuyerAccNumber { get; set; }

    public string? BuyerPaymentTo { get; set; }

    public bool? IsDelete { get; set; }

    public string? UserId { get; set; }
}
