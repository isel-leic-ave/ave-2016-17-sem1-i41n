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
 
    static IEnumerable<T> Filter<T>(IEnumerable<T> src, Predicate<T> p) {
        return new EnumerableFilter<T>(src, p);
    }  

    static IEnumerable<T> Distinct<T>(IEnumerable<T> src) {
        return new EnumerableDistinct<T>(src);
    }  
    
    static void Print(string label) {
        Console.WriteLine(label);
    }
    static void Main()
    {
        IEnumerable names = 
            Convert(
                Filter(
                    Filter(
                        Convert(
                            Lines("i41n.txt"),
                            line => { Console.WriteLine("Converting"); return Student.Parse(line); }) ,
                        s => { Console.WriteLine("Filtering"); return s.nr > 38000; }), 
                    s => { Console.WriteLine("Filtering"); return s.name.StartsWith("A"); }),
                s => { Console.WriteLine("Converting"); return s.name; }
            );
        
        Console.ReadLine();
        foreach(object l in names)
            Console.WriteLine(l);
    }
}

class EnumerableConverter<T, R> : IEnumerable<R>, IEnumerable{
    readonly IEnumerable<T> src;
    readonly Func<T, R> transf;
    public EnumerableConverter(IEnumerable<T> src, Func<T, R> transf) {
        this.src = src; this.transf = transf;
    }
    public IEnumerator<R> GetEnumerator() {
        return new EnumeratorConverter<T, R>(src.GetEnumerator(), transf);
    }
    IEnumerator IEnumerable.GetEnumerator() {
        return this.GetEnumerator();
    }
}

class EnumeratorConverter<T, R> : IEnumerator<R> { // implicito : IEnumerator
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

class EnumerableFilter<T> : IEnumerable<T>{
    readonly IEnumerable<T> src;
    readonly Predicate<T> p;
    public EnumerableFilter(IEnumerable<T> src, Predicate<T> p) {
        this.src = src; this.p = p;
    }
    public IEnumerator<T> GetEnumerator() {
        return new EnumeratorFilter<T>(src.GetEnumerator(), p);
    }
    IEnumerator IEnumerable.GetEnumerator() {
        return this.GetEnumerator();
    }
}

class EnumeratorFilter<T> : IEnumerator<T>{
    readonly IEnumerator<T> src;
    readonly Predicate<T> p;
    T curr;
    public EnumeratorFilter(IEnumerator<T> src, Predicate<T> p) {
        this.src = src; this.p = p;
    }
    
    public bool MoveNext() {
        while (src.MoveNext()) {
            curr = src.Current;
            if(p((T) curr))
                return true;
            else
                curr = default(T);
        }
        return false;
    }
    
    public T Current {
        get{ return curr; }
    }
    
    object IEnumerator.Current {
        get{ return curr; }
    }
    
    public void Reset() { 
        src.Reset();
    }
    public void Dispose() {}
}

class EnumerableDistinct<T> : IEnumerable<T>{
    readonly IEnumerable<T> src;
    public EnumerableDistinct(IEnumerable<T> src) {
        this.src = src; 
    }
    public IEnumerator<T> GetEnumerator() {
        return new EnumeratorDistinct<T>(src.GetEnumerator());
    }
    
    IEnumerator IEnumerable.GetEnumerator() {
        return this.GetEnumerator();
    }
}

class EnumeratorDistinct<T> : IEnumerator<T>{
    readonly IEnumerator<T> src;
    readonly IList selected = new ArrayList();
    T curr;
    public EnumeratorDistinct(IEnumerator<T> src) {
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
    
    public T Current {
        get{ return curr; }
    }
    object IEnumerator.Current {
        get{ return curr; }
    }
    public void Reset() { 
        src.Reset();
    }
    
    public void Dispose() {}
}

