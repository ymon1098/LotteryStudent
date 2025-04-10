namespace Lottery.Models;

public class Student
{
    public string Name { get; set; }

    // Dodane dla lepszego wyświetlania w ListView
    public override string ToString() => Name;
}