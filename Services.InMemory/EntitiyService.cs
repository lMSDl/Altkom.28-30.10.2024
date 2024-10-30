
using Models;

namespace Services.InMemory
{
    //klasa generyczna
    // where T : Entity - klasy podstawiane za T muszą dziedziczyć po wskazanej klasie
    // w tym przypadku T musi dziedziczyć po Entity ponieważ potrzebujemy informacji o właściwosći Id
    public class EntitiyService<T> where T : Entity
    {
        private List<T> _items;
        public EntitiyService()
        {
            _items = new List<T>();
        }

        public void Create(T item)
        {
            int maxId = _items.Max(x => x.Id);
            item.Id = maxId + 1;

            _items.Add(item);
        }

        public bool Delete(int id)
        {
            var item = Read(id);

            if (item != null)
                return _items.Remove(item);
            return false;
        }

        public List<T> Read()
        {
            //towrzymy nową listę (nową instancję) na podstawie iestnijącej listy - kopia listy
            return _items.ToList();
        }

        public T? Read(int id)
        {
            return _items.Where(x => x.Id == id).SingleOrDefault();
        }

        public bool Update(int id, T item)
        {
            T? p = Read(id);
            if (p == null)
                return false;

            int index = _items.IndexOf(p);
            _items.RemoveAt(index);

            item.Id = p.Id;
            _items.Insert(index, item);
            return true;
        }
    }
}
