
using Models;
using Services.Interfaces;

namespace Services.InMemory
{
    //klasa generyczna
    // where T : Entity - klasy podstawiane za T muszą dziedziczyć po wskazanej klasie
    // w tym przypadku T musi dziedziczyć po Entity ponieważ potrzebujemy informacji o właściwosći Id
    // interfejs implementujemy podobnie jak dziedziczenie, czyli po :
    public class EntityInMemoryService<T> : IEntityService<T>  where T : Entity
    {
        //read-only property - nie posiada settera i można ustawić jego wartość tylko w konstruktorze
        protected List<T> Items { get; }
        public EntityInMemoryService()
        {
            Items = new List<T>();
        }

        public virtual void Create(T item)
        {
            int maxId = Items.Select(x => x.Id).DefaultIfEmpty().Max();
            item.Id = maxId + 1;

            Items.Add(item);
        }

        public virtual bool Delete(int id)
        {
            var item = Read(id);

            if (item != null)
                return Items.Remove(item);
            return false;
        }

        public List<T> Read()
        {
            //towrzymy nową listę (nową instancję) na podstawie iestnijącej listy - kopia listy
            return Items.ToList();
        }

        public T? Read(int id)
        {
            return Items.Where(x => x.Id == id).SingleOrDefault();
        }

        public virtual bool Update(int id, T item)
        {
            T? p = Read(id);
            if (p == null)
                return false;

            int index = Items.IndexOf(p);
            Items.RemoveAt(index);

            item.Id = p.Id;
            Items.Insert(index, item);
            return true;
        }
    }
}
