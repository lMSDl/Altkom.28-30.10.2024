
//namespace ConsoleApp
//{
//internal class Program
//{
//static void Main(string[] args)
//{

//Instrukcja najwyższego poziomu - instrukcja napisana bezpośrednio w pliku bez klasy i namespace
//Jest to zawsze punkt startowy programu (main)
//Dopuszczalny jest tylko jeden plik w projekcie z instrukcjami najwyższego poziomu


Console.WriteLine("Hello, World!");

Console.WriteLine(typeof(Product).Name);
Console.WriteLine(typeof(Product).Namespace);
Console.WriteLine(typeof(Product).FullName);


Product p1 = new Product();
p1.SetName("Test");
Console.WriteLine(p1.GetName());
p1.Price = 100;
Console.WriteLine(p1.Price);
p1.ExpirationDate = DateTime.Now.AddDays(-3);
Console.WriteLine(p1.ExpirationDate);

Console.WriteLine(p1.CreateDescription());

void Nullable() {
    int a = 4;
    Sth1(a);

    int? c = null;
    Sth1(c);

    object? b = null;

    if(b != null)
        Sth2(b);

    int? Sth1(int? val)
    {
        return val;
    }

    object Sth2(object obj)
    {
        return obj;
    }
}

//}
//}
//}