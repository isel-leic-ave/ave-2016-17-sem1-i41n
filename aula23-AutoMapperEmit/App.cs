using System;

class App {
    static MapperEmit<Student, Person> mapperEmit = AutoMapperEmit
            .Build<Student, Person>()
            .Match("Nr", "Id");
   

    static Mapper<Student, Person> mapper = AutoMapper
            .Build<Student, Person>()
            .Match("Nr", "Id");

    static School isel = new School("Isel", "Lisboa");
    static Student s = new Student { Nr = 27721, Name = "Ze Manel", School = isel};

    static void Main() {
        NBench.Bench(BenchMapperPropsEmit, "BenchMapperPropsEmit");
        NBench.Bench(BenchMapperProps, "BenchMapperProps");
    }
    
    static void BenchMapperProps(){
        mapper.Map(s);
    }
    static void BenchMapperPropsEmit(){
        mapperEmit.Map(s);
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

public class Person
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


class NBench
{

    public static void Bench(Action handler, string title) {
        Console.WriteLine("########## BENCHMARKING: {0}", title);
        Perform(handler, 1000, 10);
    }

    private static void Perform(Action handler, int time, int iters)
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
        Result res = new Result() ;
        long maxThroughput = 0;
        for (int i = 0; i < iters; i++)
        {
            Console.Write("---> Iteration {0,2}: ", i);
            res = CallWhile(handler, time);
            long curr = res.OpsPerSec;
            Console.WriteLine("{0} ops/sec", curr);
            if (curr > maxThroughput) maxThroughput = curr;
            GC.Collect();
        }
        Console.WriteLine("============ BEST ===> {0 } ops/sec", maxThroughput);
    }

    private static Result CallWhile(Action handler, int time)
    {
        const int MAX = 32;
        int start = Environment.TickCount;
        int end = start + time;
        int curr = start;
        Result res = new Result();
        do{
            for (int i = 0; i < MAX; i++) handler();
            curr = Environment.TickCount;
            res.ops += MAX;

        } while(curr < end);
        res.durInMs = curr - start;
        return res;
    }



    struct Result {
        public long ops;
        public int durInMs;

        public long OpsPerMsec{
            get {
                return ops / durInMs;
            }
        }

        public long OpsPerSec
        {
            get
            {
                return (ops * 1000) / durInMs;
            }
        }
    }
}