using System;
using System.Linq;
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
        using (StreamReader file = new StreamReader(path, Encoding.UTF8)) // <=> try-with resources do Java >= 7
        {
            while ((line = file.ReadLine()) != null)
            {
                res.Add(line);
            }
        }
        return res;
    }
    
    
    static void Print(string label) {
        Console.WriteLine(label);
    }
    static void Main()
    {
        IEnumerable names = Lines("i41n.txt")
            .Select(Student.Parse)
            .Where(s => {  return s.nr > 38000; })
            .Where(s => {  return s.name.StartsWith("A"); })
            .Select( s => { return s.name.Split(' ')[0]; } )
            .Distinct();
        
        foreach(object l in names)
            Console.WriteLine(l);
    }
}
