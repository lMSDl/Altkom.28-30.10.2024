using Models;

namespace Services.Interfaces
{
    //interfejsy są mechanizmem pozwalającym na definiowanie kontraktów, które klasy lub struktury muszą spełniać, jeśli chcą ten interfejs implementować
    //Nazwa interfejsu zwyczajowo zaczyna się od litery I
    public interface IEntityService<T> where T : Entity
    {
        //domyśnie, wszyscy memberzy interfejsu są publiczni
        void Create(T item);
        List<T> Read();
        T? Read(int id);
        bool Update(int id, T item);
        bool Delete(int id);
    }
}
