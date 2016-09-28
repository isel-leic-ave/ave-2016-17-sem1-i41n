using System;

interface A{} // Tipo Referencia
class B{}     // Tipo Referencia
struct S{}    // Tipo Valor

class App {
    static void Main() {
        int a = 7;      // a é de tipo valor -- tipo primitivo
        Int32 b = 12;
        S s;            // s é de tipo valor MAS NÃO primitivo
        String msg = "Ola"; // String é a classe System.String -- Tipo Referencia
        string msg2 = "Ola"; // => Ao nivel do IL gera o mesmo que a instrução acima
    }
}
