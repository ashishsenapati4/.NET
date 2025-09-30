namespace TagHelperDemo.Models
{
    public class Employee
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string Designation {  get; set; }

        public int Salary { get; set; }

        public String Married { get; set; }

        public String Description { get; set; }

        public Gender Gender { get; set; }
    }
    public enum Gender
    {
        MALE, FEMALE
    }
}
