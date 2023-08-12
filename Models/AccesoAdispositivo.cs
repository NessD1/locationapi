using System;
using System.Collections.Generic;

namespace locationapi.Models;

public partial class AccesoAdispositivo
{
    public int Id { get; set; }

    public int IdDeDispositivoPermitido { get; set; }

    public int IdDeUsuario { get; set; }

    public virtual Usuario IdDeUsuarioNavigation { get; set; } = null!;
}
