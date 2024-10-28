using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models
{
    internal class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }

        public int GetAge()
        {
            if(BirthDate == null)
                return 0;

            int age = DateTime.Now.Year - BirthDate.Value.Year;
            return age;
        }

        public string Bio()
        {
            return $"Imię: {FirstName}; Nazwisko: {LastName}; Wiek: {GetAge()}";
        }

    }
}
