using System;
using System.Collections.Generic;

namespace locationapi.Models;

public partial class ConfiguraciónGeneral
{
    public int Id { get; set; }

    public int IdDeCliente { get; set; }

    public int DistanciaPermitida { get; set; }

    public int SegundosDeEspera { get; set; }

    public virtual Customer IdDeClienteNavigation { get; set; } = null!;
}
