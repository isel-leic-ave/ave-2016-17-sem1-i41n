using System;

class Container {
    public object Target{  get; set; }
    public Container(object target){
        this.Target = target;
    }
}

class HugeType {
    private object[] values;
    public HugeType() {
        values = new object[1024*1024];
        for(int i = 0; i < values.Length; i++)
            values[i] = new object();
    }
}

public class App
{
    static void Main() {
        Container cont1 = new Container(new HugeType());
        Console.WriteLine("Container 1 target = " + cont1.Target);
        
        WeakReference<HugeType> cont2 = new WeakReference<HugeType>(new HugeType());
        HugeType target;
        cont2.TryGetTarget(out target); // val <- WeakReference.Value
        Console.WriteLine("Container 2 target = " + target);
        
        GC.Collect();
        Console.WriteLine("Container 1 target = " + cont1.Target);
        HugeType target2;
        Console.WriteLine(cont2.TryGetTarget(out target2)); // val <- WeakReference.Value
        Console.WriteLine("Container 2 target = " + target2);
        
        
    }
}