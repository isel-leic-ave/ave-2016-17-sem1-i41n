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
    
    static IEnumerable Convert(IEnumerable src, Mapping transf) {
        return new EnumerableConverter(src, transf);
    }  
 
    static IEnumerable Filter(IEnumerable src, Predicate p) {
        return new EnumerableFilter(src, p);
    }  

    static IEnumerable Distinct(IEnumerable src) {
        return new EnumerableDistinct(src);
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
                                line => Student.Parse(line)),
                            s => ((Student) s).nr > 38000), 
                        s => ((Student) s).name.StartsWith("A")),
                    s => ((Student) s).name.Split(' ')[0]
                )
            );
        
        foreach(object l in names)
            Console.WriteLine(l);        
    }
}

delegate object Mapping(object item);

delegate bool Predicate(object data);

class EnumerableConverter : IEnumerable{
    readonly IEnumerable src;
    readonly Mapping transf;
    public EnumerableConverter(IEnumerable src, Mapping transf) {
        this.src = src; this.transf = transf;
    }
    public IEnumerator GetEnumerator() {
        return new EnumeratorConverter(src.GetEnumerator(), transf);
    }
}

class EnumeratorConverter : IEnumerator{
    readonly IEnumerator src;
    readonly Mapping transf;
    public EnumeratorConverter(IEnumerator src, Mapping transf) {
        this.src = src; this.transf = transf;
    }
    
    public bool MoveNext() {
        return src.MoveNext();
    }
    
    public object Current {
        get{ return transf(src.Current); }
    }
    
    public void Reset() { 
        src.Reset();
    }
}

class EnumerableFilter : IEnumerable{
    readonly IEnumerable src;
    readonly Predicate p;
    public EnumerableFilter(IEnumerable src, Predicate p) {
        this.src = src; this.p = p;
    }
    public IEnumerator GetEnumerator() {
        return new EnumeratorFilter(src.GetEnumerator(), p);
    }
}

class EnumeratorFilter : IEnumerator{
    readonly IEnumerator src;
    readonly Predicate p;
    object curr;
    public EnumeratorFilter(IEnumerator src, Predicate p) {
        this.src = src; this.p = p;
    }
    
    public bool MoveNext() {
        while (src.MoveNext()) {
            curr = src.Current;
            if(p(curr))
                return true;
            else
                curr = null;
        }
        return false;
    }
    
    public object Current {
        get{ return curr; }
    }
    
    public void Reset() { 
        src.Reset();
    }
}

class EnumerableDistinct : IEnumerable{
    readonly IEnumerable src;
    public EnumerableDistinct(IEnumerable src) {
        this.src = src; 
    }
    public IEnumerator GetEnumerator() {
        return new EnumeratorDistinct(src.GetEnumerator());
    }
}

class EnumeratorDistinct : IEnumerator{
    readonly IEnumerator src;
    readonly IList selected = new ArrayList();
    object curr;
    public EnumeratorDistinct(IEnumerator src) {
        this.src = src; 
    }
    
    public bool MoveNext() {
        while (src.MoveNext()) {
            curr = src.Current;
            if(!selected.Contains(curr)) {
                selected.Add(curr);
                return true;
            }
        }
        return false;
    }
    
    public object Current {
        get{ return curr; }
    }
    
    public void Reset() { 
        src.Reset();
    }
}

