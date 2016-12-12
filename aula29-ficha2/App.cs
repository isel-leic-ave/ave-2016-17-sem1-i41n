struct A : I {}
interface I {}

class App {
    static void Main() {
        A a = new A();
        I i = a; // box
    }
}