using System;
using System.Collections.Generic;

namespace locationapi.Models;

public partial class DetallesDelVehículo
{
    public int Id { get; set; }

    public string? NombreAsignadoAlDispositivo { get; set; }

    public string? Modelo { get; set; }

    public string? Tipo { get; set; }

    public string? Propietario { get; set; }

    public int IdDeDispositivo { get; set; }

    public virtual Device IdDeDispositivoNavigation { get; set; } = null!;
}
