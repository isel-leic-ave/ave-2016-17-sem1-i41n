using Mapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
