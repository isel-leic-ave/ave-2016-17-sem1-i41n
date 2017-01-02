namespace MapperTest
{
    public class Student
    {
        int nr;
        string name;
        School school;


        public int Nr
        {
            get
            {
                return nr;
            }
            set
            {
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

        public School School
        {
            get { return school; }
            set { school = value; }
        }

    }

}