using System;



class A<T> { // O scope do T = scope da definição da classe
    public void Foo(T val) {Console.WriteLine("my val is " + val);}
}

class B<T> {
    
    public void Bar<S>(S val) { // O scope de S = scope do método Bar
        Console.WriteLine("my val is " + val);
    }
    public void Foo<S, I>(T val, S x) {
        Console.WriteLine("my val is " + val + " and x is " + x);
    }
}

class App {
    static void Main() {
        A<int> a = new A<int>();
        a.Foo(5); // So aceita inteiros como parametro
        
        B<string> b = new B<string>();
        b.Bar<int>(7);// So aceita inteiros como parametro
        b.Foo<int, bool>("ola", 91);
        
        /**
         * Passamos 2 argumentos = 1 arg de tipo + 1 arg actual
         * Passamos 2 argumentos = string + "ola"
         * O argumento de tipo é INFERIDO como string
         */
        b.Bar("ola");
    }
}

