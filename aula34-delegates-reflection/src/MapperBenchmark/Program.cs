using Mapper;
using MapperTest;
using System;
using System.Linq;

namespace MapperBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
           School isel = new School("Isel", "Lisboa");
           Student s = new Student { Nr = 27721, Name = "Ze Manel", School = isel };
           Student[] a = Enumerable.Repeat(s, 1024).ToArray();  

           MapperEmit<Student, Person> mapper1 = AutoMapperEmit
                .Build<Student, Person>()
                .Match("Nr", "Id")
                .Match("School", "Org");

            MapperEmit<Student, Person> mapper2 = AutoMapperEmit
                .Load("MapperBenchmark.exe")
                .Build<Student, Person>()
                .Match("Nr", "Id")
                .Match("School", "Org");
            NBench.Bench(() => mapper1.Map(a), "Mapping 1  array of Students");
            NBench.Bench(() => mapper2.Map(a), "Mapping 2  array of Students");

            NBench.Bench(() => mapper1.Map(a), "Mapping 1  array of Students");
            NBench.Bench(() => mapper2.Map(a), "Mapping 2  array of Students");
        }

        [MapperAttribute]
        public static object SchoolToCompany(object target)
        {
            School s = (School)target;
            return new Company(s.Name);
        }
    }
}
