using System;
using System.Reflection;


public class Logger {

    /*
     * Escreve na consola o estado do objecto obj: o valor dos seus campos de instancia
     */
    public static void Log(object obj) {
        Type klass = obj.GetType();
        FieldInfo[] fs = klass.GetFields(
            BindingFlags.Instance | 
            BindingFlags.Public);
        
        Console.Write(klass.Name + ": ");
        
        foreach(FieldInfo f in fs) {
            Console.Write(f.Name + ": " + f.GetValue(obj) + ", ");
        }
        
        PropertyInfo[] ps = klass.GetProperties();
        foreach(PropertyInfo p in ps) {
            Console.Write(p.Name + ": " + p.GetValue(obj) + ", ");
        }
        
        Console.WriteLine();
    }
}

class App {
    static void Main() {
        School isel = new School("Lisbon", "ISEL");
        Student st1 = new Student(76145, "Ze Manel", isel);
        Student st2 = new Student(89755, "Maria Carica", isel);
        
        Logger.Log(isel);
        Logger.Log(st1);
        Logger.Log(st2);
        
        Course c = new Course("LEIC", new Student[]{st1, st2});
        Logger.Log(c);
    }
}

public class Student
{
    public readonly int nr;
    string name;
    public readonly School sch;
    public Student(int nr, string name, School sch) {
        this.nr = nr;
        this.Name = name; 
        this.sch = sch;
    }
    
    public String Name{
        get{return name;}
        set{
            if(value == null) throw new ArgumentException();
            name = value;
        }
    }
    
    
}
public class School
{
    public readonly string location;
    string name;
    public School(string location, string name) {
        this.location = location;
        this.Name = name;
    }
    public String Name{
        get{return name;}
        set{
            if(value == null) throw new ArgumentException();
            name = value;
        }
    }
}

public class Course
{
    public readonly Student[] stds;
    public readonly string id;
    
    public Course(string id, Student[] stds) {
        this.stds = stds; 
        this.id = id;
    }
    
    public String GetStudentsNrs() {
        String res = "";
        foreach(Student s in stds){
            res += s.nr;
        }
        return res;
    }
}