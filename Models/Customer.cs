using System;
using System.Collections.Generic;

namespace locationapi.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? MobilePhone { get; set; }

    public string? CountryCode { get; set; }

    public string? PlanDePago { get; set; }

    public virtual ConfiguraciónGeneral? ConfiguraciónGeneral { get; set; }

    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
