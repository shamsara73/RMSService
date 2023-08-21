using System;
using System.Collections.Generic;

namespace RMSModel.Models;

public partial class MsParameter
{
    public long ID { get; set; }

    public long? PRM_VALUE { get; set; }

    public string? PRM_TEXT { get; set; }

    public string? PRM_DESC { get; set; }

    public string? PRM_DESC2 { get; set; }

    public long? CREATED_BY { get; set; }

    public DateTime? CREATED_DATE { get; set; }
}
