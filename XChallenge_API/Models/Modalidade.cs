using System;
using System.Collections.Generic;

namespace XChallenge_API.Models;

public partial class Modalidade
{
    public string IdModalidade { get; set; } = null!;

    public string? NomeModalidade { get; set; }

    public string? DescModalidade { get; set; }

    public virtual ICollection<DataNacional> DataNacionals { get; set; } = new List<DataNacional>();

    public virtual ICollection<SkillModalidade> SkillModalidades { get; set; } = new List<SkillModalidade>();
}
