using Models;
using Services.InMemeory;
using Warehouse;

Warehouse.Warehouse _warehouse = new();

bool exit = false;
do
{
    Console.Clear();
    _warehouse.ShowProducts();
    Console.WriteLine("Commands: create, edit, delete, exit");
    var input = Console.ReadLine();

    switch (input)
    {
        case "create":
            _warehouse.CreateProduct();
            break;
        case "edit":
            _warehouse.EditProduct();
            break;
        case "delete":
            _warehouse.DeleteProduct();
            break;
        case "exit":
            exit = true;
            break;
        default:
            _warehouse.ShowInfo("Komenda nie istnieje...");
            break;
    }

} while ( !exit );




