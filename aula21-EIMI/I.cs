using System;

interface I { void Foo(); }
interface I1 { void Bar(); }
interface I2 { void Bar(); }

class A : I, I1, I2 {
    public virtual void Foo() {
        Console.WriteLine("A::Foo");
    }
    void I1.Bar() {
        Console.WriteLine("A::I1::Bar");
    }
    void I2.Bar() {
        Console.WriteLine("A::I2::Bar");
    }
}

class B : A {
    public override void Foo() {
        Console.WriteLine("B::Foo");
    }
}

class App {
    static void Main() {
        A a = new A();
        a.Foo();
        
        // I1 i1 = a;
        // i1.Bar();
        ((I1) a).Bar();

        I2 i2 = a;
        i2.Bar();
        
        a.Bar(); // TPC !!!!
        
    }
}