namespace RangoAgil.API.Models;

public class IngredienteDTO
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public int RangoId { get; set; }
}