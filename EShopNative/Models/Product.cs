namespace EShopNative.Models
{
    public class Product
    {
        public int Id { get; set; }              // maps to id
        public string Name { get; set; }         // maps to name
        public decimal Price { get; set; }       // maps to price (numeric)
        public string Description { get; set; }  // maps to description
    }
}
