
namespace Inheritance.Models
{
    //każda klasa dziedziczy niejawnie po klasie Object
    // : - oznacza dziedziczenie po wskazanej klasie
    //klasa abstrakcyjna to klasa, której instancji nie możemy utworzyć  (mimo posiadania publicznego konstruktora) - służy ona jako baza dla innych klas
    internal abstract class Shape /*: Object*/
    {
        //protected - modyfikator dostępu pozwalający korzystać typom pochodnym ale na zewnątrz klasy działa jak private
        protected string Name { get; set; }

        public Shape(string name)
        {
            Name = name;
        }

        //virtual - pozwala zmienić implementację metody w klasach pochodnych (używając override)
        public virtual string GetName()
        {
            return Name;
        }

        public override string ToString()
        {
            return GetName();
        }
    }
}
