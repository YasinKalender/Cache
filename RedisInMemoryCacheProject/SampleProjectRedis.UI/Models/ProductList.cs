namespace SampleProjectRedis.Models
{
    public static class ProductList
    {
        public static List<Product> GetAllProduct()
        {
            var products = new List<Product>();

            products.Add(new Product() { Name = "Product" });
            products.Add(new Product() { Name = "Product2" });

            return products;

        }
    }
}
