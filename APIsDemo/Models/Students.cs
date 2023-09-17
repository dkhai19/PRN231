namespace APIsDemo.Models
{
    public class Students
    {
        public int Code { get; set; }
        public string FullNam { get; set; }
        public int Age { get; set; }
        public Students() {}
        public Students(int code, string fullNam, int age)
        {
            Code = code;
            FullNam = fullNam;
            Age = age;
        }
    }
}
