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
        base.Foo(); // call !!!! => Há a garantia de que o this != null
    }
}


class App {    
    static void Main() {
        A a = new B();
        a.Foo(); // callvirt pq Foo é de instância => Verificar target != null
        a.Bar(); // callvirt pq Bar é de instância => Verificar target != null
    }
}

