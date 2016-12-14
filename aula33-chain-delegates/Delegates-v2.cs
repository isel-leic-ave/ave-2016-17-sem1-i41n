using System;

class Program {

  static Random rand = new Random();
  static int println(String msg) { return rand.Next(100); }
  
  static void CallAndPrint(Func<string, int> handlers, string msg) {
    Delegate[] hs = handlers.GetInvocationList();
    string str = "";
    foreach(Delegate h in hs){
        Func<string, int> f = (Func<string, int>) h;
        str += f(msg) + " ";
    }
    Console.WriteLine(str);
  }
  
  static void Main(string[] args) {
    Func<string, int> h1 = Program.println;
    Func<string, int> h2 = Program.println;
    Func<string, int> h1h2 = h1; // v2 = v0
    h1h2 += h2;                  // v2 = Combine(v2, v1)
    CallAndPrint(h1h2, "Ola Mundo!!!");

    Console.WriteLine("---------------------------------------");
    h1h2 += str => str.Length;
    CallAndPrint(h1h2, "Ola Mundo!!!");
    
    Console.WriteLine("---------------------------------------");
   /**
    * Remove a primeira ocorrencia de h1 em h1h2
    * Action<string> h1h2 = (Action<string>) Delegate.Remove(h1h2, h1);
    * <=>
    */
    h1h2 -= h1;
    CallAndPrint(h1h2, "Ola Mundo!!!");

    Console.WriteLine("---------------------------------------");
    h1h2 -= h2;
    CallAndPrint(h1h2, "Ola Mundo!!!");
  }
}
