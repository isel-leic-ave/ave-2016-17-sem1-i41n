using Mapper;
using MapperTest;
using System;

namespace MapperBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
           School isel = new School("Isel", "Lisboa");
           Student s = new Student { Nr = 27721, Name = "Ze Manel", School = isel };

            MapperEmit<Student, Person> mapper = AutoMapperEmit
                .Build<Student, Person>()
                .Match("Nr", "Id")
                .Match("School", "Org");

            NBench.Bench(() => mapper.Map(s), "Mapping complex properties");
            Person p = mapper.Map(s);
            Console.ReadLine();
        }
    }
}
