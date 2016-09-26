using System;

public class PontoApp {

    public static void Main(String[] args)
    {
        Ponto p = new Ponto(5, 7);
        p.print();
        Console.WriteLine(
            String.Format("p._x = {0}\n", p.x));
    }

}