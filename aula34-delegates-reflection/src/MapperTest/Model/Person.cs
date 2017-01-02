namespace MapperTest
{
    public class Person
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public Company Org { get; set; }
        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", Id, Name, Org);
        }
    }

}