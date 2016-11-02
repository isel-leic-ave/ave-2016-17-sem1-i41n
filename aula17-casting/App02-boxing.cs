using System;
using System.Reflection;


class Logger {
    public static void Log(object o) {
        IPrinter p = o as IPrinter; // isinst
        if(p != null) {
            p.Print();
            return;
        }
        // Inspeccionar o estado do objecto o via Reflexão
        Console.WriteLine("inspecting....");
    }
}
interface IPrinter { void Print(); }

class A : IPrinter{
    public void Print() {
        Console.WriteLine("I am a dummy A!!!!!");
    }
}

class B {}

struct Point : IPrinter {
    int x, y; 
    public Point(int x, int y) {
        this.x = x;
        this.y = y;
    }
    public void Print() {
        Console.WriteLine("[{0}, {1}]", this.x, this.y);
    }
}

class App {
    
    static void Main() {
        int n = 5;
        object o = n;   // box
        
        Point p = new Point();
        o = p; // box
        IPrinter pr = p; // box
        
        Console.WriteLine(p); // box
        
        Point p2 = (Point) pr; // unbox
    }

}

