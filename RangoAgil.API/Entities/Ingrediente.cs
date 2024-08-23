namespace RangoAgil.API.Entities;

public class Ingrediente
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Ingrediente()
    {
        
    }

    public Ingrediente(int id, string name)
    {
        Id = id;
        Name = name;
    }
}