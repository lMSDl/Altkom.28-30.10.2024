Warehouse.PeopleWarehouse _warehouse = new();

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
            _warehouse.ShowInfo("Komenda nie istnieje...");
            break;
    }

} while ( !exit );




