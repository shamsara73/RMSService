using System;
using System.Collections.Generic;

namespace RMSModel.Models;

public partial class MsSeller
{
    public long SellerId { get; set; }

    public string SellerCode { get; set; } = null!;

    public string SellerName { get; set; } = null!;

    public string? SellerAddrs { get; set; }

    public string? SellerProv { get; set; }

    public string? SellerCity { get; set; }

    public string? SellerDist { get; set; }

    public string? SellerZipCode { get; set; }

    public string? SellerPhone { get; set; }

    public int? SellerAccNumber { get; set; }

    public bool? IsDelete { get; set; }

    public long? UserId { get; set; }
}
