
using Models;
using System.Globalization;
using Warehouse;
using Warehouse.Properties;

//"ręczne" ustawienie języka aplikacji - domyślnie wybierany będzie język systemowy 
//CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("de");


//wykorzystanie dziedziczenia i generyczności
//GenericWarehouse<Person> _warehouse = new PeopleWarehouse();

GenericWarehouse<Pet> _warehouse = new DelegateWarehouse<Pet>(() => new Pet { Name = GenericWarehouse<Pet>.GetString($"{Resources.name}:"), Age = GenericWarehouse<Pet>.GetFloat("Wiek:") },
                                                               old => new Pet { Name = GenericWarehouse<Pet>.GetString($"{Resources.name} ({old.Name}):"), Age = GenericWarehouse<Pet>.GetFloat($"Wiek {old.Age}:") },
                                                               x => $"{x.Id}\t{x.Name}\t{x.Age}" );

bool exit = false;
do
{
    Console.Clear();
    _warehouse.Show();
    Console.WriteLine($"Commands: {Resources.create}, {Resources.edit}, {Resources.delete}, json, xml, exit");
    var input = Console.ReadLine();

    if(input == Resources.create)
    {
        _warehouse.Create();
    }
    else if (input == Resources.edit)
    {
        _warehouse.Edit();
    }
    else if (input == Resources.delete)
    {
        _warehouse.Delete();
    }
    else if (input == "json")
    {
        _warehouse.ToJson();
    }
    else if (input == "xml")
    {
        _warehouse.ToXml();
    }
    else if (input == "exit")
    {
        exit = true;
    }
    else
    {
        GenericWarehouse<Pet>.ShowInfo(Resources.unknownCommand);
    }

} while ( !exit );




