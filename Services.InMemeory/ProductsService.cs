using Models;

namespace Services.InMemeory
{
    //CRUD - CREATE, READ, UPDATE, DELETE 
    public class ProductsService
    {
        private List<Product> _products;
        public ProductsService()
        {
            _products = new List<Product>();
        }

        public void Create(Product product)
        {
            int maxId = 0;

            foreach(var p in _products)
            {
                if(maxId < p.Id)
                    maxId = p.Id;
            }

            product.Id = maxId + 1;


            _products.Add(product);
        }

        public bool Delete(int id)
        {
            var product = Read(id);

            if(product != null)
                return _products.Remove(product);
            return false;
        }

        public List<Product> Read()
        {
            //towrzymy nową listę (nową instancję) na podstawie iestnijącej listy - kopia listy
            return new List<Product>(_products);
        }

        public Product? Read(int id)
        {
            foreach (var p in _products)
            {
                if (p.Id == id)
                {
                    return p;
                }
            }

            return null;
        }

        public bool Update(int id, Product product)
        {
            Product? p = Read(id);
            if (p == null)
                return false;

            //Delete(id);
            //_products.Add(p);
            int index = _products.IndexOf(p);
            _products.RemoveAt(index);

            product.Id = p.Id;
            _products.Insert(index, product);
            return true;
        }
    }
}
