using System;

class App {
    static void Main() {
        Mapper<Student, Person> mapper = AutoMapper
            .Build<Student, Person>()
            // .IgnoreMember("Nr")            
            // .IgnoreMember("Name");
            .To("Org", "IBM");
        Student s = new Student { Nr = 27721, Name = "Ze Manel", School = "Isel"};
        Person p = mapper.Map(s);
        Console.WriteLine(p);
    }
}


class Person
{
    public string Name{ get; set; }
    public int Nr { get; set; }
    public string Org { get; set; }
    public override string ToString() {
        return String.Format("{0}, {1}, {2}", Nr, Name, Org);
    }
}


 public class Student
{
    int nr;
    string name;
    string school;

    public int Nr {
        get{
            return nr;
        }
        set {
            nr = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }

    public string School {
        get { return school;  }
        set { school = value; }
    }
}