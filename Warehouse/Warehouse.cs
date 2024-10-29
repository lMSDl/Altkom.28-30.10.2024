using Models;
using Services.InMemeory;

namespace Warehouse
{
    internal class Warehouse
    {
        ProductsService _service = new ProductsService();

        public void ShowProducts()
        {
            foreach (Product product in _service.Read())
            {
                Console.WriteLine($"{product.Id}\t{product.Name}\t{product.Price}\t{product.ExpirationDate.ToShortDateString()}");
            }
        }

        public void CreateProduct()
        {
            var product = new Product
            {
                Name = GetString("Nazwa:")!,
                Price = GetFloat("Cena:"),
                ExpirationDate = GetDateTime("Data przydatności:")
            };

            _service.Create(product);
        }

        public void DeleteProduct()
        {
            int id = GetId();

            var result = _service.Delete(id);
            if (!result)
            {
                ShowInfo("Produkt o danym Id nie mógł być usunięty lub nie istnieje");
            }
        }

        public void EditProduct()
        {
            int id = GetId();
            var old = _service.Read(id);

            if (old == null)
            {
                ShowInfo("Product o danym Id nie istnieje");
                return;
            }

            var product = new Product
            {
                Name = GetString($"Nazwa ({old.Name}):"),
                Price = GetFloat($"Cena ({old.Price}):"),
                ExpirationDate = GetDateTime($"Data przydatności ({old.ExpirationDate.ToShortDateString()}):")
            };

            _ = _service.Update(id, product);
        }

        public void ShowInfo(string output)
        {
            Console.WriteLine(output);
            //_ = - discard, używany gdy chcemy zaznaczyć, że wiemy iż metoda zwraca jakiś rezultat, ale nie jest on nam potrzebny / nie interesuje nas
            _ = Console.ReadLine();
        }

        private int GetId()
        {
            var input = GetString("Id:");
            return int.Parse(input);
        }

        private string GetString(string label)
        {
            Console.WriteLine(label);

            //metoda zwraca nullowany obiekt, który przypisujemy do nie-nullowalnego
            //! - za pomocą wykrzyknika wyciszamy ostrzeżenie o niedopasowaniu typów nullowalnych, ponieważ w tym przypadku nie oczekujemy, że null wystąpi
            return Console.ReadLine()!;
        }

        private float GetFloat(string label) {
            var input = GetString(label);
            return float.Parse(input);
        }

        private DateTime GetDateTime(string label)
        {
            var input = GetString(label);
            return DateTime.Parse(input);
        }
    }
}
