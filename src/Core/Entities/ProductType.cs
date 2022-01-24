namespace Core.Entities
{
    public class ProductType : BaseEntity
    {
        public ProductType()
        {
            Name = string.Empty;
        }

        public string Name { get; set; }
    }
}