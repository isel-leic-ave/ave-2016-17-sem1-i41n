using System;

class Point{
    int x, y;
    
    public Point(int x, int y) {
        this.X = x;
        this.Y = y;
    }
    
    public int X{
        get{return x;}
        set{
            if(value < 0) throw new ArgumentException("Must be a non negative value");
            x = value;
        }
    }
    public int Y{
        get{return y;}
        set{
            if(value < 0) throw new ArgumentException("Must be a non negative value");
            y = value;
        }
    }
    public double Modulo{ // Propriedade RO = Sem set
        get{ return Math.Sqrt(X*X + Y*Y); }
    }
}

class App {
    static void Main() {
        // Point p = new Point(5, -7); // => Lança excepção
        Point p = new Point(11, 9);
        double res = p.Modulo;
        
    }
}
