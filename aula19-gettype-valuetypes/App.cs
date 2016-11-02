using System;

class App {    
    static void Main() {
        object o = new object();
        int n = 596;
        Type t = n.GetType(); // box + call
        Console.WriteLine(t);
        
        Type t2 = o.GetType(); // callvirt
        Console.WriteLine(t2);
        
    }
}

