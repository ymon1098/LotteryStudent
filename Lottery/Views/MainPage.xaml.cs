using Lottery.Models;

namespace Lottery.Views;

public partial class MainPage : ContentPage
{
    private ClassManager classManager = new();

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnAddClassClicked(object sender, EventArgs e)
    {
        var className = ClassEntry.Text?.Trim();
        if (!string.IsNullOrWhiteSpace(className))
        {
            classManager.AddClass(className);
            RefreshClassPicker();
            ClassEntry.Text = string.Empty;
        }
    }

    private void RefreshClassPicker()
    {
        ClassPicker.ItemsSource = null;
        ClassPicker.ItemsSource = classManager.Classes;
    }

    private void OnClassSelected(object sender, EventArgs e)
    {
 
    }

    private void OnAddStudentClicked(object sender, EventArgs e)
    {
        var studentName = StudentEntry.Text?.Trim();
        if (ClassPicker.SelectedItem is ClassModel selectedClass &&
            !string.IsNullOrWhiteSpace(studentName))
        {
            classManager.AddStudent(selectedClass.Name, studentName);
            StudentEntry.Text = string.Empty;
        }
    }

    private void OnDrawStudentClicked(object sender, EventArgs e)
    {
        if (ClassPicker.SelectedItem is ClassModel selectedClass)
        {
            var student = classManager.GetRandomStudent(selectedClass.Name);
            if (student != null)
            {
                ResultLabel.Text = $"Wylosowano: {student.Name}";
            }
            else
            {
                ResultLabel.Text = "Brak uczniów w tej klasie.";
            }
        }
        else
        {
            ResultLabel.Text = "Najpierw wybierz klasê.";
        }
    }
}
