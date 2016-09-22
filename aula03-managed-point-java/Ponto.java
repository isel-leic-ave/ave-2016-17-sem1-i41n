import java.lang.Math;

public class Ponto {
    public int x, y, z;

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