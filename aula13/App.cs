using System;

public class App
{
    static void Main()
    {
    }
    
    public static void AreEqual(object v1, object v2) {
    }
    
    public void TestStudent()
    {
        Mapper<Student, Person> m = AutoMapper
            .Build<Student, Person>()
            .Match("Nr", "Id");
        var s = new Student { Nr = 27721, Name = "Ze Manel", Org = new School("ISEL", "Lisbon")};
        Person p = m.Map(s);
        AreEqual(s.Name, p.Name);
        AreEqual(s.Nr, p.Id);
        AreEqual(s.Org.Name, p.Org.Name);
    }

    public void TestIgnorePropertyName()
    {
        Mapper<Student, Person> m = AutoMapper
            .Build<Student, Person>()
            .IgnoreMember("Name");
            
    }
}

public class Organization
{
    public Organization()
    {

    }

    public Organization(string name)
    {
        this.Name = name;
    }
    public string Name { get; set; }
}

class Person
{
    public string Name{ get; set; }
    public int Id { get; set; }
    public Organization Org { get; set; }
}

 public class Student
{
    int nr;
    string name;
    School org;


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

    public School Org {
        get { return org;  }
        set { org = value; }
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        Student other = obj as Student;
        if (other == null) return false;
        return nr == other.nr && name.Equals(other.name);
    }
}

public class School
{
    public School(string name, string loc)
    {
        this.Name = name;
        this.Location = loc;
    }

    public string Name{ get; set; }
    public string Location{ get; set; }
}