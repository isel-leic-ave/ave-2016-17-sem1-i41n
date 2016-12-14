using System;

class Program {

  static void println(String msg) { Console.WriteLine(msg); }
  
  static void Main(string[] args) {
    /**
    * Action<string>  -- (string) -> void
    * println         -- (string) -> void
    */
    Action<string> h1 = Program.println;
    Action<string> h2 = Program.println;
   /**
    * Action<string> h1h2 = (Action<string>) Delegate.Combine(h1, h2);
    * <=>
    */
    Action<string> h1h2 = h1; // v2 = v0
    h1h2 += h2;               // v2 = Combine(v2, v1)
    h1h2("Ola Mundo!!!");

    Console.WriteLine("---------------------------------------");
    h1h2 += str => Console.WriteLine("My argument is = " + str);
    h1h2("Ola Mundo!!!");
    
    Console.WriteLine("---------------------------------------");
   /**
    * Remove a primeira ocorrencia de h1 em h1h2
    * Action<string> h1h2 = (Action<string>) Delegate.Remove(h1h2, h1);
    * <=>
    */
    h1h2 -= h1;
    h1h2("Ola Mundo!!!");

    Console.WriteLine("---------------------------------------");
    h1h2 -= h2;
    h1h2("Ola Mundo!!!");
    
  }
}
