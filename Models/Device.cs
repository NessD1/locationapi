using System;
using System.Collections.Generic;

namespace locationapi.Models;

public partial class Device
{
    public int Id { get; set; }

    public string Imei { get; set; } = null!;

    public string NúmeroDeControl { get; set; } = null!;

    public int CustomerId { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsHistoryActive { get; set; }

    public string? LatitudActual { get; set; }

    public string? LongitudActual { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual DetallesDelVehículo? DetallesDelVehículo { get; set; }

    public virtual ICollection<LocationHistory> LocationHistories { get; set; } = new List<LocationHistory>();
}
