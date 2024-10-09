using Bogus;
using Catalog.API.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(x => true).Any();
            if (!existProduct)
            {
                productCollection.InsertMany(GetPreconfiguredProducts());
            }
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            var faker = new Faker();
            
            List<Product> products = new();
            
            for (int i = 0; i < 100; i++)
            {
                products.Add(new Product
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = faker.Commerce.ProductName(),
                    Description = faker.Commerce.ProductDescription(),
                    Category = faker.Commerce.Department(),
                    Image = faker.Image.LoremFlickrUrl(),
                    Price = faker.Finance.Amount(1, 9999999)
                });
            }
            
            return products;
        }
    }
}
