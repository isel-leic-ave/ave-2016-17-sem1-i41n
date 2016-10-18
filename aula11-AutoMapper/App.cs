using System;

class App {
    static void Main() {
        Mapper<Student, Person> mapper = AutoMapper
            .Build<Student, Person>()
            .Match("School", "Org");
            // .To("Org", "IBM");
            
        School isel = new School("Isel", "Lisboa");
        Student s = new Student { Nr = 27721, Name = "Ze Manel", School = isel};
        Student s2 = new Student { Nr = 83486, Name = "Maria Papoila", School = isel};
        Person p = mapper.Map(s);
        Person p2 = mapper.Map(s2);
        Console.WriteLine(p);
        Console.WriteLine(p2);
    }
}


public class Company
{
    public Company()
    {
    }

    public Company(string name)
    {
        this.Name = name;
    }
    public string Name { get; set; }
    public override string ToString() {
        return Name;
    }
}

class Person
{
    public string Name{ get; set; }
    public int Id { get; set; }
    public Company Org { get; set; }
    public override string ToString() {
        return String.Format("{0}, {1}, {2}", Id, Name, Org);
    }
}

 public class Student
{
    int nr;
    string name;
    School school;


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

    public School School {
        get { return school;  }
        set { school = value; }
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
