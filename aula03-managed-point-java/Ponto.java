import java.lang.Math;

class A{
    static class X{}
}

class B{}


public class Ponto {
    public int w, x, y, z;

    public Ponto(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public double getModule() {
        return Math.sqrt((double)x*x + y*y);
    }

    public void print(){
        System.out.println(
            String.format("Print 3 super (x = %d, y = %d)", x, y));
    }
}