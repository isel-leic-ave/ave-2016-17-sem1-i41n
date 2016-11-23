// import java.util.function.*;

interface Supplier<T> { T get(); }

public class App01
{
    static void Foo() { System.out.println("I am Foo"); }
    static Object Bar() { System.out.println("I am Bar"); return "Bar";}

    public static void main(String [] args)
    {
        // Action tem o descritor: () -> void
        Runnable h1 = App01::Foo;
        
        //Func tem o descritor: () -> Object
        Supplier<Object> h2 = App01::Bar;
        
        // Erro de compilação porque Foo não é compatível com Func<Object>
        // Foo é um método sem parametros que NÃO retorna Object.
        // Supplier<Object> h3 = App01::Foo;
        
        Foo();
        h1.run();
        Bar();
        h2.get();
    }
}