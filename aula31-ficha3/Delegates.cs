delegate void SomeHandler();	

class Program {
  static void println() { }
  static void Test(SomeHandler h) { }
  static void Main(string[] args) {
      Test(Program.println);
  }
}
