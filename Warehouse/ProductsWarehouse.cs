using Models;
using Services.InMemory;

namespace Warehouse
{
    //dziedziczymy po klasie generycznej i abstakcyjnej dlatego musimy zapewnić ciało dla funkcji abstrakcyjnych z klasy bazowej
    internal class ProductsWarehouse : GenericWarehouse<Product>
    {
        protected override Product CreateNewItem()
        {
            return new Product
            {
                Name = GetString("Nazwa:"),
                Price = GetFloat("Cena:"),
                ExpirationDate = GetDateTime("Data przydatności:")
            };
        }

        protected override Product CreateUpdatedItem(Product old)
        {
            return new Product
            {
                Name = GetString($"Nazwa ({old.Name}):"),
                Price = GetFloat($"Cena ({old.Price}):"),
                ExpirationDate = GetDateTime($"Data przydatności ({old.ExpirationDate.ToShortDateString()}):")
            };
        }

        protected override string GetItemInfo(Product item)
        {
            return $"{item.Id}\t{item.Name}\t{item.Price}\t{item.ExpirationDate.ToShortDateString()}";
        }
    }
}
