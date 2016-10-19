using System;
using System.Reflection;

class NoteXAttribute : Attribute {
    public string Name{get; set;}
    public string Label{get; set;}
    public NoteXAttribute(string name) {
        this.Name = name;
    }
    public override String ToString() {
        return String.Format("NoteX({0, {1}})", Name, Label);
    }
    
}
class NoteYAttribute : Attribute {
    public int Size{get; set;}
    public NoteYAttribute() {}
    public NoteYAttribute(int size) {
        this.Size = size;
    }
    public override String ToString() {
        return String.Format("NoteY({0})", Size);
    }
}

[NoteX("Ola")]
class A {
    
    int x;
    [NoteY(7)]
    int y;
    
    [NoteX("ISEL", Label = "Super")]
    [NoteY]
    public void Foo() {
        Console.WriteLine("I am Foo");
    }
}

class App {
    static void Main() {
        ShowMembersAttributes(typeof(A));
    }
    
    static void ShowMembersAttributes(Type klass) {
        MemberInfo[] ms = klass.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance );
        foreach(MemberInfo m in ms) {
            Console.Write(m.Name + " -> ");
            object[] atts = m.GetCustomAttributes(true);
            foreach(object a in atts) {
                Console.Write(a.ToString() + ", ");
            }
            Console.WriteLine();
        }
    }
}

