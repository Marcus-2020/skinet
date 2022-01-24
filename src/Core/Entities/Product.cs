namespace Core.Entities;

public class Product : BaseEntity
{
    public Product()
    {
        Name = string.Empty;
    }
    
    public string Name { get; set; }
}