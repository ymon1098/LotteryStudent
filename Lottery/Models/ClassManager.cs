namespace Lottery.Models;
using System.IO;

public class ClassManager
{
    private readonly string _dataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ClassData");

    public ClassManager()
    {
        Directory.CreateDirectory(_dataDir);
    }

    public List<string> GetClasses()
    {
        return Directory.GetFiles(_dataDir, "*.txt")
                      .Select(Path.GetFileNameWithoutExtension)
                      .ToList();
    }

    public List<Student> GetStudents(string className)
    {
        var path = Path.Combine(_dataDir, $"{className}.txt");
        return File.Exists(path)
            ? File.ReadAllLines(path).Select(name => new Student { Name = name }).ToList()
            : new List<Student>();
    }

    public void AddStudent(string className, Student student)
    {
        File.AppendAllLines(Path.Combine(_dataDir, $"{className}.txt"), new[] { student.Name });
    }

    public void RemoveStudent(string className, Student student)
    {
        var students = GetStudents(className);
        students.RemoveAll(s => s.Name == student.Name);
        File.WriteAllLines(Path.Combine(_dataDir, $"{className}.txt"), students.Select(s => s.Name));
    }
}