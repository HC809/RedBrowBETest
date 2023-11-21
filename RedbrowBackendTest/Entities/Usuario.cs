using System;
using System.Collections.Generic;

namespace RedbrowBackendTest.Entities;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int Edad { get; set; }

    public string Correo { get; set; } = null!;
}
