using System;

class App {
    static void Main() {
        School isel = new School("Isel", "Lisboa");
        Console.WriteLine(typeof(School).IsSubclassOf(typeof(Company)));
        Console.WriteLine(typeof(School).IsSubclassOf(typeof(Organization)));
        Console.WriteLine(typeof(Organization).IsAssignableFrom(typeof(School)));
        Person p = new Person() {Org = isel};
        Console.WriteLine(p.Org);
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

public class School : Company
{
    public School(string name, string loc)
    {
        this.Name = name;
        this.Location = loc;
    }
    public string Location{ get; set; }
}

public class Company :Organization
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

interface Organization{}