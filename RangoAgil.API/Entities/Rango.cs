using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace RangoAgil.API.Entities;

public class Rango
{
    public Rango()
    {
    }

    [SetsRequiredMembers]
    public Rango(int id, string name)
    {
        Id = id;
        Name = name;
    }

    [Key] public int Id { get; set; }

    [Required] [MaxLength(200)] public required string Name { get; set; }

    public ICollection<Ingrediente> Ingredientes { get; set; } = [];
}