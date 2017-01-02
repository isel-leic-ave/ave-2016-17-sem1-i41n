namespace MapperTest
{
    public class School
    {
        public School(string name, string loc)
        {
            this.Name = name;
            this.Location = loc;
        }

        public string Name { get; set; }
        public string Location { get; set; }
    }

}