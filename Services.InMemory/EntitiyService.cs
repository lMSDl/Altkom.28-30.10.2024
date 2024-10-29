
using Models;

namespace Services.InMemory
{
    public class EntitiyService<T> where T : Entity
    {
        private List<T> _items;
        public EntitiyService()
        {
            _items = new List<T>();
        }

        public void Create(T item)
        {
            int maxId = 0;

            foreach (var p in _items)
            {
                if (maxId < p.Id)
                    maxId = p.Id;
            }

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
            return new List<T>(_items);
        }

        public T? Read(int id)
        {
            foreach (var p in _items)
            {
                if (p.Id == id)
                {
                    return p;
                }
            }

            return null;
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
