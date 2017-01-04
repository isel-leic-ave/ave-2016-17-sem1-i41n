using System;

class MyGCCollectClass
{
    static void PrintGeneration(object obj){
        Console.WriteLine("Object in generation " + GC.GetGeneration(obj));
    }

    static void PrintRunningGC(){
        Console.WriteLine("Running GC for all generations...");
        GC.Collect();
    }

    static void PrintRunningGC(int n){
        Console.WriteLine("Running GC for generation " + n);
        GC.Collect(n);
    }

    static void Main()
    {
        object [] data = new object[1];
    
        Console.WriteLine("Total Memory: {0}", GC.GetTotalMemory(false));

        // Determine the maximum number of generations the system 
        // garbage collector currently supports.
        Console.WriteLine("The highest generation is {0}", GC.MaxGeneration);
        
        PrintGeneration(data); // -> gen 0
        PrintRunningGC();
        PrintGeneration(data); // -> gen 1
        PrintRunningGC();
        PrintGeneration(data); // -> gen 2
        PrintRunningGC();
        PrintGeneration(data); // -> gen 2
        
        Console.ReadLine();
        Console.WriteLine("Total Memory: {0}", GC.GetTotalMemory(false));
        data = MakeSomeGarbage(); // Compilar com: csc /debug- /optimize+
        PrintGeneration(data); // -> gen 0 
        Console.WriteLine("Total Memory: {0}", GC.GetTotalMemory(false));
        PrintRunningGC();
        Console.WriteLine("Total Memory: {0}", GC.GetTotalMemory(false));
        // Console.WriteLine(data);
        
        /*
        PrintRunningGC();
        PrintGeneration(data); // -> gen 1
        PrintRunningGC();
        PrintGeneration(data); // -> gen 2
        Console.WriteLine("Total Memory: {0}", GC.GetTotalMemory(false));
        
        data = new object[1];
        PrintRunningGC(0);
        Console.WriteLine("Total Memory: {0}", GC.GetTotalMemory(false));
        PrintRunningGC(1);
        Console.WriteLine("Total Memory: {0}", GC.GetTotalMemory(false));
        PrintRunningGC(2);
        Console.WriteLine("Total Memory: {0}", GC.GetTotalMemory(false));
        */
    }

    private const long maxGarbage = 4096;        
    static object[] MakeSomeGarbage()
    {
        Console.WriteLine("..... Making garbage...");
        object[] vts = new object[maxGarbage];

        for(int i = 0; i < maxGarbage; i++)
        {
            vts[i] = new object();
        }
        return vts;
    }
}