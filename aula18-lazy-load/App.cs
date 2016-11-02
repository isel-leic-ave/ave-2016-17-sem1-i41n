using System;

public class A {
    public static void Test() {
        B.Bar();
    }

    public static void Foo() {
        Console.WriteLine("Foo.....");
    }
}

class App {    
    static void Main() {
        A.Foo();
        A.Foo();
    }
}

