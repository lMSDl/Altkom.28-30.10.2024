
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


Product p2 = new Product("Marchewka", 3.5f);
Console.WriteLine(p2.CreateDescription());


Person person1 = new Person();
person1.FirstName = "Ewa";
person1.LastName = "Ewowska";
person1.BirthDate = DateTime.Now.AddYears(-35);

Console.WriteLine(person1.Bio());


Person person2 = new Person();
//person2.FirstName = "Adam";
person2.LastName = "Adamski";
person2.BirthDate = new DateTime(1976, 4, 15);

Console.WriteLine(person2.Bio());

Person person3 = new Person("Monika", "Monikowska");
Person person4 = new Person("Edward", "Edwardowski", DateTime.Now.AddYears(-23));

Console.WriteLine(person3.Bio());
Console.WriteLine(person4.Bio());

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