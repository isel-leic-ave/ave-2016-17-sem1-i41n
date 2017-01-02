namespace MapperTest
{
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
        public override string ToString()
        {
            return Name;
        }
    }

}