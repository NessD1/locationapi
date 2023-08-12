using System;
using System.Collections.Generic;

namespace locationapi.Models;

public partial class InventarioActual
{
    public string NumeroDeParte { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Proveedor { get; set; }

    public string? Manufacturador { get; set; }

    public string? EmpaquetadoIntrinseco { get; set; }

    public string? EmpaquetadoExtrinseco { get; set; }

    public int Cantidad { get; set; }
}
