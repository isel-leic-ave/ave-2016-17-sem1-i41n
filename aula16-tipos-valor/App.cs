using System;
using System.Reflection;

class X {
    public int a, b, c;
    public void Print() {
        Console.WriteLine("X: {0}, {1}, {2}", a, b, c);
    }
}

struct A{
    public int a, b, c;
    public void Print() {
        Console.WriteLine("A: {0}, {1}, {2}", a, b, c);
    }
}

struct B {
    int a, b, c; 
    
    public B(int n) {
        a = n;
        b = 0;
        c = 0;
    }
}

class App {
    static void UpdateX(X x) {x.b = 7; }
    
    static void UpdateA(A a) {a.b = 7;/*Altera uma cópia e não o original*/ }
    
    static void UpdateAByRef(ref A a) {a.b = 7;/*Altera uma cópia e não o original*/ }
    
    static void Main() {
        X x = new X();   // => newobj
        A a = new A();   // => initobj
        B b = new B(11); // => call B::ctor
        
        
        Console.WriteLine("-------------- Initial: ");
        x.Print();
        a.Print();
        Console.WriteLine("-------------- Update By Val: ");
        UpdateX(x); UpdateA(a);
        x.Print();
        a.Print();
        Console.WriteLine("-------------- Update By Ref: ");
        UpdateAByRef(ref a);
        a.Print();
    }
}

