using System;
using System.Collections.Generic;

namespace locationapi.Models;

public partial class LocationHistory
{
    public int Id { get; set; }

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }

    public DateTime? Timestamp { get; set; }

    public string? NúmeroDeControl { get; set; }

    public int? DeviceId { get; set; }

    public virtual Device? Device { get; set; }
}
