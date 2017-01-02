using Mapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace MapperTest
{
    [TestClass]
    public class AutoMapperEmitTest
    {
        [TestMethod]
        public void MapStudentToSchoolTest() {
            MapperEmit<Student, Person> mapper = AutoMapperEmit
                .Build<Student, Person>()
                .Match("Nr", "Id")
                .Match("School", "Org");

            School isel = new School("Isel", "Lisboa");
            Student s = new Student { Nr = 27721, Name = "Ze Manel", School = isel };

            Person p = mapper.Map(s);
            Assert.AreEqual(p.Id, s.Nr);
            Assert.AreEqual(p.Name, s.Name);
            Assert.AreEqual(p.Org.Name, s.School.Name);
        }
        [TestMethod]
        public void MapSudentsArraysTest()
        {
            MapperEmit<Student, Person> mapper = AutoMapperEmit
                            .Build<Student, Person>()
                            .Match("Nr", "Id")
                            .Match("School", "Org");

            School isel = new School("Isel", "Lisboa");
            Student s = new Student { Nr = 27721, Name = "Ze Manel", School = isel };

            // !!!!!!! Cuidado com o desempenho!!!
            Student[] a = Enumerable.Repeat(s, 1024).ToArray();  // argumento T é inferido a partir de s
            // <=> Student[] a = Enumerable.Repeat<Student>(s, 1024).ToArray(); 
            /* <=>
            Student[] a = new Student[1024];
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = s;
            }
            */

            Person[] ps = mapper.Map(a);
            for (int i = 0; i < a.Length; i++)
            {
                Assert.AreEqual(ps[i].Id, a[i].Nr);
                Assert.AreEqual(ps[i].Name, a[i].Name);
                Assert.AreEqual(ps[i].Org.Name, a[i].School.Name);
            }
        }
    }
}
