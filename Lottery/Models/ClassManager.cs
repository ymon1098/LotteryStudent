namespace Lottery.Models
{
    public class ClassManager
    {
        public List<ClassModel> Classes { get; set; } = new();

        public void AddClass(string name)
        {
            if (!Classes.Any(c => c.Name == name))
            {
                Classes.Add(new ClassModel { Name = name });
            }
        }

        public void AddStudent(string className, string studentName)
        {
            var selectedClass = Classes.FirstOrDefault(c => c.Name == className);
            if (selectedClass != null && !selectedClass.Students.Any(s => s.Name == studentName))
            {
                selectedClass.Students.Add(new Student { Name = studentName });
            }
        }

        public Student GetRandomStudent(string className)
        {
            var selectedClass = Classes.FirstOrDefault(c => c.Name == className);
            if (selectedClass != null && selectedClass.Students.Any())
            {
                var random = new Random();
                return selectedClass.Students[random.Next(selectedClass.Students.Count)];
            }
            return null;
        }
    }
}
