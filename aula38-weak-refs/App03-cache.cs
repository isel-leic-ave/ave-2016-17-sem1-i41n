using System;

class HugeTypeProvider {
    int maxSize;
    WeakReference<HugeType>[] values;
    public HugeTypeProvider(int max){
        this.maxSize = max;
        this.values = new WeakReference<HugeType>[max];
        // Eager initialization
        // !!!! Mesmo que não sejam usados está a consumir memória
        /*
        for(int i = 0; i < values.Length; i++)
            values[i] = new HugeType();
        */
    }
    public HugeType Get(){
        // !!!! Não controla recursos que já não estejam a ser usados.
        /*
        if(maxSize == 0) throw new IllegalOperation("No more resources available!")
        return values[--maxSize];
        */
        int idx = FindAvailableIndex();
        if(idx < 0) {
            Console.WriteLine("Running GC....");
            GC.Collect();
            idx = FindAvailableIndex();
            if(idx < 0) throw new InvalidOperationException("!!!No resources available!!");
        }
        HugeType target = new HugeType();
        values[idx] = new WeakReference<HugeType>(target);
        Console.WriteLine("Huge in index " + idx);
        return target;
    }
    /**
     * Retorna o índice da primeira posição do array a null
     * ou que tenha uma WeakReference com target a null.
     * Retorna < 0 se não existirem posições disponiveis.
     */
    public int FindAvailableIndex(){
        HugeType target;
        for(int i = 0; i < values.Length; i++) {
            if(values[i] == null || !values[i].TryGetTarget(out target))
                return i;
        }
        return -1;
    }
}
class HugeType {
    private object[] values;
    public HugeType() {
        values = new object[1024];
        for(int i = 0; i < values.Length; i++)
            values[i] = new object();
    }
}

public class App
{
    static void Main() {
        HugeTypeProvider prov = new HugeTypeProvider(3);
        object ref1 = prov.Get();
        object ref2 =prov.Get();
        object ref3 =prov.Get();
        prov.Get();
        ref1.GetHashCode();
        ref2.GetHashCode();
        // ref3.GetHashCode();
    }
}