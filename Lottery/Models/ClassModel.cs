namespace Lottery.Models
{
    public class ClassModel
    {
        public string Name { get; set; }
        public List<Student> Students { get; set; } = new();

        public override string ToString()
        {
            return Name;
        }
    }
}
