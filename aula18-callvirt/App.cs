using System;

public class A {
    
    public virtual void Print() {
        Console.WriteLine("I am a dummy A!!!!");
    }
    
    public void Test() {
        Console.WriteLine("Test.....A");
    }
    
    public static void Foo() {
        Console.WriteLine("Foo.....");
    }
}

class B : A{
    public override void Print() {
        Console.WriteLine("I am a B!!!!");
    }
    
    public void Test() {
        Console.WriteLine("Test.....B");
    }

}

class App {    
    static void Main() {
        A a = new B();
        a.Print(); // callvirt  => Desp dinamico
        a.Test();  // callvirt  => Desp estático 
        A.Foo();   // call      => Desp estático 
    }
}

