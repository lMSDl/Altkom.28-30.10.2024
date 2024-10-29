using Models;
using Services.InMemory;

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
                Name = GetString("Nazwa:"),
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
            //try-catch - służy do obsługi wyjątków.
            //W bloku try umieszczamy kod, w którym może potencjalnie wystąpić wyjątek
            try
            {
                return int.Parse(input);
            }
            //Blok catch zawiera kod, który ma być wykonany jeśli wyjątek wystąpi
            //catch - bez parametru - przechwytuje wszystkie wyjątki i nie daje wglądu w obiekt wyjątku
            catch
            {
                return 0;
            }
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
            try
            {
                var result = float.Parse(input);
                if (result < 0)
                    //throw służy do rzucenia wyjątku. Po tym słowie kluczonym tworzymy wyjątek, który ma zostać rzucony
                    throw new InvalidDataException("Wartość nie może być ujemna.");

                return result;
            }
            //możemy mieć wiele bloków catch w kolejność od szczegółu do ogółu (najbardziej ogólny wyjątek na końcu)
            //dzięki temu możemy wykonywać różne akcje w zależności od klasy wyjątku
            //catch() - z parametrem - przechwytuje wyjątki zgodne z klasą parametru, dając wgląd w obiekt
            catch (FormatException e) //korzystamy z właściwości wyjątku, więc nazywamy jego obiekt jako "e", żeby mieć do niego dostęp
            {
                ShowInfo($"Error: {e.Message}");
                //throw bez dodatkowego kodu służy do rzucenia ponownie wyjątku, który właśnie obsługujemy
                throw;
            }
            catch (Exception e)
            {
                ShowInfo($"Error: {e.Message}");
                return GetFloat(label);
            }
        }

        private DateTime GetDateTime(string label)
        {
            var input = GetString(label);
            return DateTime.Parse(input);
        }
    }
}
