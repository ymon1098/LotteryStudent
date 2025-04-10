using Lottery.Models;
namespace Lottery.Views;

public partial class MainPage : ContentPage
{
    private readonly ClassManager _classManager = new();
    private string _selectedClass;

    public MainPage()
    {
        InitializeComponent();
        LoadClasses();
    }

    private void LoadClasses()
    {
        classPicker.ItemsSource = _classManager.GetClasses();
    }

    private void OnClassSelected(object sender, EventArgs e)
    {
        _selectedClass = classPicker.SelectedItem?.ToString();
        if (_selectedClass != null)
        {
            studentsListView.ItemsSource = _classManager.GetStudents(_selectedClass);
        }
    }

    private async void OnAddStudent(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_selectedClass))
        {
            await DisplayAlert("B³¹d", "Najpierw wybierz klasê!", "OK");
            return;
        }

        string name = await DisplayPromptAsync("Nowy uczeñ", "Wpisz imiê i nazwisko:");
        if (!string.IsNullOrWhiteSpace(name))
        {
            _classManager.AddStudent(_selectedClass, new Student { Name = name });
            RefreshStudentList();
        }
    }

    private async void OnDeleteStudent(object sender, EventArgs e)
    {
        if (studentsListView.SelectedItem is Student selected)
        {
            bool confirm = await DisplayAlert("PotwierdŸ", $"Usun¹æ {selected.Name}?", "Tak", "Nie");
            if (confirm)
            {
                _classManager.RemoveStudent(_selectedClass, selected);
                RefreshStudentList();
            }
        }
        else
        {
            await DisplayAlert("B³¹d", "Zaznacz ucznia do usuniêcia", "OK");
        }
    }

    private void OnDrawStudent(object sender, EventArgs e)
    {
        var students = _classManager.GetStudents(_selectedClass);
        if (students.Count == 0)
        {
            resultLabel.Text = "Brak uczniów w klasie!";
            return;
        }

        var random = new Random();
        var winner = students[random.Next(students.Count)];
        resultLabel.Text = $"Wylosowano: {winner.Name}";
    }

    private void RefreshStudentList()
    {
        studentsListView.ItemsSource = _classManager.GetStudents(_selectedClass);
    }
}