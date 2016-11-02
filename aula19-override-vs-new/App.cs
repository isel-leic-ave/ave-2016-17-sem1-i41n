using System;

public class A {
    public virtual void Foo() {
        Console.WriteLine("Foo on AAAAA");
    }
    public void Bar() {
        Console.WriteLine("Bar on AAAAA");
    }
}

public class B : A {
    public override void Foo() {
        Console.WriteLine("Foo on Bbbbbbb");
    }
    public new void Bar() {
        Console.WriteLine("Bar on Bbbbbbb");
    }
}


class App {    
    static void Main() {
        A a = new B();
        // Metodo de instancia => callvirt => verificar se o target != null
        a.Foo(); // callvirt => D.D. => ao objecto         => B::Foo
        a.Bar(); // callvirt => D.E. => tipo da variavel a => A::Bar 
        ((B) a).Bar(); // // callvirt => D.E. => tipo da variavel => B::Bar 
    }
}

