using System;
using System.Reflection;


class Logger {
    public static void Log(object o) {
        // if(typeof(IPrinter).IsAssignableFrom(o.GetType())) {
        
        /*
        if(o is IPrinter) { // isinst 
            ((IPrinter) o).Print(); // castclass
            return; 
        }
        */
        
        IPrinter p = o as IPrinter; // isinst
        if(p != null) {
            p.Print();
            return;
        }
        // Inspeccionar o estado do objecto o via Reflexão
        Console.WriteLine("inspecting....");
    }
}
interface IPrinter { void Print(); }

class A : IPrinter{
    public void Print() {
        Console.WriteLine("I am a dummy A!!!!!");
    }
}

class B {}

class App {
    
    static void Main() {
        Logger.Log(new A());
        Logger.Log(new B());
    }
    
    static void TestCastingAndCoercion() {
        long nr = 1000000; // loc.0
        short a = (short) nr; // ldloc.0 + conv.i2 + stloc.1
        int b = a;            // ldloc.1 + stloc.2
        
        A a1 = new A(); // loc.3
        Object o = a1;  // v_4
        A a2 = (A) o;   // ldloc v4 + castclass A + stloc v5
    }
}

