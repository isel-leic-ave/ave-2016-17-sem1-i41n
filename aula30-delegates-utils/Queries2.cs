using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class Student
{
    public readonly int nr;
    public readonly string name;
    public readonly int group;
    public readonly string githubId;

    public Student(int nr, String name, int group, string githubId)
    {
        this.nr = nr;
        this.name = name;
        this.group = group;
        this.githubId = githubId;
    }

    public override String ToString()
    {
        return String.Format("{0} {1} ({2}, {3})", nr, name, group, githubId);
    }
    public void Print()
    {
        Console.WriteLine(this.ToString());
    }
    
    public static Student Parse(object src){
        string [] words = src.ToString().Split('|');
        return new Student(
            int.Parse(words[0]),
            words[1],
            int.Parse(words[2]),
            words[3]);
    }
}

static class App
{
    
    static List<String> Lines(string path)
    {
        string line;
        List<string> res = new List<string>();
        using (StreamReader file = new StreamReader(path)) // <=> try-with resources do Java >= 7
        {
            while ((line = file.ReadLine()) != null)
            {
                res.Add(line);
            }
        }
        return res;
    }
    
    delegate object Mapping(object data);
    
    delegate bool Predicate(object data);
    
    static IList Convert(IList src, Mapping transf) {
        IList res = new ArrayList();
        foreach(object item in src) {
            res.Add(transf(item));
        }
        return res;
    }
    
    static IList Filter(IList src, Predicate p) {
        IList res = new ArrayList();
        foreach(object item in src) {
            if(p(item))
                res.Add(item);
        }
        return res;
    }
    
    static void Main()
    {
        IList names = 
            Convert(
                Filter(
                    Filter(
                        Convert(
                            Lines("i41n.txt"),
                            line => Student.Parse(line)),
                        s => ((Student) s).nr > 38000), 
                    s => ((Student) s).name.StartsWith("A")),
                s => ((Student) s).name
            );
        
        foreach(object l in names)
            Console.WriteLine(l);
    }
}