using System;
using System.Collections.Generic;

namespace XChallenge_API.Models;

public partial class Noticium
{
    public int Id { get; set; }

    public DateOnly? Data { get; set; }

    public string? Titulo { get; set; }

    public string? Noticia { get; set; }

    public string? Status { get; set; }
}
