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
        using (StreamReader file = new StreamReader(path, Encoding.UTF8)) // <=> try-with resources do Java >= 7
        {
            while ((line = file.ReadLine()) != null)
            {
                res.Add(line);
            }
        }
        return res;
    }
    
    static IEnumerable<R> Convert<T, R>(IEnumerable<T> src, Func<T,R> transf) {
        foreach(T item in src) {
            yield return transf(item);
        }

    }  
 
    static IEnumerable<T> Filter<T>(IEnumerable<T> src, Predicate<T> p) {
        foreach(T item in src) {
            if(p(item))
                yield return item;
        }
    }  

    static IEnumerable<T> Distinct<T>(IEnumerable<T> src) {
        IList selected = new ArrayList();
        foreach(T item in src) {
            if(!selected.Contains(item)) {
                selected.Add(item);
                yield return item;
            }
        }
    }  
    
    static void Print(string label) {
        Console.WriteLine(label);
    }
    static void Main()
    {
        IEnumerable names = 
            Distinct(
                Convert(
                    Filter(
                        Filter(
                            Convert(
                                Lines("i41n.txt"),
                                line => { return Student.Parse(line); }) ,
                            s => {  return s.nr > 38000; }), 
                        s => {  return s.name.StartsWith("A"); }),
                    s => { return s.name.Split(' ')[0]; }
                )
            );
        
        foreach(object l in names)
            Console.WriteLine(l);
    }
}
