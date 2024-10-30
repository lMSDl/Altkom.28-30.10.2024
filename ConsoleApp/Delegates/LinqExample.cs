using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Delegates
{
    internal class LinqExample
    {
        int[] numbers = new[] { 1, 2, 5, 7, 3, 8, 0, 9 };

        List<string> strings = "Ala ma kota i dwa psy".Split(' ').ToList();

        List<Person> people = new List<Person>()
        {
            new Person("Adam","Adamski", DateTime.Now.AddYears(-23) ),
            new Person("Ewa","Adamska", DateTime.Now.AddYears(-32) ),
            new Person("Adam","Ewowski", DateTime.Now.AddYears(-35) ),
            new Person("Ewa","Ewowska", DateTime.Now.AddYears(-63) ),
            new Person("Piotr","Piotrowska", DateTime.Now.AddYears(-33) ),
            new Person("Piotr","Adamski", DateTime.Now.AddYears(-66) ),
            new Person("Ewa","Piotrowska", DateTime.Now.AddYears(-72) ),
            new Person("Piotr","Ewowski", DateTime.Now.AddYears(-42) )
        };

        public void Test()
        {
            //ToList() - jedna z form zakończenia zapytania LINQ
            var result1 = numbers.Where(x => x > 4).ToList();
            var result2 = numbers.Where(x => x < 4).OrderByDescending(x => x).ToList();

            var result3 = people.Where(x => x.BirthDate.Value.Year > 1970).Select(x => $"{x.FirstName} {x.LastName}").ToList();
            var result4 = people.Where(x => x.FirstName == "Adam")//.Single(); //single zwraca JEDYNY rezultat
                                                                  //.SingleOrDefault(); //orDefault - zwraca wartość domyślną jeśli nie ma żadnego obiektu
                                                                  //.First(); //zwraca pierwszy znaleziony
                                                                  //.FirstOrDefault();
                                                                  .Last(); //zwraca ostatnio pasujący element
                                                                           //LastOrDefault();

            var result5 = people.Where(x => x.FirstName == "Adam").Where(x => x.LastName.StartsWith("E")).Single();
            var result6 = people.Where(x => x.FirstName == "Adam" || x.FirstName == "Ewa").ToList();

            var result7 = people.Select(x => x.FirstName).Skip(2).Take(3).Aggregate((a, b) => $"{a}, {b}");

            //1. posortować kolekcję strings po ilości liter w wyrazach
            var result8 = strings.OrderBy(x => x.Length)/*.ThenByDescending(x => x)*/.ToList();
            //2. Zsumować wartości kolekcji numbers
            var result9 = numbers/*.Where(x => x % 2 == 0)*/.Sum();
            //3. Z People wybrać osoby, które mają na imię Piotr lub Ewa
            var result10 = people.Where(x => x.FirstName == "Piotr" || x.FirstName == "Ewa").ToList();
            //4. z People wybrać osoby w wieku 50+ i wybrać ich nazwisko małymi literami
            var result11 = people.Where(x => x.BirthDate.Value.AddYears(50) < DateTime.Now).Select(x => x.LastName.ToLower()).ToList();
            //5. wybrać pojedynczą osobę z imieniem dłuższym niż 3 znaki
            var result12 = people.Where(x => x.FirstName.Length > 3).First();

        }
    }
}
