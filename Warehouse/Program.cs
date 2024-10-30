
using Models;
using Warehouse;

//wykorzystanie dziedziczenia i generyczności
//GenericWarehouse<Person> _warehouse = new PeopleWarehouse();

GenericWarehouse<Pet> _warehouse = new DelegateWarehouse<Pet>(() => new Pet { Name = GenericWarehouse<Pet>.GetString("Nazwa:"), Age = GenericWarehouse<Pet>.GetFloat("Wiek:") },
                                                               old => new Pet { Name = GenericWarehouse<Pet>.GetString($"Nazwa ({old.Name}):"), Age = GenericWarehouse<Pet>.GetFloat($"Wiek {old.Age}:") },
                                                               x => $"{x.Id}\t{x.Name}\t{x.Age}" );

bool exit = false;
do
{
    Console.Clear();
    _warehouse.Show();
    Console.WriteLine("Commands: create, edit, delete, exit");
    var input = Console.ReadLine();

    switch (input)
    {
        case "create":
            _warehouse.Create();
            break;
        case "edit":
            _warehouse.Edit();
            break;
        case "delete":
            _warehouse.Delete();
            break;
        case "exit":
            exit = true;
            break;
        default:
            GenericWarehouse<Pet>.ShowInfo("Komenda nie istnieje...");
            break;
    }

} while ( !exit );




