using Models;

namespace Warehouse
{
    internal class PeopleWarehouse : GenericWarehouse<Person>
    {
        protected override Person CreateNewItem()
        {
            return new Person() { FirstName = GetString("Imię:"), LastName = GetString("Nazwisko:"), BirthDate = GetDateTime("Data urodzenia:") };
        }

        protected override Person CreateUpdatedItem(Person old)
        {
            return new Person() { FirstName = GetString($"Imię ({old.FirstName}):"), 
                                    LastName = GetString($"Nazwisko ({old.LastName}):"), 
                                    BirthDate = GetDateTime($"Data urodzenia ({old.BirthDate.ToShortDateString()}):") };
        }

        protected override string GetItemInfo(Person item)
        {
            return $"{item.Id}\t{item.FirstName}\t{item.LastName}\t{item.BirthDate.ToShortDateString()}";
        }
    }
}
