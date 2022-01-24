namespace Core.Entities;

public class Product
{
    public Product()
    {
        Name = string.Empty;
    }

    public int Id { get; set; }
    public string Name { get; set; }
}