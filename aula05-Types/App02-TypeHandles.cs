using System;

class Point{
    int x, y;
    public Point(int x, int y) {
        this.x = x;
        this.y = y;
    }
    
    public override String ToString() {
        return String.Format("[{0}, {1}]", x, y);
    }
    
    public override bool Equals(Object other) {
        if(Object.ReferenceEquals(this, other)) return true;
        Point p2 = (Point) other;
        return this.x == p2.x && this.y == p2.y;
    }
}

class B{}

class App {
    static void Inspect(Object o){
        Type t = o.GetType();
        Console.WriteLine(o.ToString() + " is instance of: " + t.ToString());
    }
    
    static void SameType(Object o1, Object o2) {
        // if(o1.GetType().Equals(o2.GetType())) // Comparar Igualdade = Estado dos Objectos
        // if(o1.GetType() == o2.GetType())      // Comparar Igualdade 
        if(Object.ReferenceEquals(o1.GetType(), o2.GetType()))
            Console.WriteLine("Same type");
        else 
            Console.WriteLine("Not of same type");
    }
    
    static void IsTypeOf(Object o, Type t){
        if(Object.ReferenceEquals(o.GetType(), t))
            Console.WriteLine(o  + " is instance of " + t);
        else
            Console.WriteLine(o  + " is NOT instance of " + t);
    }

    static void TestInspect(){
        Point p1 = new Point(11, 17);
        Point p2 = new Point(23, 31);
        Point p3 = new Point(11, 17); // p1 e p3 são iguais => Têm o mesmo ESTADO.
        
        Inspect(p1);
        Inspect(p2);
        SameType(p1, p2);
        SameType(p1, new B());
        SameType(new B(), new B());
    }
    
    static void TestIsTypeOf(){
        Point p1 = new Point(11, 17);
        Point p2 = new Point(23, 31);
        Point p3 = new Point(11, 17); // p1 e p3 são iguais => Têm o mesmo ESTADO.
        
        IsTypeOf(p1, typeof(Point));
        IsTypeOf(p2, typeof(Point));
        IsTypeOf(new B(), typeof(Point));
    }
    
    static void Main() {
        TestInspect();
        TestIsTypeOf();
    }
}
