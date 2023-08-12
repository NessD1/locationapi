using System;
using System.Collections.Generic;

namespace locationapi.Models;

public partial class Usuario
{
    public Guid? SubjectId { get; set; }

    public int Id { get; set; }

    public int IdDelCliente { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public string NombreDeUsuario { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public string CorreoElectrónico { get; set; } = null!;

    public string? TeléfonoCelular { get; set; }

    public string? TeléfonoAlterno { get; set; }

    public string Rol { get; set; } = null!;

    public virtual ICollection<AccesoAdispositivo> AccesoAdispositivos { get; set; } = new List<AccesoAdispositivo>();

    public virtual Customer IdDelClienteNavigation { get; set; } = null!;
}
