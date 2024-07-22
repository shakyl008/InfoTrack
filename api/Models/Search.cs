using System;
using System.Collections.Generic;

namespace api.Models;

public partial class Search
{
    public int Id { get; set; }

    public string? SearchQuery { get; set; }

    public string? Url { get; set; }

    public string? Positions { get; set; }

    public DateTime? SearchDate { get; set; }
}
