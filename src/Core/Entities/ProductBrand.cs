namespace Core.Entities
{
    public class ProductBrand : BaseEntity
    {
        public ProductBrand()
        {
            Name = string.Empty;
        }
        
        public string Name { get; set; }
    }
}