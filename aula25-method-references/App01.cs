// using System;

delegate void Action();
delegate T Func<T>();

class App
{
    static void Foo() { System.Console.WriteLine("I am Foo"); }
    static object Bar() { System.Console.WriteLine("I am Bar"); return "Bar";}

    static void Main()
    {
        // Action tem o descritor: () -> void
        // Action h1 = App.Foo;
        /*
         * ldnull
         * ldftn  App::Foo
         * newobj Action::ctor(object, int)
         */
        Action h1 = new Action(App.Foo); 
        
        //Func tem o descritor: () -> object
        Func<object> h2 = App.Bar;
        
        // Erro de compilação porque Foo não é compatível com Func<object>
        // Foo é um método sem parametros que NÃO retorna object.
        // Func<object> h3 = App.Foo;
        
        Foo(); // IL: call App::Foo ---> AMD64: call 838468136
        h1.Invoke(); // h1(); IL: callvirt Action::Invoke -----> AMD64: 
        Bar();
        h2.Invoke(); // h2();
    }
}