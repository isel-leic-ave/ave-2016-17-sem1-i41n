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
        return new EnumerableConverter<T, R>(src, transf);
    }  
 
    static IEnumerable Filter<T>(IEnumerable src, Predicate<T> p) {
        return new EnumerableFilter<T>(src, p);
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
                
                    Filter<Student>(
                        Filter<Student>(
                            Convert<object, Student>(
                                Lines("i41n.txt"),
                                Student.Parse), // Method Reference 
                            s => ((Student) s).nr > 38000), 
                        s => ((Student) s).name.StartsWith("A"))
                    // s => ((Student) s).name.Split(' ')[0]
                
            );
        
        foreach(object l in names)
            Console.WriteLine(l);        
    }
}

class EnumerableConverter<T, R> : IEnumerable<R>{
    readonly IEnumerable<T> src;
    readonly Func<T, R> transf;
    public EnumerableConverter(IEnumerable<T> src, Func<T, R> transf) {
        this.src = src; this.transf = transf;
    }
    public IEnumerator<R> GetEnumerator() {
        return new EnumeratorConverter<T, R>(src.GetEnumerator(), transf);
    }
    IEnumerator IEnumerable.GetEnumerator() {
        return new EnumeratorConverter<T, R>(src.GetEnumerator(), transf);
    }
}

class EnumeratorConverter<T, R> : IEnumerator<R>{ // implicito : IEnumerable
    readonly IEnumerator<T> src;
    readonly Func<T, R> transf;
    public EnumeratorConverter(IEnumerator<T> src, Func<T, R> transf) {
        this.src = src; this.transf = transf;
    }
    
    public bool MoveNext() {
        return src.MoveNext();
    }
    
    public R Current {
        get{ return transf(src.Current); }
    }
    
    object IEnumerator.Current {
        get {return this.Current; }
    }
    
    public void Reset() { 
        src.Reset();
    }
    
    public void Dispose() {}
}

class EnumerableFilter<T> : IEnumerable{
    readonly IEnumerable src;
    readonly Predicate<T> p;
    public EnumerableFilter(IEnumerable src, Predicate<T> p) {
        this.src = src; this.p = p;
    }
    public IEnumerator GetEnumerator() {
        return new EnumeratorFilter<T>(src.GetEnumerator(), p);
    }
}

class EnumeratorFilter<T> : IEnumerator{
    readonly IEnumerator src;
    readonly Predicate<T> p;
    object curr;
    public EnumeratorFilter(IEnumerator src, Predicate<T> p) {
        this.src = src; this.p = p;
    }
    
    public bool MoveNext() {
        while (src.MoveNext()) {
            curr = src.Current;
            if(p((T) curr))
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

