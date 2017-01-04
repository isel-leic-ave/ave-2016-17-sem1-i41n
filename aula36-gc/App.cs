using System;

class App
{
    static void Main()
    {
        object root = new object();
        Console.WriteLine(GC.GetGeneration(root));
        GC.Collect();
        Console.WriteLine(GC.GetGeneration(root));
        GC.Collect();
        Console.WriteLine(GC.GetGeneration(root));
        GC.Collect();
        Console.WriteLine(GC.GetGeneration(root));
        GC.Collect();
        Console.WriteLine(GC.GetGeneration(root));
        Console.WriteLine(GC.GetTotalMemory(false));
        root = MakeSomeGarbage();
        Console.WriteLine(GC.GetTotalMemory(false));
        GC.Collect();
        Console.WriteLine(GC.GetTotalMemory(false));
        root.GetHashCode();  // prolongar o tempo de roots
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