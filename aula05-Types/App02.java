
class Point{
    int x, y;
    public Point(int x, int y) {
        this.x = x;
        this.y = y;
    }
    
	
    public String ToString() {
        return x + "" + y;
    }
    
    public boolean Equals(Object other) {
        if(this==other)return true;
        Point p2 = (Point) other;
        return this.x == p2.x && this.y == p2.y;
    }
}

class B{}

class App {
    static void Inspect(Object o){
        Class t = o.getClass();
        System.out.println(o.getClass().getName()+ " is instance of: " + t);
    }
    
    static void SameType(Object o1, Object o2) {
        // if(o1.GetType().Equals(o2.GetType())) // Comparar Igualdade = Estado dos Objectos
        // if(o1.GetType() == o2.GetType())      // Comparar Igualdade 
       
		
	   if(o1.getClass()==o2.getClass())
             System.out.println("Same type");
        else 
             System.out.println("Not of same type");
    }
    
    static void IsTypeOf(Object o, Class t){
        if(o.getClass()==t)
            System.out.println(o.getClass().getName()  + " is instance of " + t);
        else
            System.out.println(o.getClass().getName()  + " is NOT instance of " + t);
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
        
        IsTypeOf(p1, Point.class);
        IsTypeOf(p2, Point.class);
        IsTypeOf(new B(), Point.class);
    }
    
    public static void main(String []args) {
        TestInspect();
        TestIsTypeOf();
    }
}
