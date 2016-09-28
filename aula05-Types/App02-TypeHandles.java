class A{}
class B{}

public class App02TypeHandles {
    public static void main(String [] args) {
        A a = new A();
        A c = new A();
        Class ta = a.getClass();
        Class tc = c.getClass();
        System.out.println("ta == tc --> " +  (ta == tc)); // Cmp Identidade --> Cmp Referencias
        
        Class tb = new B().getClass();
        System.out.println("ta == tb --> " +  (ta == tb)); // Cmp Identidade --> Cmp Referencias
        
        Class klassB = B.class;
        System.out.println("klassB == tb --> " +  (klassB == tb)); // Cmp Identidade --> Cmp Referencias
        
        int n = 5;
        Object o = n;
        Class klassInt = o.getClass(); 
        System.out.println(klassInt);
    }
}
