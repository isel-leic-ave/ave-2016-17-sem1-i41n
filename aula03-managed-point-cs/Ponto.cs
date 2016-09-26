using System;

class A{
    public class X{}
}

class B{
    public B(){ new A.X();}
}


public class Ponto {
    public int x, y, z;

    public Ponto(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public double getModule() {
        return Math.Sqrt(x*x + y*y);
    }

    public void print(){
        Console.WriteLine(
            String.Format("Print 3 super (x = {0}, y = {1})", x, y));
    }
}