using Models;
using Services.Interfaces;

namespace Services.InFile
{
    public class EntityInFileService<T> : IEntityService<T> where T : Entity
    {
        public void Create(T item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<T> Read()
        {
            throw new NotImplementedException();
        }

        public T Read(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(int id, T item)
        {
            throw new NotImplementedException();
        }
    }
}
