namespace RangoAgil.API.Entities;

public class Rango
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Rango()
    {
        
    }

    public Rango(int id, string name)
    {
        Id = id;
        Name = name;
    }
}