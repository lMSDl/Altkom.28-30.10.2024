using Inheritance.Models;

/*Shape shape = new Shape("X");

Shape1D shape1D = new Shape1D("linia", 10);

Shape2D shape2D = new Shape2D("prostokąt", 10, 5);

shape1D.GetName();*/

Circle circle = new Circle(4);
Rectangle rectangle = new Rectangle(3, 6);
Line line = new Line(3);

Shape shape = circle;
Console.WriteLine(  shape.GetName() );

shape = rectangle;
shape = line;

Shape1D shape1D = rectangle;
shape1D = line;
shape1D = circle;

Shape2D shape2D = rectangle;
shape2D = circle;
//shape2D = line; - nie możemy przypisać linii do zmiennej typu Shape2d, ponieważ linia nie dziedziczy po tym typie


List<Shape> shapes = new List<Shape>();
shapes.Add(line);
shapes.Add(rectangle);
shapes.Add(circle);

foreach (var item in shapes)
{
    Console.WriteLine(item.ToString());
    Console.WriteLine(item.GetName());

    //sprawdzamy czy item jest klasy Shape2D. Jeśli tak, to deklarujemy dla niego nazwę
    if (item is Shape2D shape2d)
        Console.WriteLine(  shape2d.CalculateArea() );
}