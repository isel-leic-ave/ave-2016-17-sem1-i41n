
class A {
    public void foo() {
        System.out.println("Foo on AAAAA");
    }
    public final void bar() {
        System.out.println("Bar on AAAAA");
    }
}

class B extends A {
    public void foo() {
        System.out.println("Foo on Bbbbbbb");
        super.foo(); // call !!!! => Há a garantia de que o this != null
    }
}


public class App {    
    public static void main(String [] args) {
        A a = new B();
        a.foo(); // callvirt pq Foo é de instância => Verificar target != null
        a.bar(); // callvirt pq Bar é de instância => Verificar target != null
    }
}

