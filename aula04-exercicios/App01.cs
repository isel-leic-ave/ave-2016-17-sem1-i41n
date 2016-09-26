using System;

class App {
    static void Main() {
        Console.WriteLine(gdc(6, 9));
    }
    static double gdc(int m, int n) { // Maximo Divisor Comum
        if(m == n) return n;
        return m < n ? gdc(m, n-m): gdc(m-n, n);
    }
}